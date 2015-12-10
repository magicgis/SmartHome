using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json.Linq;
using Plugin;

namespace HueLightning.API {
    /// <summary>
    /// Represents a wrapper for Lights connected to the hue bridge
    /// </summary>
    public class HueLight {
        private static int _debugChannel = -1;

        /// <summary>
        /// The public ID of the Light. Will not change
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// The Bridge this light is connected to
        /// </summary>
        public HueBridge Bridge { get; private set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The lights unique id</param>
        /// <param name="bridge">The parent bridge</param>
        public HueLight(string id, HueBridge bridge) { 
            Id = id;
            Bridge = bridge;
            if (_debugChannel == -1)
                _debugChannel = Debug.AddChannel("com.projectgame.huelightning.huelight");
        }

        /// <summary>
        /// The lights (display) name
        /// </summary>
        public string Name {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(Bridge.BaseUrl, "lights/" + Id);
                    req.Send();
                    string response = req.Read();
                    JObject json = JObject.Parse(response);
                    JToken nameValue = json.First.Next.Next;
                    JValue value = (JValue)nameValue.First;
                    return (string)value.Value;
                } catch(Exception e){
                    Debug.Log(_debugChannel, $"Could not fetch name. Exception={e.Message}");
                    return null;
                }
            }
            set {
                try {
                    string body = $"{{\"name\":\"{value}\"}}";
                    HttpPUTRequest req = new HttpPUTRequest($"{Bridge.BaseUrl}/lights/{Id}", body);
                    req.Send();
                    req.Read();   
                }catch(Exception e) {
                    Debug.Log(_debugChannel, $"Could not set name. Exception={e.Message}");
                }
            }
        }

        /// <summary>
        /// Is the light on or off?
        /// </summary>
        public bool On {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(Bridge.BaseUrl, "lights/" + Id);
                    req.Send();
                    string response = req.Read();
                    JObject json = JObject.Parse(response);
                    JToken onValue = json.First.First.First;
                    JValue value = (JValue)onValue.First;
                    return (bool)value.Value;
                }catch(Exception e) {
                    Debug.Log(_debugChannel, $"Could not fetch on state. Exception={e.Message}");
                    return false;
                }
            }
            set {
                try {
                    string body = $"{{\"on\":{(value ? "true" : "false")}}}";
                    HttpPUTRequest req = new HttpPUTRequest($"{Bridge.BaseUrl}/lights/{Id}/state", body);
                    req.Send();
                    req.Read();
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not set on state. Exception={e.Message}");
                }
            }
        }

        /// <summary>
        /// The brightness
        /// </summary>
        public byte Brightness {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(Bridge.BaseUrl, "lights/" + Id);
                    req.Send();
                    string response = req.Read();
                    JObject json = JObject.Parse(response);
                    JToken onValue = json.First.First.First.Next;
                    JValue value = (JValue)onValue.First;   
                    return (byte)((long)value.Value);
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not fetch brightness. Exception={e.Message}");
                    return 0;
                }
            }
            set {
                try {
                    string body = $"{{\"bri\":{value}}}";
                    HttpPUTRequest req = new HttpPUTRequest($"{Bridge.BaseUrl}/lights/{Id}/state", body);
                    req.Send();
                    req.Read();
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not set brightness. Exception={e.Message}");
                }
            }
        }

        /// <summary>
        /// The lights hue. Not supported by Lux Lights
        /// </summary>
        public ushort Hue {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(Bridge.BaseUrl, "lights/" + Id);
                    req.Send();
                    string response = req.Read();
                    JObject json = JObject.Parse(response);
                    JToken onValue = json.First.First.First.Next.Next;
                    JValue value = (JValue)onValue.First;
                    return (ushort)((long)value.Value);
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not fetch hue. Exception={e.Message}");
                    return 0;
                }
            }
            set {
                try {
                    string body = $"{{\"hue\":{value}}}";
                    HttpPUTRequest req = new HttpPUTRequest($"{Bridge.BaseUrl}/lights/{Id}/state", body);
                    req.Send();
                    req.Read();
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not set hue. Exception={e.Message}");
                }
            }
        }

        /// <summary>
        /// The lights saturation. Not supported by Lux Lights
        /// </summary>
        public byte Saturation {
            get {
                try {
                    HttpGETRequest req = new HttpGETRequest(Bridge.BaseUrl, "lights/" + Id);
                    req.Send();
                    string response = req.Read();
                    JObject json = JObject.Parse(response);
                    JToken onValue = json.First.First.First.Next.Next.Next;
                    JValue value = (JValue)onValue.First;
                    return (byte)((long)value.Value);
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not fetch saturation. Exception={e.Message}");
                    return 0;
                }
            }
            set {
                try {
                    string body = $"{{\"sat\":{value}}}";
                    HttpPUTRequest req = new HttpPUTRequest($"{Bridge.BaseUrl}/lights/{Id}/state", body);
                    req.Send();
                    req.Read();
                } catch (Exception e) {
                    Debug.Log(_debugChannel, $"Could not set saturation. Exception={e.Message}");
                }
            }
        }

        /// <summary>
        /// <see cref="object.ToString"/>
        /// </summary>
        /// <returns><see cref="object.ToString"/></returns>
        public override string ToString() {
            return $"{Id} - {Name}";
        }
    }
}
