using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminTools {
    /// <summary>
    /// The visible control of the <see cref="DPLog"/> plugin
    /// </summary>
    public partial class CLog : UserControl {
        private List<KeyValuePair<int, string>> _internalCache = new List<KeyValuePair<int, string>>();

        /// <summary>
        /// Creates a new instance
        /// </summary>             
        public CLog() {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a new message to the log list
        /// </summary>
        /// <param name="channel">The sending channel</param>
        /// <param name="message">The sended message</param>
        public void AddMessage(int channel, string message) {
            if (this.IsDisposed)
                return;

            if (!this.Created) {
                _internalCache.Add(new KeyValuePair<int, string>(channel, message));
                return;
            }   

            if(_internalCache.Count > 0) {
                WriteCache();
            }

            this.Invoke((MethodInvoker)delegate {
                lbLog.Items.Add(String.Format("[{0}]: {1}", Plugin.Debug.GetChannelName(channel), message));
                lbLog.SelectedIndex = lbLog.Items.Count - 1;
            });                                                                                             
        }

        /// <summary>
        /// Writes all messages from the internal cache to the log list
        /// </summary>
        public void WriteCache() {
            foreach (KeyValuePair<int, string> cachedMessage in _internalCache)
                this.Invoke((MethodInvoker)delegate {
                    lbLog.Items.Add(String.Format("[{0}]: {1}", Plugin.Debug.GetChannelName(cachedMessage.Key), cachedMessage.Value));
                    lbLog.SelectedIndex = lbLog.Items.Count - 1;
                });

            _internalCache.Clear();
        }
        
        /// <summary>
        /// Refreshs the displayed list of currently enabled channels
        /// </summary>
        public void RefreshEnabledChannels() {
            if (!this.Created)
                return;
            if (this.IsDisposed)
                return;

            this.Invoke((MethodInvoker)delegate {
                lbEnabledChannels.Items.Clear();

                List<int> ichannels = Plugin.Debug.EnabledChannels.ToList();
                List<string> schannels = new List<string>();

                foreach (int ichannel in ichannels)
                    schannels.Add(Plugin.Debug.GetChannelName(ichannel));

                schannels.Sort();

                foreach (string channel in schannels)
                    lbEnabledChannels.Items.Add(channel);
            });     
        }

        /// <summary>
        /// Refreshs the displayed list of currently disabled channels
        /// </summary>
        public void RefreshDisabledChannels() {
            if (!this.Created)
                return;
            if (this.IsDisposed)
                return;

            this.Invoke((MethodInvoker)delegate {
                lbDisabledChannels.Items.Clear();

                List<int> ichannels = Plugin.Debug.DisabledChannels.ToList();
                List<string> schannels = new List<string>();

                foreach (int ichannel in ichannels)
                    schannels.Add(Plugin.Debug.GetChannelName(ichannel));

                schannels.Sort();

                foreach (string channel in schannels)
                    lbDisabledChannels.Items.Add(channel);
            });
        }

        private void btnEnable_Click(object sender, EventArgs e) {
            if (lbDisabledChannels.SelectedIndex == -1)
                return;

            Plugin.Debug.EnableChannel(Plugin.Debug.GetChannelId(lbDisabledChannels.SelectedItem.ToString()));
        }

        private void btnDisable_Click(object sender, EventArgs e) {
            if (lbEnabledChannels.SelectedIndex == -1)
                return;

            Plugin.Debug.DisableChannel(Plugin.Debug.GetChannelId(lbEnabledChannels.SelectedItem.ToString()));
        }

        private void CLog_Load(object sender, EventArgs e) {
            RefreshDisabledChannels();
            RefreshEnabledChannels();
        }
    }
}
