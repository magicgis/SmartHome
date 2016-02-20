using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Networking;                

namespace Plugin {
    /// <summary>
    /// Manages all network stuff
    /// </summary>
    public static class NetworkManager{
        /// <summary>
        /// Gets called if a network function is registered or unregistered
        /// </summary>
        public static event EventDelegate NetworkFunctionsChanged;

        private static Server _server;

        private static List<FunctionInfo> _networkFunctions = new List<FunctionInfo>();
        private static int _debugChannel;           

        /// <summary>
        /// Initializes the manager
        /// </summary>
        public static void Init() {
            PluginManager.PluginUnload += PluginManager_PluginUnload;
            _debugChannel = Debug.AddChannel("com.projectgame.plugin.networkmanager");
            Debug.Log(_debugChannel, "Starting Server");
            _server = new Server();
            _server.Start();
            Server.ClientConnected += Server_ClientConnected;
            Server.ClientDisconnected += Server_CliendDisconnected;
            Server.MessageReceived += Server_MessageReceived;
        }

        private static void Server_MessageReceived(ServerInstance arg1, byte[] arg2) {
            Debug.Log(_debugChannel, "Client '" + arg1.IP + "' sent " + arg2.Length + " bytes");

            //Check for multiple messages
            String message = Encoding.UTF8.GetString(arg2);

            int xmlCount = Regex.Matches(message, Regex.Escape("<?xml version=")).Count;

            if(xmlCount == 1) {
                HandleMessage(arg1, message);
                return;
            }

            List<int> indices = new List<int>();
            foreach (int index in message.IndexesOf("<?xml version="))
                indices.Add(index);
            
            for(int i = 0; i < indices.Count; i++) {
                string substring = null;

                if (i == indices.Count - 1) {
                    substring = message.Substring(indices[i]);
                } else {
                    substring = message.Substring(indices[i], indices[i + 1] - indices[i]);
                }

                HandleMessage(arg1, substring);
            }  
        }

        private static IEnumerable<int> IndexesOf(this string haystack, string needle) {
            int lastIndex = 0;
            while (true) {
                int index = haystack.IndexOf(needle, lastIndex);
                if (index == -1) {
                    yield break;
                }
                yield return index;
                lastIndex = index + needle.Length;
            }
        }

        private static void HandleMessage(ServerInstance arg1, string message) {
            IXPFile ixp = new IXPFile(message);

            FunctionInfo nfunc = _networkFunctions.FirstOrDefault(nf => nf.Name.Equals(ixp.NetworkFunction));

            if (nfunc != null)
                Debug.Log(_debugChannel, "Function: " + ixp.NetworkFunction);
            else {
                Debug.Log(_debugChannel, "Function: Unknown");
                return;
            }

            Dictionary<string, object> dicParams = new Dictionary<string, object>();
            foreach (string param in nfunc.Parameters) {
                if (ixp.GetInfoValue(param) == null && nfunc.GetParameterType(param) != typeof(ServerInstance)) {
                    Debug.Log(_debugChannel, "Missing parameter: " + param);
                    return;
                }

                object paramVal = null;
                Type t = nfunc.GetParameterType(param);
                if (t == typeof(ServerInstance)) {
                    paramVal = arg1;
                } else {
                    string strVal = ixp.GetInfoValue(param);
                    
                    if (t == typeof(string)) {
                        paramVal = strVal;
                    } else if (t == typeof(int)) {
                        int i = -1;
                        bool s = int.TryParse(strVal, out i);
                        if (s)
                            paramVal = i;
                    } else if(t == typeof(byte)) {
                        byte b = 0;
                        bool s = byte.TryParse(strVal, out b);
                        if (s)
                            paramVal = b; 
                    } else if(t == typeof(ushort)){
                        ushort us = 0;
                        bool s = ushort.TryParse(strVal, out us);
                        if (s)
                            paramVal = us;
                    } else if (t == typeof(bool)) {
                        if (strVal.Equals("TRUE"))
                            paramVal = true;
                        else if (strVal.Equals("FALSE"))
                            paramVal = false;
                    }

                    if (paramVal == null) {
                        Debug.Log(_debugChannel, "Invalid type for param " + param + ". Expected " + t.ToString() + ". Got Value: " + strVal);
                        return;
                    }
                }    

                dicParams.Add(param, paramVal);
            }

            if (nfunc.ReturnType == typeof(void))
                nfunc.Invoke(dicParams);
            else if (nfunc.ReturnType == typeof(IXPFile)) {
                IXPFile file = (IXPFile)nfunc.Invoke(dicParams);
				if(ixp.Headers.Contains ("req_id"))
					file.PutHeader ("req_id", ixp.GetHeaderValue ("req_id"));
                arg1.Send(Encoding.UTF8.GetBytes(file.XML));
            } else {
                object response = nfunc.Invoke(dicParams);
                IXPFile iresponse = new IXPFile();
				iresponse.NetworkFunction = ixp.NetworkFunction;
				if(ixp.Headers.Contains("req_id"))
					iresponse.PutHeader ("req_id", ixp.GetHeaderValue ("req_id"));
                iresponse.PutInfo("Response", response.ToString());
                arg1.Send(Encoding.UTF8.GetBytes(iresponse.XML));
            }
        }

