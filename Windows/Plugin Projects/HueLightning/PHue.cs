using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;
using HueLightning.API;
using Networking;

namespace HueLightning {
    /// <summary>
    /// A Plugin for hue lightning
    /// </summary>
    public class PHue : Plugin.Plugin {
        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.huelightning.hue";
            }
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.Version"/>
        /// </summary>
        public override int Version {
            get {
                return 1;
            }
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginLoad"/>
        /// </summary>
        public override void OnPluginLoad() {
            HueHub.Init(DataDir);
        }

        /// <summary>
        /// Returns all light ids
        /// </summary>
        /// <returns>
        /// Light ids as IXP File List
        /// Infos:
        ///     Count -> The number of elements
        ///     0 -> The first element
        ///     1 -> The second element
        ///     etc...
        /// </returns>
        [NetworkFunction("com.projectgame.huelightning.hue.getlights")]
        public IXPFile NetworkGetLights() {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            IXPFile res = new IXPFile();
            res.NetworkFunction = "com.projectgame.huelightning.hue.getlights";
            res.PutInfo("Count", "" + lights.Count);
            
            for(int i = 0; i < lights.Count; i++) {
                res.PutInfo("L" + i, lights.ElementAt(i).Id);
            }

            return res;
        }

        /// <summary>
        /// Returns the name of a light
        /// </summary>
        /// <param name="light_id">The light</param>
        /// <returns>The name</returns>
        [NetworkFunction("com.projectgame.huelightning.hue.getlightname")]
        public string NetworkGetLightName(string light_id) {
            IReadOnlyCollection<HueLight> lights  = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            return light?.Name;
        }

        /// <summary>
        /// Returns if the light is turned on
        /// </summary>
        /// <param name="light_id">The light</param>
        /// <returns>The on state</returns>
        [NetworkFunction("com.projectgame.huelightning.hue.getlightenabled")]
        public bool NetworkGetEnabled(string light_id) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            return light == null ? false : light.On;
        }

        /// <summary>
        /// Returns a lights color
        /// </summary>
        /// <param name="light_id">The light</param>
        /// <returns>
        /// The color as IXP File
        /// Infos:
        ///     bri -> Brightness
        ///     sat -> Saturation
        ///     hue -> HUE
        /// </returns>
        [NetworkFunction("com.projectgame.huelightning.hue.getlightcolor")]
        public IXPFile NetworkGetColor(string light_id) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            IXPFile res = new IXPFile();
            res.NetworkFunction = "com.projectgame.huelightning.hue.getlightcolor";
            res.PutInfo("bri", "" + light.Brightness);
            res.PutInfo("sat", "" + light.Saturation);
            res.PutInfo("hue", "" + light.Hue);

            return res;
        }

        /// <summary>
        /// Sets if a light should be enabled
        /// </summary>
        /// <param name="light_id">The light</param>
        /// <param name="enabled">The enabled state</param>
        [NetworkFunction("com.projectgame.huelightning.hue.setlightenabled")]
        public void NetworkSetEnabled(string light_id, bool enabled) {
            IReadOnlyCollection<HueLight> lights = HueHub.Bridge.HueLights;
            HueLight light = lights.FirstOrDefault(l => l.Id.Equals(light_id));

            light.On = enabled;
        }

        /// <summary>
        /// Sets a lights color
        /// </summary>
        /// <param name="light_id">The light</param>
        /// <param name="bri">Brightness</param>
        /// <param name="sat">Saturation</param>
        /// <param name="hue">Hue</param>
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
