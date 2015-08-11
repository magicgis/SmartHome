using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace HueLightning.API {  
    /// <summary>
    /// The Bridge is the base station that manages all lights in the house
    /// </summary>
    public class HueBridge {
        /// <summary>
        /// Checks if the server can connect to a specific bridge. Should be checked before creating an instance
        /// </summary>
        /// <param name="ip">The bridges (local) ip</param>
        /// <param name="username">The username the server takes to identify at the bridge</param>
        /// <returns>Bool if connection could be etablished</returns>
        public static bool CheckConnection(string ip, string username) {
            try {
                HttpGETRequest r = new HttpGETRequest("http://" + ip, "api/" + username);
                r.Send();
                string response = r.Read();
                JObject json = JObject.Parse(response);
                JToken lightToken = json.GetValue("lights");
                return response.Length > 0;
            } catch {
                return false;
            }                              
        }

        private string _ip;
        private string _applicationName;
        private string _deviceName;
        private string _username;
        /// <summary>
        /// Contains a base url for network communication
        /// http://{bridge_ip}/api/{username}
        /// </summary>
        public string BaseUrl;

        /// <summary>
        /// Creates a new instance and sets the base url
        /// </summary>
        /// <param name="ip">The bridges ip</param>
        /// <param name="applicationName">The used application name</param>
        /// <param name="deviceName">The used device name</param>
        /// <param name="userName">The used userName</param>
        public HueBridge(string ip, string applicationName, string deviceName, string userName) {
            _ip = ip;
            _applicationName = applicationName;
            _deviceName = deviceName;
            _username = userName;
            BaseUrl = "http://" + _ip + "/api/" + _username;
        }  

        /// <summary>
        /// Returns a list of connected Lights
        /// </summary>
        public ReadOnlyCollection<HueLight> HueLights {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(BaseUrl, "lights");
                    req.Send();
                    string res = req.Read();
                    JObject json = JObject.Parse(res);
                    List<HueLight> lights = new List<HueLight>();
                    foreach (JToken light in json.Children()) {
                        string id = light.Path;                          
                        lights.Add(new HueLight(id, this));
                    }
                    return lights.AsReadOnly();
                } catch {
                    return null;
                }
            }
        }
    }
}
