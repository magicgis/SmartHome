using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin {
    /// <summary>
    /// Base class for all display plugins that provide GUI
    /// </summary>
    public abstract class DisplayPlugin : Plugin{
        /// <summary>
        /// The visible control of the plugin. This control will get its own tab page and gets DockStyle.Fill at startup
        /// </summary>
        public abstract UserControl Control { get; }
        /// <summary>
        /// The visible name of the tab page
        /// </summary>
        public abstract String TabName { get; }

        /// <summary>
        /// This method will get called when the plugins control is loaded
        /// </summary>
        public virtual void OnControlLoad() { }
        /// <summary>
        /// This method will get called when the plugins control is unloaded
        /// </summary>
        public virtual void OnControlUnload() { }

        /// <summary>
        /// This method will get called when the plugins control gets invisible
        /// </summary>
        public virtual void OnControlInvisible() { }
        /// <summary>
        /// This metod will get called when the plugins control gets visible
        /// </summary>
        public virtual void OnControlVisible() { }
    }
}
