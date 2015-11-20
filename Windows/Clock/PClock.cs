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
        [Plugin.NetworkFunction("com.projectgame.clock.clock.gettimestring")]
        public string NetworkGetTimeString() {
            return DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// Returns the current date
        /// </summary>
        /// <returns>Current date in IXP Format</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.gettime")]
        public IXP.IXPFile NetworkGetTime() {
            IXP.IXPFile file = new IXP.IXPFile();
            file.NetworkFunction = "com.projectgame.clock.clock.gettime";

            file.PutInfo("hour", DateTime.Now.Hour.ToString());
            file.PutInfo("minute", DateTime.Now.Minute.ToString());
            file.PutInfo("second", DateTime.Now.Second.ToString());

            return file;
        }
                                                            
        /// <summary>
        /// Returns the current date string
        /// </summary>
        /// <returns>Current date in String format</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.getdatestring")]
        public string NetworkGetDateString() {
            return DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Returns the current date
        /// </summary>
        /// <returns>IXP File containing the current date</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.clock.getdate")]
        public IXP.IXPFile NetworkGetDate(){
            IXP.IXPFile file = new IXP.IXPFile();
            file.NetworkFunction = "com.projectgame.clock.clock.getdate";

            file.PutInfo("day", DateTime.Now.Day.ToString());
            file.PutInfo("month", DateTime.Now.Month.ToString());
            file.PutInfo("year", DateTime.Now.Year.ToString());
            file.PutInfo("weekday", "" + (((int)DateTime.Now.DayOfWeek) - 1));

            return file;
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
