using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin;

namespace Server {
    public partial class FormMain : Form {
        private Dictionary<TabPage, DisplayPlugin> _tabMap = new Dictionary<TabPage, Plugin.DisplayPlugin>();


        /// <summary>
        /// Creates a new instance
        /// </summary>
        public FormMain() {
            InitializeComponent();
            FormClosing += FormMain_FormClosing;
        }

        private void PluginManager_PluginUnload(Plugin.Plugin plugin) {
            if (plugin.GetType().IsSubclassOf(typeof(DisplayPlugin))) {
                DisplayPlugin dpp = (DisplayPlugin)plugin;
                dpp.OnControlUnload();
                TabPage tp = _tabMap.FirstOrDefault(p => p.Value == dpp).Key;
                _tabMap.Remove(tp);    
            }
        }

        private void DisplayPlugin(DisplayPlugin plugin) {
            Invoke((MethodInvoker)delegate {
                TabPage page = new TabPage(plugin.TabName);
                page.Controls.Add(plugin.Control);
                plugin.Control.Dock = DockStyle.Fill;
                tcTabs.TabPages.Add(page);
                plugin.OnControlLoad();
                _tabMap.Add(page, plugin);
            });
        }

        private void FormMain_Load(object sender, EventArgs e) {     
            foreach (string plugin in PluginManager.DisplayPlugins) 
                DisplayPlugin((DisplayPlugin)PluginManager.GetPlugin(plugin));

            if(tcTabs.SelectedTab != null)
                _tabMap[tcTabs.SelectedTab].OnControlVisible();

            PluginManager.PluginUnload += PluginManager_PluginUnload;
            PluginManager.PluginLoad += PluginManager_PluginLoad;
        }

        private void PluginManager_PluginLoad(Plugin.Plugin plugin) {
            if (plugin.GetType().IsSubclassOf(typeof(DisplayPlugin)))
                DisplayPlugin((DisplayPlugin)plugin);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            PluginManager.UnloadAll();                                      
        }

        private void tcTabs_Deselecting(object sender, TabControlCancelEventArgs e) {
            _tabMap[tcTabs.SelectedTab].OnControlInvisible();    
        }

        private void tcTabs_Selecting(object sender, TabControlCancelEventArgs e) {
            _tabMap[tcTabs.SelectedTab].OnControlVisible();
        }
    }
}
