using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

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
            HueHub.Init();
        }
    }
}
