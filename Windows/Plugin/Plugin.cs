using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Plugin {
    /// <summary>
    /// Base class for all custom plugins that get loaded dynamically at runtime
    /// </summary>
    public abstract class Plugin {
        /// <summary>                                      
        /// The display name of the plugin. This will also be used for identification
        /// </summary>
        public abstract String Name { get; }
        /// <summary>
        /// The current version of the plugin
        /// </summary>
        public abstract int Version { get; }

        /// <summary>
        /// Provides the path to a plugin specific directory to save data
        /// </summary>
        public string DataDir {
            get {
                string dataDir = System.Windows.Forms.Application.StartupPath + "/Data/" + Name;
                if (!System.IO.Directory.Exists(dataDir))
                    System.IO.Directory.CreateDirectory(dataDir);

                return dataDir;
            }
        }

        /// <summary>
        /// Returns a MySqlConnection for the plugins specific database
        /// </summary>
        public MySqlConnection GetDatabaseConnection {
            get {
                MySqlConnection checkCon = new MySqlConnection("Server=127.0.0.1; Uid=dev; Pwd=test"); //"Server=192.168.178.94; Uid=dev; Pwd=bischi300");
                checkCon.Open();
                MySqlCommand cmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS " + Name.Replace('.', '_') + "", checkCon);
                cmd.ExecuteNonQuery();
                checkCon.Close();

                MySqlConnection con = new MySqlConnection("Server=127.0.0.1; Database=" + Name.Replace('.', '_') + "; Uid=dev; Pwd=test");
                return con;
            }
        }

        /// <summary>
        /// This method will get called when this plugin is loaded. It is not guaranteed that other plugins already are loaded too
        /// </summary>
        public virtual void OnPluginLoad() { }
        /// <summary>
        /// This method will get called once loading all plugins is finished 
        /// </summary>
        public virtual void OnPluginsLoad() { }

        /// <summary>
        /// This method will get called if the plugin system attempts to unload one or more plugins. At call time no plugin is unloaded
        /// <param name="plugins">A list of plugins that is going to be unloaded</param>
        /// </summary>
        public virtual void OnPluginsUnload(List<string> plugins) { }
        /// <summary>
        /// This method will get called once this plugin gets unloaded. It is not guaranteed that other plugins are loaded any more
        /// </summary>
        public virtual void OnPluginUnload() { }
    }
}
