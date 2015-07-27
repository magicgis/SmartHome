using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace Clock {
    /// <summary>
    /// Provides network functions for time based operations
    /// </summary>
    public class PClock : Plugin.Plugin {
        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.clock.clock";
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
            TimeService.Start();
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginUnload"/>
        /// </summary>
        public override void OnPluginUnload() {
            TimeService.Stop();
        }

        /// <summary>
        /// Returns the current time string
        /// </summary>
        /// <returns>Current time in String format</returns>     
        [Plugin.NetworkFunction("com.projectgame.clock.clock.gettime")]
        public string NetworkGetTime() {
            return DateTime.Now.ToLongTimeString();
        }
                                                            
        /// <summary>
        /// Returns the current date string
        /// </summary>
        /// <returns>Current date in String format</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.getdate")]
        public string NetworkGetDate() {
            return DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Registers the sending client to the time service
        /// </summary>
        /// <param name="instance">The sending client</param>
        /// <param name="functionName">The clients function name</param>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.registertotimeservice")]
        public void NetworkRegisterToTimeService(ServerInstance instance, string functionName) {
            TimeService.Register(instance, functionName);  
        }

        /// <summary>
        /// Unregisters the sending client from the time service
        /// </summary>
        /// <param name="instance">The sending client</param>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.unregisterfromtimeservice")]
        public void NetworkUnregisterFromTimeService(ServerInstance instance) {
            TimeService.Unregister(instance);
        }
    }
}
