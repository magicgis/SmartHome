using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Plugin;

namespace Clock {
    internal delegate void AlarmChangedDelegate(Alarm alarm);

    internal static class AlarmDbConnector {
        public const string ID = "ID";
        public const string NAME = "Name";
        public const string HOURS = "Hours";
        public const string MINUTES = "Minutes";
        public const string SECONDS = "Seconds";
        public const string MON = "Mon";
        public const string TUE = "Tue";
        public const string WED = "Wed";
        public const string THU = "Thu";
        public const string FRI = "Fri";
        public const string SAT = "Sat";
        public const string SUN = "Sun";
        public const string ENABLED = "Enabled";

        public static event AlarmChangedDelegate AlarmAdded;
        public static event AlarmChangedDelegate AlarmModified;
        public static event AlarmChangedDelegate AlarmDeleted;

        private static MySqlConnection _connection;
        private static int _debugChannel;
        private static Semaphore _semaphore;
        
        public static void Init(MySqlConnection connection) {
            _debugChannel = Debug.AddChannel("com.projectgame.clock.alarmdbconnector");
            Debug.Log(_debugChannel, "Initializing...");
            _semaphore = new Semaphore();

            _connection = connection;

            _connection.Open();
            MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS `com_projectgame_clock_alarm`.`alarms` (" +
                                                "`ID` INT NOT NULL AUTO_INCREMENT," +
                                                "`Name` VARCHAR(50) NOT NULL," +
                                                "`Hours` INT NOT NULL," +
                                                "`Minutes` INT NOT NULL," +
                                                "`Seconds` INT NOT NULL," +
                                                "`Mon` TINYINT(5) NOT NULL," +
                                                "`Tue` TINYINT(5) NOT NULL," +
                                                "`Wed` TINYINT(5) NOT NULL," +
                                                "`Thu` TINYINT(5) NOT NULL," +
                                                "`Fri` TINYINT(5) NOT NULL," +
                                                "`Sat` TINYINT(5) NOT NULL," +
                                                "`Sun` TINYINT(5) NOT NULL," +
                                                "`Enabled` TINYINT(5) NOT NULL," +
                                                "PRIMARY KEY(`ID`))", _connection);
            cmd.ExecuteNonQuery();
            _connection.Close();
        } 

        public static int AddAlarm(string name,
            int hours, int minutes, int seconds,
            bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun,
            bool enabled) {
            _semaphore.Enqueue();

            int result = -1;

            try {
                _connection.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO alarms (Name, HOurs, Minutes, Seconds, Mon, Tue, Wed, Thu, Fri, Sat, Sun, Enabled) " +
                    "VALUES('" + name + "', " + hours + ", " + minutes + ", " + seconds + ", " + 
                    (mon ? 1 : 0) + ", " + (tue ? 1 : 0) + ", " + (wed ? 1 : 0) + ", " + (thu ? 1 : 0) + ", " + 
                    (fri ? 1 : 0) + ", " + (sat ? 1 : 0) + ", " + (sun ? 1 : 0) + ", " + 
                    (enabled ? 1 : 0) + "); " +
                    "SELECT LAST_INSERT_ID();", _connection);

                result = Convert.ToInt32(cmd.ExecuteScalar());
            } catch(Exception e) {
                Debug.Log(_debugChannel, "Error adding alarm: " + e.Message);
                result = -1;
            }

            _connection.Close();
            _semaphore.Dequeue();

            Alarm alarm = GetAlarm(result);
            AlarmAdded?.Invoke(alarm);
            return result;
        }

        public static void ModifyAlarm(int alarm_id, Dictionary<string, object> data) {
            _semaphore.Enqueue();
            _connection.Open();

            try {
                foreach(string coloumn in data.Keys) {
                    string command = "UPDATE alarms SET " + coloumn + "=";

                    if(data[coloumn] is string){
                        command += "'" + data[coloumn] + "'";
                    }else if(data[coloumn] is int) {
                        command += data[coloumn];
                    }else if(data[coloumn] is bool) {
                        command += (((bool)data[coloumn]) ? 1 : 0);
                    }

                    command += " WHERE ID=" + alarm_id;

                    MySqlCommand cmd = new MySqlCommand(command, _connection);
                    cmd.ExecuteNonQuery();
                }
            }catch(Exception e) {
                Debug.Log(_debugChannel, "Could not modify alarm: " + e.Message);
            }

            _connection.Close();
            _semaphore.Dequeue();

            Alarm alarm = GetAlarm(alarm_id);
            AlarmModified?.Invoke(alarm);
        }

        public static void RemoveAlarm(int alarm_id) {
            Alarm alarm = GetAlarm(alarm_id);

            _semaphore.Enqueue();
            _connection.Open();

            try {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM alarms WHERE ID=" + alarm_id, _connection);
                cmd.ExecuteNonQuery();
            } catch (Exception e) {
                Debug.Log(_debugChannel, "Could not delete alarm: " + e.Message);
            }

            _connection.Close();
            _semaphore.Dequeue();

            AlarmDeleted?.Invoke(alarm);
        }

        public static List<int> GetAlarms() {
            List<int> alarms = new List<int>();
            _semaphore.Enqueue();

            try {
                _connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT ID FROM alarms", _connection);
                    
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    alarms.Add(reader.GetInt32("ID"));
                }

                reader.Close();
                reader.Dispose();
            }catch(Exception e) {
                Debug.Log(_debugChannel, "Could not fetch alarms: " + e.Message);
                alarms = new List<int>();
            }

            _connection.Close();
            _semaphore.Dequeue();
            return alarms;
        }

        public static Alarm GetAlarm(int alarm_id) {
            Alarm alarm = new Alarm(-1, "", -1, -1, -1, false, false, false, false, false, false, false, false);
            _semaphore.Enqueue();
            _connection.Open();

            try {
                MySqlCommand cmd = new MySqlCommand("SELECT ID, Name, Hours, Minutes, Seconds, Mon, Tue, Wed, Thu, Fri, Sat, Sun, Enabled FROM alarms WHERE ID=" + alarm_id + "", _connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    int id = reader.GetInt32("ID");
                    string name = reader.GetString("Name");
                    int hours = reader.GetInt32("Hours");
                    int minutes = reader.GetInt32("Minutes");
                    int seconds = reader.GetInt32("Seconds");
                    bool mon = reader.GetInt16("Mon") == 1;
                    bool tue = reader.GetInt16("Tue") == 1;
                    bool wed = reader.GetInt16("Wed") == 1;
                    bool thu = reader.GetInt16("Thu") == 1;
                    bool fri = reader.GetInt16("Fri") == 1;
                    bool sat = reader.GetInt16("Sat") == 1;
                    bool sun = reader.GetInt16("Sun") == 1;
                    bool enabled = reader.GetInt16("Enabled") == 1;

                    alarm = new Alarm(id, name, hours, minutes, seconds, mon, tue, wed, thu, fri, sat, sun, enabled);
                }

                reader.Close();
                reader.Dispose();
            } catch(Exception e) {
                Debug.Log(_debugChannel, "Could not fetch alarm: " + e.Message);
                alarm = new Alarm(-1, "", -1, -1, -1, false, false, false, false, false, false, false, false);
            }

            _connection.Close();
            _semaphore.Dequeue();
            return alarm;
        }
    }         
}
