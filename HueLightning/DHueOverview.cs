using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace HueLightning {
    public class DHueOverview : DisplayPlugin {
        public override System.Windows.Forms.UserControl Control {
            get {
                return new CHueOverview();
            }
        }

        public override string Name {
            get {
                return "com.projectgame.huelightning.overview";
            }
        }

        public override string TabName {
            get {
                return "Hue";
            }
        }

        public override int Version {
            get {
                return 1;
            }
        }
    }
}
