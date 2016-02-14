using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Plugin;

namespace Clock {
	internal delegate void TimerChangedDelegate(Timer alarm);

	internal static class TimerDbConnector {
		public static event TimerChangedDelegate TimerAdded;
		public static event TimerChangedDelegate TimerModified;
		public static event TimerChangedDelegate TimerDeleted;

		private static MySqlConnection _connection;
		private static int _debugChannel;
		private static Semaphore _semaphore;

		public static void Init(MySqlConnection connection) {
			_debugChannel = Debug.AddChannel("com.projectgame.clock.timerdbconnector");
			Debug.Log(_debugChannel, "Initializing...");
			_semaphore = new Semaphore();

			_connection = connection;

			_connection.Open();
			MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS `com_projectgame_clock_alarm`.`timers` (" +
				"`ID` INT NOT NULL AUTO_INCREMENT," +
				"`Name` VARCHAR(50) NOT NULL," +
				"`Hours` INT NOT NULL," +
				"`Minutes` INT NOT NULL," +
				"`Seconds` INT NOT NULL," +
				"PRIMARY KEY(`ID`))", _connection);
			cmd.ExecuteNonQuery();
			_connection.Close();
		} 

		public static int AddTimer(string name,
			int hours, int minutes, int seconds) {
			_semaphore.Enqueue();

			int result = -1;

			try {
				_connection.Open();

				MySqlCommand cmd = new MySqlCommand(
					"INSERT INTO timers (Name, Hours, Minutes, Seconds) " +
					"VALUES('" + name + "', " + hours + ", " + minutes + ", " + seconds + "); " +
					"SELECT LAST_INSERT_ID();", _connection);

				result = Convert.ToInt32(cmd.ExecuteScalar());
			} catch(Exception e) {
				Debug.Log(_debugChannel, "Error adding timer: " + e.Message);
				result = -1;
			}

			_connection.Close();
			_semaphore.Dequeue();

			Timer timer = GetTimer(result);
			TimerAdded?.Invoke(timer);
			return result;
		}

		public static void ModifyAlarm(int timer_id, Dictionary<string, object> data) {
			_semaphore.Enqueue();
			_connection.Open();

			try {
				foreach(string coloumn in data.Keys) {
					string command = "UPDATE timers SET " + coloumn + "=";

					if(data[coloumn] is string){
						command += "'" + data[coloumn] + "'";
					}else if(data[coloumn] is int) {
						command += data[coloumn];
					}else if(data[coloumn] is bool) {
						command += (((bool)data[coloumn]) ? 1 : 0);
					}

					command += " WHERE ID=" + timer_id;

					MySqlCommand cmd = new MySqlCommand(command, _connection);
					cmd.ExecuteNonQuery();
				}
			}catch(Exception e) {
				Debug.Log(_debugChannel, "Could not modify timer: " + e.Message);
			}

			_connection.Close();
			_semaphore.Dequeue();

			Timer timer = GetTimer(timer_id);
			TimerModified?.Invoke(timer);
		}

		public static void RemoveTimer(int timer_id) {
			Timer timer = GetTimer(timer_id);

			_semaphore.Enqueue();
			_connection.Open();

			try {
				MySqlCommand cmd = new MySqlCommand("DELETE FROM timers WHERE ID=" + timer_id, _connection);
				cmd.ExecuteNonQuery();
			} catch (Exception e) {
				Debug.Log(_debugChannel, "Could not delete timer: " + e.Message);
			}

			_connection.Close();
			_semaphore.Dequeue();

			TimerDeleted?.Invoke(timer);
		}

		public static List<int> GetTimers() {
			List<int> timers = new List<int>();
			_semaphore.Enqueue();

			try {
				_connection.Open();
				MySqlCommand cmd = new MySqlCommand("SELECT ID FROM timers", _connection);

				MySqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read()) {
					timers.Add(reader.GetInt32("ID"));
				}

				reader.Close();
				reader.Dispose();
			}catch(Exception e) {
				Debug.Log(_debugChannel, "Could not fetch alarms: " + e.Message);
				timers = new List<int>();
			}

			_connection.Close();
			_semaphore.Dequeue();
			return timers;
		}

		public static Timer GetTimer(int timer_id) {
			Timer timer = new Timer(-1, "", -1, -1, -1);
			_semaphore.Enqueue();
			_connection.Open();

			try {
				MySqlCommand cmd = new MySqlCommand("SELECT ID, Name, Hours, Minutes, Seconds FROM timers WHERE ID=" + timer_id + "", _connection);
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read()) {
					int id = reader.GetInt32("ID");
					string name = reader.GetString("Name");
					int hours = reader.GetInt32("Hours");
					int minutes = reader.GetInt32("Minutes");
					int seconds = reader.GetInt32("Seconds");

					timer = new Timer(id, name, hours, minutes, seconds);
				}

				reader.Close();
				reader.Dispose();
			} catch(Exception e) {
				Debug.Log(_debugChannel, "Could not fetch timer: " + e.Message);
				timer = new Timer(-1, "", -1, -1, -1);
			}

			_connection.Close();
			_semaphore.Dequeue();
			return timer;
		}
	}         
}