        private static void Server_CliendDisconnected(ServerInstance obj) {
            Debug.Log(_debugChannel, "Client '" + obj.IP + "' disconnected");
        }

        private static void Server_ClientConnected(ServerInstance obj) {
            Debug.Log(_debugChannel, "Client '" + obj.IP + "' connected");
        }

        /// <summary>
        /// Closes the networkmanager
        /// </summary>
        public static void Close() {
            Debug.Log(_debugChannel, "Stopping Server");
            Server.ClientConnected -= Server_ClientConnected;
            Server.ClientDisconnected -= Server_CliendDisconnected;
            Server.MessageReceived -= Server_MessageReceived;
            _server.Stop();
        }

        /// <summary>
        /// A readonly collection of network functions
        /// </summary>
        public static ReadOnlyCollection<string> NetworkFunctions {
            get {
                List<string> functions = new List<string>();

                foreach (FunctionInfo info in _networkFunctions)
                    functions.Add(info.Name);

                return functions.AsReadOnly();
            }
        }

        /// <summary>
        /// Returns the corresponding network function to the custom name
        /// </summary>
        /// <param name="name">The network functions name</param>
        /// <returns>FunctionInfo of the network function</returns>
        public static FunctionInfo GetNetworkFunction(string name) {
            return _networkFunctions.FirstOrDefault(f => f.Name.Equals(name));
        }

        /// <summary>
        /// Registers a networkfunction so it can get called
        /// </summary>
        /// <param name="info">The functions info</param>
        public static void RegisterNetworkFunction(FunctionInfo info) {
            if (_networkFunctions.FirstOrDefault(f => f.Name.Equals(info.Name)) != null)
                return;

            _networkFunctions.Add(info);

            NetworkFunctionsChanged?.Invoke();
            //Debug.Log(_debugChannel, "Method '" + info.Name + "' has been added");
        }          
        /// <summary>
        /// Unregisteres a networkfunction so it cant get called anymore
        /// </summary>
        /// <param name="name">The functions info</param>
        public static void UnregisterNetworkFunction(string name) {
            FunctionInfo info = GetNetworkFunction(name);

            _networkFunctions.Remove(info);

            NetworkFunctionsChanged?.Invoke();
            //Debug.Log(_debugChannel, "Method '" + info.Name + "' has been removed");
        }
                                                                         
        private static void PluginManager_PluginUnload(Plugin plugin) {
            FunctionInfo current = null;

            while((current = _networkFunctions.FirstOrDefault(n => n.Instance == plugin)) != null) {
                UnregisterNetworkFunction(current.Name);  
            }
        }
    }
}
