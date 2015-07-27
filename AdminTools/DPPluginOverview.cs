using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin;

namespace AdminTools {
    /// <summary>
    /// Shows all currently loaded plugins
    /// </summary>
    public class DPPluginOverview : DisplayPlugin {
        private CPluginOverview _control = new CPluginOverview();

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
                return "com.projectgame.admintools.pluginoverview";
            }
        }

        /// <summary>
        /// <see cref="DisplayPlugin.TabName"/>
        /// </summary>
        public override string TabName {
            get {
                return "Plugins";
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
            PluginManager.PluginLoad += PluginManager_PluginLoad;
            PluginManager.PluginUnload += PluginManager_PluginUnload;
            _control.RefreshPlugins();
        }

        private void PluginManager_PluginUnload(Plugin.Plugin plugin) {
            _control.RefreshPlugins();
        }

        private void PluginManager_PluginLoad(Plugin.Plugin plugin) {
            _control.RefreshPlugins();
        }

        /// <summary>
        /// <see cref="DisplayPlugin.OnControlUnload"/>
        /// </summary>
        public override void OnControlUnload() {
            PluginManager.PluginLoad -= PluginManager_PluginLoad;
            PluginManager.PluginUnload -= PluginManager_PluginUnload;
        }  
    }
}
