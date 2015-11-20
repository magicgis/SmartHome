using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin;

namespace AdminTools {
    /// <summary>
    /// Displays the applications log and lets the user take control about the currently avaliable debug channels
    /// </summary>
    public class DPLog : DisplayPlugin {
        private CLog _control = new CLog();

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
                return "com.projectgame.admintools.displaylogging";
            }
        }
        
        /// <summary>
        /// <see cref="DisplayPlugin.TabName"/>
        /// </summary>
        public override string TabName {
            get {
                return "Log";
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
            Debug.MessageListened += Debug_MessageListened;
            Debug.DisabledChannelsChanged += Debug_DisabledChannelsChanged;
            Debug.EnabledChannelsChanged += Debug_EnabledChannelsChanged;

            _control.RefreshDisabledChannels();
            _control.RefreshEnabledChannels();

            foreach (KeyValuePair<int, string> cachedMessage in Debug.Cache)
                _control.AddMessage(cachedMessage.Key, cachedMessage.Value);
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginUnload"/>
        /// </summary>
        public override void OnPluginUnload() {
            Debug.MessageListened -= Debug_MessageListened;
            Debug.DisabledChannelsChanged -= Debug_DisabledChannelsChanged;
            Debug.EnabledChannelsChanged -= Debug_EnabledChannelsChanged;
        }                               

        /// <summary>
        /// <see cref="DisplayPlugin.OnControlLoad"/>
        /// </summary>
        public override void OnControlLoad() {
            _control.WriteCache();
        }

        private void Debug_EnabledChannelsChanged() {
            _control.RefreshEnabledChannels();
        }

        private void Debug_DisabledChannelsChanged() {
            _control.RefreshDisabledChannels();
        }

        private void Debug_MessageListened(int channel, string message) {
            _control.AddMessage(channel, message);
        }
    }
}
