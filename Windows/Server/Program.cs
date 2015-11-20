using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin;

namespace Server {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            int debugChannel = Debug.AddChannel("com.projectgame.server.main");
            Debug.EnableCaching();
            Debug.Log(debugChannel, "Initializing");

            NetworkManager.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Debug.Log(debugChannel, "Loading Plugins");
            PluginManager.LoadFromDir(new System.IO.DirectoryInfo(Application.StartupPath + "/Plugins"));               

            Debug.Log(debugChannel, "Loading UI");
            Application.Run(new FormMain());

            NetworkManager.Close();
        }
    }
}
