using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdminTools {
    /// <summary>
    /// Creates text files that contains the applications logs
    /// </summary>
    public class PLog : Plugin.Plugin {
        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.admintools.logging";
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

        private FileStream _stream;
        private StreamWriter _writer;
        private int _debugChannel;

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginLoad"/>
        /// </summary>
        public override void OnPluginLoad() {
            _debugChannel = Plugin.Debug.AddChannel("com.projectgame.admintools.logging");
            Plugin.Debug.Log(_debugChannel, "Initialized Logging");

            string filePath = DataDir + "/" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".txt";
            _stream = new FileStream(filePath, FileMode.Create);
            _writer = new StreamWriter(_stream);   

            foreach (KeyValuePair<int, string> cachedMessage in Plugin.Debug.Cache)
                WriteMessage(cachedMessage.Key, cachedMessage.Value);

            Plugin.Debug.MessageListened += WriteMessage;
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginUnload"/>
        /// </summary>
        public override void OnPluginUnload() {
            Plugin.Debug.Log(_debugChannel, "Plugin gets unloaded. End of stream");

            _writer.Flush();
            _writer.Close();
            _writer.Dispose();
            _writer = null;
            _stream = null;
        }

        private void WriteMessage(int channel, string message) {
            if (_writer == null)
                return;

            string str = String.Format("[{0} - {1}][{2}]: {3}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), Plugin.Debug.GetChannelName(channel), message);
            _writer.WriteLine(str);
        }
    }
}
