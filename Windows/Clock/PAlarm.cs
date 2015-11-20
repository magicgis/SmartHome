using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IXP;

namespace Clock {
    /// <summary>
    /// Manages all functions for alarms
    /// </summary>
    public class PAlarm : Plugin.Plugin {
        /// <summary>
        /// <see cref="Plugin.Plugin.Name"/>
        /// </summary>
        public override string Name {
            get {
                return "com.projectgame.clock.alarm";
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
            AlarmDbConnector.Init(GetDatabaseConnection);
            AlarmService.Start();
        }

        /// <summary>
        /// <see cref="Plugin.Plugin.OnPluginUnload"/>
        /// </summary>
        public override void OnPluginUnload() {
            AlarmService.Stop();
        }

        /// <summary>
        /// Registers an new alarm
        /// </summary>
        /// <param name="name">The name or the alarm</param>
        /// <param name="hours">The hours of the time</param>
        /// <param name="minutes">The minutes of the time</param>
        /// <param name="seconds">The seconds of the time</param>
        /// <param name="mon">If alarm is enabled on mon</param>
        /// <param name="tue">If alarm is enabled on tue</param>
        /// <param name="wed">If alarm is enabled on wed</param>
        /// <param name="thu">If alarm is enabled on thu</param>
        /// <param name="fri">If alarm is enabled on fri</param>
        /// <param name="sat">If alarm is enabled on sat</param>
        /// <param name="sun">If alarm is enabled on sun</param>
        /// <param name="enabled">If alarm is enabled</param>
        /// <returns>The id of the alarm. -1 if fails</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.registeralarm")]
        public int NetworkRegisterAlarm(
            string name,
            int hours, int minutes, int seconds,
            bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun,
            bool enabled) {
            return AlarmDbConnector.AddAlarm(name, hours, minutes, seconds, mon, tue, wed, thu, fri, sat, sun, enabled);
        }

        /// <summary>
        /// Unregisters an alarm
        /// </summary>
        /// <param name="alarm_id">The alarms id</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.unregisteralarm")]
        public void NetworkUnregisterAlarm(int alarm_id) {
            AlarmDbConnector.RemoveAlarm(alarm_id);  
        }

        /// <summary>
        /// Changes the name of an existing alarm
        /// </summary>
        /// <param name="alarm_id">The alarm to change</param>
        /// <param name="name">The new name</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.setalarmname")]
        public void NetworkSetAlarmName(int alarm_id, string name) {
            Dictionary<string, object> changes = new Dictionary<string, object>();
            changes.Add(AlarmDbConnector.NAME, name);
            AlarmDbConnector.ModifyAlarm(alarm_id, changes);
        }

        /// <summary>
        /// Changes the time of an existing alarm
        /// </summary>
        /// <param name="alarm_id">The alarm to change</param>
        /// <param name="hours">The hours of the time</param>
        /// <param name="minutes">The minutes of the time</param>
        /// <param name="seconds">The seconds of the time</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.setalarmtime")]
        public void NetworkSetAlarmTime(int alarm_id, int hours, int minutes, int seconds) {
            Dictionary<string, object> changes = new Dictionary<string, object>();
            changes.Add(AlarmDbConnector.HOURS, hours);
            changes.Add(AlarmDbConnector.MINUTES, minutes);
            changes.Add(AlarmDbConnector.SECONDS, seconds);
            AlarmDbConnector.ModifyAlarm(alarm_id, changes);
        }

        /// <summary>
        /// Changes the weekdays of an existing alarm
        /// </summary>
        /// <param name="alarm_id">The alarm to change</param>
        /// <param name="mon">If alarm is enabled on mon</param>
        /// <param name="tue">If alarm is enabled on tue</param>
        /// <param name="wed">If alarm is enabled on wed</param>
        /// <param name="thu">If alarm is enabled on thu</param>
        /// <param name="fri">If alarm is enabled on fri</param>
        /// <param name="sat">If alarm is enabled on sat</param>
        /// <param name="sun">If alarm is enabled on sun</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.setalarmweekdays")]
        public void NetworkSetAlarmWeekdays(int alarm_id, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun) {
            Dictionary<string, object> changes = new Dictionary<string, object>();
            changes.Add(AlarmDbConnector.MON, mon);
            changes.Add(AlarmDbConnector.TUE, tue);
            changes.Add(AlarmDbConnector.WED, wed);
            changes.Add(AlarmDbConnector.THU, thu);
            changes.Add(AlarmDbConnector.FRI, fri);
            changes.Add(AlarmDbConnector.SAT, sat);
            changes.Add(AlarmDbConnector.SUN, sun);
            AlarmDbConnector.ModifyAlarm(alarm_id, changes);
        }

        /// <summary>
        /// Enables or disables an existing alarm
        /// </summary>
        /// <param name="alarm_id">The alarm to change</param>
        /// <param name="enabled">Enabled or disabled</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.setalarmenabled")]
        public void NetworkSetAlarmEnabled(int alarm_id, bool enabled) {
            Dictionary<string, object> changes = new Dictionary<string, object>();
            changes.Add(AlarmDbConnector.ENABLED, enabled);
            AlarmDbConnector.ModifyAlarm(alarm_id, changes);
        }

        /// <summary>
        /// Returns all registered alarms
        /// </summary>                                        
        /// <returns>A IXP File containing all information</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.getalarms")]
        public IXPFile NetworkGetAlarms() {
            IXPFile response = new IXPFile();
            response.NetworkFunction = "com.projectgame.clock.alarm.getalarms";
                                                        
            List<int> alarms = AlarmDbConnector.GetAlarms();
            response.PutInfo("alarm_count", "" + alarms.Count);

            for(int i = 0; i < alarms.Count; i++) {
                response.PutInfo("alarm_" + i, "" + alarms[i]);
            }

            return response;
        }

        /// <summary>
        /// Returns all alarm data in one call
        /// </summary>
        /// <returns>An IXP File containing all information</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.getfullalarms")]
        public IXPFile NetworkGetFullAlarms(){
            IXPFile response = new IXPFile();
            response.NetworkFunction = "com.projectgame.clock.alarm.getfullalarms";

            List<int> alarmIDs = AlarmDbConnector.GetAlarms();

            response.PutInfo("alarm_count", "" + alarmIDs.Count);

            for(int i = 0; i < alarmIDs.Count; i++){
                Alarm alarm = AlarmDbConnector.GetAlarm(alarmIDs[i]);

                response.PutInfo("alarm_" + i + "_id", "" + alarm.ID);
                response.PutInfo("alarm_" + i + "_name", "" + alarm.Name);
                response.PutInfo("alarm_" + i + "_hours", "" + alarm.Hours);
                response.PutInfo("alarm_" + i + "_minutes", "" + alarm.Minutes);
                response.PutInfo("alarm_" + i + "_seconds", "" + alarm.Seconds);
                response.PutInfo("alarm_" + i + "_mon", alarm.Mon ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_tue", alarm.Tue ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_wed", alarm.Wed ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_thu", alarm.Thu ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_fri", alarm.Fri ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_sat", alarm.Sat ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_sun", alarm.Sun ? "TRUE" : "FALSE");
                response.PutInfo("alarm_" + i + "_enabled", alarm.Enabled ? "TRUE" : "FALSE");
            }

            return response;
        }

        /// <summary>
        /// Returns all important information about a alarm
        /// </summary>
        /// <param name="alarm_id">The alarms is</param>
        /// <returns>A IXP File containing all information</returns>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.getalarm")]
        public IXPFile NetworkGetAlarm(int alarm_id) {
            IXPFile response = new IXPFile();
            response.NetworkFunction = "com.projectgame.clock.alarm.getalarm";

            Alarm alarm = AlarmDbConnector.GetAlarm(alarm_id);

            response.PutInfo("id", "" + alarm.ID);
            response.PutInfo("name", "" + alarm.Name);
            response.PutInfo("hours", "" + alarm.Hours);
            response.PutInfo("minutes", "" + alarm.Minutes);
            response.PutInfo("seconds", "" + alarm.Seconds);
            response.PutInfo("mon", alarm.Mon ? "TRUE" : "FALSE");
            response.PutInfo("tue", alarm.Tue ? "TRUE" : "FALSE");
            response.PutInfo("wed", alarm.Wed ? "TRUE" : "FALSE");
            response.PutInfo("thu", alarm.Thu ? "TRUE" : "FALSE");
            response.PutInfo("fri", alarm.Fri ? "TRUE" : "FALSE");
            response.PutInfo("sat", alarm.Sat ? "TRUE" : "FALSE");
            response.PutInfo("sun", alarm.Sun ? "TRUE" : "FALSE");
            response.PutInfo("enabled", alarm.Enabled ? "TRUE" : "FALSE");

            return response;
        }

        /// <summary>
        /// Registers a client to the alarm service
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="function">The instances function</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.registertoalarmservice")]
        public void NetworkRegisterToAlarmService(Networking.ServerInstance instance, string function) {
            AlarmService.RegisterClient(instance, function);
        }

        /// <summary>
        /// Unregisters a client from the alarm service
        /// </summary>
        /// <param name="instance">The instance</param>
        [Plugin.NetworkFunction("com.projectgame.clock.alarm.unregisterfromalarmservice")]
        public void NetworkUnregisterFromAlarmService(Networking.ServerInstance instance) {
            AlarmService.UnregisterClient(instance);
        }
    }
}
