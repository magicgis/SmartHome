using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin;

namespace AdminTools {
    /// <summary>
    /// Displays the currently avaliable network functions
    /// </summary>
    public class DPNetworkFunctions : DisplayPlugin {
        CNetworkFunctions _control = new CNetworkFunctions();

        /// <summary>
        /// <see cref="DisplayPlugin.Control"/>
        /// </summary>
        public override UserControl Control {
            get {
                return _control;
            }
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.admintools.networkfunctions";
            }
        }

        /// <summary>
        /// <see cref="DisplayPlugin.TabName"/>
        /// </summary>
        public override string TabName {
            get {
                return "Network Functions";
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
        /// <see cref="DisplayPlugin.OnControlLoad"/>
        /// </summary>
        public override void OnControlLoad() {
            NetworkManager.NetworkFunctionsChanged += NetworkManager_NetworkFunctionsChanged;

            _control.RefreshFunctions();
        }

        /// <summary>
        /// <see cref="DisplayPlugin.OnControlUnload"/>
        /// </summary>
        public override void OnControlUnload() {
            NetworkManager.NetworkFunctionsChanged -= NetworkManager_NetworkFunctionsChanged;   
        }

        private void NetworkManager_NetworkFunctionsChanged() {
            _control.RefreshFunctions();
        }
    }
}
