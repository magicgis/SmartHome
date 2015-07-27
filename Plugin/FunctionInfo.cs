using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Plugin {
    /// <summary>
    /// Provides info and functionality for calling reflected methods
    /// </summary>
    public class FunctionInfo {
        private static int _debugChannel = -1;

        /// <summary>
        /// A custom name of the function
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The instance that will get invoked
        /// </summary>
        public object Instance { get; }
        private MethodInfo Info { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="name">A custom name</param>
        /// <param name="instance">The instance to call the function on</param>
        /// <param name="info">The function info itself</param>
        public FunctionInfo(string name, object instance, MethodInfo info) {
            Name = name;
            Instance = instance;
            Info = info;
                                                                     
            if (_debugChannel == -1)
                _debugChannel = Debug.AddChannel("com.projectgame.plugin.functioninfo");
        }

        /// <summary>
        /// Returns a list with parameter names
        /// </summary>
        public ReadOnlyCollection<string> Parameters {
            get {
                List<string> parameters = new List<string>();

                foreach (ParameterInfo info in Info.GetParameters())
                    parameters.Add(info.Name);

                return parameters.AsReadOnly(); 
            }
        }

        /// <summary>
        /// Returns the type of a specific parameter
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>The return type</returns>
        public Type GetParameterType(string parameter) {
            return GetParamInfo(parameter).ParameterType;
        }

        /// <summary>
        /// Returns the returntype
        /// </summary>
        public Type ReturnType {
            get {
                return Info.ReturnType;
            }
        }

        /// <summary>
        /// Invokes the method with a given parameter set
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <returns>The methods return value</returns>
        public object Invoke(Dictionary<string, object> parameters) {
            //Check missing params
            foreach(string param in Parameters) {
                if (!parameters.ContainsKey(param)) {
                    Debug.Log(_debugChannel, "Could not find param ' " + param + "' on method call '" + Name + "'");
                    throw new ArgumentException();
                }
            }  

            //Check unusable params
            foreach(string param in parameters.Keys) {
                List<string> unusables = new List<string>();
                if (!Parameters.Contains(param)) {
                    Debug.Log(_debugChannel, "Could not resolve param '" + param + "' on method call '" + Name + "'");
                    unusables.Add(param);
                }

                foreach (string unusable in unusables)
                    parameters.Remove(unusable);
            }

            object[] invokeParams = new object[parameters.Count];
            foreach(string parameter in parameters.Keys) {
                invokeParams[GetParamInfo(parameter).Position] = parameters[parameter];
            }

            return Info.Invoke(Instance, invokeParams);
        }

        private ParameterInfo GetParamInfo(string parameter) {
            return Info.GetParameters().FirstOrDefault(p => p.Name.Equals(parameter));
        }
    }
}
