using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;                     

namespace Alarm {
    internal static class ExtentionMethods {
        public static IEnumerable<int> IndexesOf(this string haystack, string needle) {
            int lastIndex = 0;
            while (true) {
                int index = haystack.IndexOf(needle, lastIndex);
                if (index == -1) {
                    yield break;
                }
                yield return index;
                lastIndex = index + needle.Length;
            }
        } 
        
        public static Alarm AddAlarm(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.registeralarm";
            file.PutInfo("name", alarm.Name);
            file.PutInfo("hours", "" + alarm.Hours);
            file.PutInfo("minutes", "" + alarm.Minutes);
            file.PutInfo("seconds", "" + alarm.Seconds);
            file.PutInfo("mon", alarm.Mon ? "TRUE" : "FALSE");
            file.PutInfo("tue", alarm.Tue ? "TRUE" : "FALSE");
            file.PutInfo("wed", alarm.Wed ? "TRUE" : "FALSE");
            file.PutInfo("thu", alarm.Thu ? "TRUE" : "FALSE");
            file.PutInfo("fri", alarm.Fri ? "TRUE" : "FALSE");
            file.PutInfo("sat", alarm.Sat ? "TRUE" : "FALSE");
            file.PutInfo("sun", alarm.Sun ? "TRUE" : "FALSE");
            file.PutInfo("enabled", alarm.Enabled ? "TRUE" : "FALSE");
            int id = int.Parse(client.SimpleRequest(file));
            alarm.ID = id;
            return alarm;
        }

        public static void DeleteAlarm(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.unregisteralarm";
            file.PutInfo("alarm_id", "" + alarm.ID);
            client.NoResponseRequest(file);
        }

        public static void UpdateAlarm(this Client client, Alarm alarm) {
            client.UpdateAlarmName(alarm);
            client.UpdateAlarmTime(alarm);
            client.UpdateAlarmWeekdays(alarm);
            client.UpdateAlarmEnabled(alarm);
        }

        public static void UpdateAlarmName(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.setalarmname";
            file.PutInfo("alarm_id", "" + alarm.ID);
            file.PutInfo("name", alarm.Name);
            client.NoResponseRequest(file);
        }

        public static void UpdateAlarmTime(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.setalarmtime";
            file.PutInfo("alarm_id", "" + alarm.ID);
            file.PutInfo("hours", "" + alarm.Hours);
            file.PutInfo("minutes", "" + alarm.Minutes);
            file.PutInfo("seconds", "" + alarm.Seconds);
            client.NoResponseRequest(file);
        }

        public static void UpdateAlarmWeekdays(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.setalarmweekdays";
            file.PutInfo("alarm_id", "" + alarm.ID);
            file.PutInfo("mon", alarm.Mon ? "TRUE" : "FALSE");
            file.PutInfo("tue", alarm.Tue ? "TRUE" : "FALSE");
            file.PutInfo("wed", alarm.Wed ? "TRUE" : "FALSE");
            file.PutInfo("thu", alarm.Thu ? "TRUE" : "FALSE");
            file.PutInfo("fri", alarm.Fri ? "TRUE" : "FALSE");
            file.PutInfo("sat", alarm.Sat ? "TRUE" : "FALSE");
            file.PutInfo("sun", alarm.Sun ? "TRUE" : "FALSE");
            client.NoResponseRequest(file);
        }

        public static void UpdateAlarmEnabled(this Client client, Alarm alarm) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.setalarmenabled";
            file.PutInfo("alarm_id", "" + alarm.ID);
            file.PutInfo("enabled", alarm.Enabled ? "TRUE" : "FALSE");
            client.NoResponseRequest(file);
        }

        public static List<Alarm> GetAlarms(this Client client) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.getalarms";
            IXPFile response = client.IXPRequest(file);

            List<Alarm> alarms = new List<Alarm>();
            int count = int.Parse(response.GetInfoValue("alarm_count"));
            for(int i = 0; i < count; i++) {
                int id = int.Parse(response.GetInfoValue("alarm_" + i));
                alarms.Add(client.GetAlarm(id));
            }

            return alarms;
        }             
        
        public static Alarm GetAlarm(this Client client, int alarmId) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.getalarm";
            file.PutInfo("alarm_id", "" + alarmId);
            IXPFile response = client.IXPRequest(file);

            Alarm alarm = new Alarm();
            alarm.ID = int.Parse(response.GetInfoValue("id"));
            alarm.Name = response.GetInfoValue("name");
            alarm.Hours = int.Parse(response.GetInfoValue("hours"));
            alarm.Minutes = int.Parse(response.GetInfoValue("minutes"));
            alarm.Seconds = int.Parse(response.GetInfoValue("seconds"));
            alarm.Mon = response.GetInfoValue("mon").Equals("TRUE");
            alarm.Tue = response.GetInfoValue("tue").Equals("TRUE");
            alarm.Wed = response.GetInfoValue("wed").Equals("TRUE");
            alarm.Thu = response.GetInfoValue("thu").Equals("TRUE");
            alarm.Fri = response.GetInfoValue("fri").Equals("TRUE");
            alarm.Sat = response.GetInfoValue("sat").Equals("TRUE");
            alarm.Sun = response.GetInfoValue("sun").Equals("TRUE");
            alarm.Enabled = response.GetInfoValue("enabled").Equals("TRUE");

            return alarm;
        }                                    
        
        public static void RegisterToAlarmService(this Client client, string function) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.alarm.registertoalarmservice";
            file.PutInfo("function", function);
            client.NoResponseRequest(file);
        }     

        public static void RegisterToTimeService(this Client client, string function) {
            IXPFile file = new IXPFile();
            file.NetworkFunction = "com.projectgame.clock.clock.registertotimeservice";
            file.PutInfo("functionName", function);
            client.NoResponseRequest(file);
        }
    }
}
