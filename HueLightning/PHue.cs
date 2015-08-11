using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;
using IXP;
using HueLightning.API;

namespace HueLightning {
    public class PHue : Plugin.Plugin {
        public override string Name {
            get {
                return "com.projectgame.huelightning.hue";
            }
        }

        public override int Version {
            get {
                return 1;
            }
        }

        public override void OnPluginLoad() {
            HueHub.Init(DataDir);
        }

        [NetworkFunction("com.projectgame.huelightning.hue.getlights")]
        public IXPFile NetworkGetLights() {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            IXPFile res = new IXPFile();
            res.NetworkFunction = "com.projectgame.huelightning.hue.getlights";
            res.PutInfo("Count", "" + lights.Count);
            
            for(int i = 0; i < lights.Count; i++) {
                res.PutInfo("" + i, lights.ElementAt(i).Id);
            }

            return res;
        }

        [NetworkFunction("com.projectgame.huelightning.hue.getlightname")]
        public string NetworkGetLightName(string light_id) {
            IReadOnlyCollection<HueLight> lights  = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            return light?.Name;
        }

        [NetworkFunction("com.projectgame.huelightning.hue.getlightenabled")]
        public bool NetworkGetEnabled(string light_id) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            return light == null ? false : light.On;
        }

        [NetworkFunction("com.projectgame.huelightning.hue.getlightcolor")]
        public IXPFile NetworkGetColor(string light_id) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            IXPFile res = new IXPFile();
            res.NetworkFunction = "com.projectgame.huelightning.hue.getcolor";
            res.PutInfo("bri", "" + light.Brightness);
            res.PutInfo("sat", "" + light.Saturation);
            res.PutInfo("hue", "" + light.Hue);

            return res;
        }

        [NetworkFunction("com.projectgame.huelightning.hue.setlightenabled")]
        public void NetworkSetEnabled(string light_id, bool enabled) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            light.On = enabled;
        }

        [NetworkFunction("com.projectgame.huelightning.hue.setlightcolor")]
        public void NetworkSetColor(string light_id, byte bri, byte sat, ushort hue) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            light.Brightness = bri;
            light.Saturation = sat;
            light.Hue = hue;
        }
    }
}
