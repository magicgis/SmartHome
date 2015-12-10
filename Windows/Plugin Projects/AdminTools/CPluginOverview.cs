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
    /// The visible component of the <see cref="DPPluginOverview"/> plugin
    /// </summary>
    public partial class CPluginOverview : UserControl {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public CPluginOverview() {
            InitializeComponent();                                              
        }
        
        /// <summary>
        /// Refreshs the plugin list
        /// </summary>
        public void RefreshPlugins() {
            Invoke((MethodInvoker)delegate {
                lbPlugins.Items.Clear();

                foreach (string plugin in Plugin.PluginManager.Plugins)
                    lbPlugins.Items.Add(plugin);
            });  
        }
    }
}
