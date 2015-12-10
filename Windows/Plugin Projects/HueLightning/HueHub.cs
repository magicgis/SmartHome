using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Plugin;
using HueLightning.API;

namespace HueLightning {
    internal static class HueHub {
        public static HueBridge Bridge { get; private set; }

        private static bool _initialized;
        private static int _debugChannel;
        
        public static void Init(string dataDir) {
            if (_initialized)
                return;

            _debugChannel = Debug.AddChannel("com.projectgame.huelightning.huehub");
            Debug.Log(_debugChannel, "Loading config");
             
            if(!File.Exists(dataDir + "/config.txt")) {
                Debug.Log(_debugChannel, "Could not found config file");
                return;
            }

            string ip = null;
            string appName = null;
            string devName = null;
            string usrName = null;
            try {
                string fileContent = null;

                using (FileStream fs = new FileStream(dataDir + "/config.txt", FileMode.Open)) {
                    using (StreamReader sr = new StreamReader(fs)) {
                        fileContent = sr.ReadToEnd();
                    }
                }

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(fileContent);

                ip = xml["Config"]["Bridge"]["IP"].InnerText;
                appName = xml["Config"]["Bridge"]["App_Name"].InnerText;
                devName = xml["Config"]["Bridge"]["Dev_Name"].InnerText;
                usrName = xml["Config"]["Bridge"]["Usr_Name"].InnerText;
            } catch(Exception e) {
                Debug.Log(_debugChannel, "Failed loading config file");
                Debug.Log(_debugChannel, "Exception: " + e.Message);
                return;
            }

            Debug.Log(_debugChannel, "IP: " + ip);
            if (!HueBridge.CheckConnection(ip, usrName)) {
                Debug.Log(_debugChannel, "Could not connect to bridge");
                return;
            }

            Bridge = new HueBridge(ip, appName, devName, usrName);
            Debug.Log(_debugChannel, "Connected");
            _initialized = true;
        }
    }
}
