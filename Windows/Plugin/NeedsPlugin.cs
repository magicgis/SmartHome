using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin {
    /// <summary>
    /// Should only be applied to Plugin classed
    /// 
    /// This attribute tells the plugin system, that another plugin is required for this plugin to work.
    /// This plugin will not be loaded, if the required plugin is not loaded.
    /// At startup, this plugin will wait
    /// At runtime, the loading process will be canceled
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class NeedsPlugin : System.Attribute {
        /// <summary>
        /// The name of the needed plugin
        /// </summary>
        public String NeededPlugin { get; }

        /// <summary>
        /// Constructor of NeedsPlugin
        /// </summary>
        /// <param name="neededPlugin">The name of the needed plugin</param>
        public NeedsPlugin(string neededPlugin) {
            NeededPlugin = neededPlugin;
        }         
    }
}
                                                                               