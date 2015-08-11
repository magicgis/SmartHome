using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace HueLightning {
    /// <summary>
    /// Displays information about hue
    /// </summary>
    public class DHueOverview : DisplayPlugin {
        /// <summary>
        /// Contains a instance of <see cref="CHueOverview"/>
        /// </summary>
        public override System.Windows.Forms.UserControl Control {
            get {
                return new CHueOverview();
            }
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.huelightning.overview";
            }
        }

        /// <summary>
        /// <see cref="Plugin.DisplayPlugin.TabName"/>
        /// </summary>
        public override string TabName {
            get {
                return "Hue";
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
    }
}
