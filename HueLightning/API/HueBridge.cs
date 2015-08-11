using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace HueLightning.API {  
    public class HueBridge {
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
        public string BaseUrl;

        public HueBridge(string ip, string applicationName, string deviceName, string userName) {
            _ip = ip;
            _applicationName = applicationName;
            _deviceName = deviceName;
            _username = userName;
            BaseUrl = "http://" + _ip + "/api/" + _username;
        }  

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
