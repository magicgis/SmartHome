using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Networking;
using IXP;
using Plugin;

namespace Clock {
    /// <summary>
    /// Handles all active alarms
    /// </summary>
    public static class AlarmService {
        private static Dictionary<ServerInstance, string> _clients;
        private static int _debugChannel;
        private static Thread _thread;
        private static bool _running;
        private static Plugin.Semaphore _clientSemaphore;
        private static Plugin.Semaphore _alarmSemaphore;

        private static List<Alarm> _alarms;

        /// <summary>
        /// Starts the service
        /// </summary>
        public static void Start() {
            _debugChannel = Debug.AddChannel("com.projectgame.clock.alarmservice");
            Debug.Log(_debugChannel, "Initializing service...");
            AlarmDbConnector.AlarmAdded += AlarmDbConnector_AlarmAdded;
            AlarmDbConnector.AlarmModified += AlarmDbConnector_AlarmModified;
            AlarmDbConnector.AlarmDeleted += AlarmDbConnector_AlarmDeleted;
            ServerInstance.InstanceClosed += ServerInstance_InstanceClosed;
            _clients = new Dictionary<ServerInstance, string>();
            _thread = new Thread(new ThreadStart(Work));
            _running = true;
            _clientSemaphore = new Plugin.Semaphore();
            _alarmSemaphore = new Plugin.Semaphore();
            _alarms = new List<Alarm>();

            foreach (int alarm in AlarmDbConnector.GetAlarms())
                AlarmDbConnector_AlarmAdded(AlarmDbConnector.GetAlarm(alarm));

            Debug.Log(_debugChannel, "Alarms loaded: " + _alarms.Count);

            _thread.Start();
        }

        private static void AlarmDbConnector_AlarmDeleted(Alarm alarm) {
            _alarmSemaphore.Enqueue();

            Alarm tmp = null;
            if ((tmp = _alarms.FirstOrDefault(a => a.ID == alarm.ID)) != null) {
                _alarms.Remove(tmp);
                Debug.Log(_debugChannel, "Removed alarm " + tmp.Name);
            }

            _alarmSemaphore.Dequeue();
        }

        private static void AlarmDbConnector_AlarmModified(Alarm alarm) {
            _alarmSemaphore.Enqueue();

            Alarm tmp = null;
            if((tmp = _alarms.FirstOrDefault(a => a.ID == alarm.ID)) != null) {
                if (!alarm.Enabled) {
                    _alarms.Remove(tmp);
                    Debug.Log(_debugChannel, "Disabled alarm " + alarm.Name);
                } else {
                    tmp.Name = alarm.Name;
                    tmp.Hours = alarm.Hours;
                    tmp.Minutes = alarm.Minutes;
                    tmp.Seconds = alarm.Seconds;
                    tmp.Mon = alarm.Mon;
                    tmp.Tue = alarm.Tue;
                    tmp.Wed = alarm.Wed;
                    tmp.Thu = alarm.Thu;
                    tmp.Fri = alarm.Fri;
                    tmp.Sat = alarm.Sat;
                    tmp.Sun = alarm.Sun;
                    Debug.Log(_debugChannel, "Updated alarm " + alarm.Name);
                }
            } else {
                if (alarm.Enabled) {
                    _alarms.Add(alarm);
                    Debug.Log(_debugChannel, "Enabled alarm " + alarm.Name);
                }
            }

            _alarmSemaphore.Dequeue();
        }

        private static void AlarmDbConnector_AlarmAdded(Alarm alarm) {
            _alarmSemaphore.Enqueue();

            if (alarm.Enabled) {
                _alarms.Add(alarm);
                Debug.Log(_debugChannel, "Added alarm " + alarm.Name);
            }

            _alarmSemaphore.Dequeue();
        }

        private static void ServerInstance_InstanceClosed(ServerInstance obj) {
            UnregisterClient(obj);
        }

        /// <summary>
        /// Stops the service
        /// </summary>
        public static void Stop() {
            Debug.Log(_debugChannel, "Stopping service...");
            _running = false;
        }

        /// <summary>
        /// Registers a client
        /// </summary>
        /// <param name="instance">The client</param>
        /// <param name="function">The clients function to call</param>
        public static void RegisterClient(ServerInstance instance, string function) {
            _clientSemaphore.Enqueue();

            Debug.Log(_debugChannel, "Registering client " + instance.IP);

            if (_clients.ContainsKey(instance))
                _clients[instance] = function;
            else
                _clients.Add(instance, function);

            _clientSemaphore.Dequeue();
        }

        /// <summary>
        /// Unregisters a client
        /// </summary>
        /// <param name="instance">The client</param>
        public static void UnregisterClient(ServerInstance instance) {
            if (!_clients.ContainsKey(instance))
                return;

            _clientSemaphore.Enqueue();

            _clients.Remove(instance);
            Debug.Log(_debugChannel, "Unregistering client " + instance.IP);

            _clientSemaphore.Dequeue();
        }

        private static void Work() {
            while (_running) {
                Thread.Sleep(1000); ;

                _alarmSemaphore.Enqueue();

                DateTime now = DateTime.Now;

                foreach(Alarm alarm in _alarms) {
                    if(alarm == null) {
                        Debug.Log(_debugChannel, "Alarm is null. This should not happen");
                        continue;
                    }

                    if ((now.DayOfWeek == DayOfWeek.Monday) && !alarm.Mon ||
                       (now.DayOfWeek == DayOfWeek.Tuesday) && !alarm.Tue ||
                       (now.DayOfWeek == DayOfWeek.Wednesday) && !alarm.Wed ||
                       (now.DayOfWeek == DayOfWeek.Thursday) && !alarm.Thu ||
                       (now.DayOfWeek == DayOfWeek.Friday) && !alarm.Fri ||
                       (now.DayOfWeek == DayOfWeek.Saturday) && !alarm.Sat ||
                       (now.DayOfWeek == DayOfWeek.Sunday) && !alarm.Sun)
                        continue;

                    if (alarm.Hours != now.Hour)
                        continue;
                    if (alarm.Minutes != now.Minute)
                        continue;
                    if (alarm.Seconds != now.Second)
                        continue;

                    CallAlarm(alarm);
                }

                _alarmSemaphore.Dequeue();
            }  
        }

        private static void CallAlarm(Alarm alarm) {
            _clientSemaphore.Enqueue();

            Debug.Log(_debugChannel, "Calling alarm " + alarm.Name);

            foreach(ServerInstance client in _clients.Keys) {
                IXPFile file = new IXPFile();
                file.NetworkFunction = _clients[client];
                file.PutInfo("alarm_id", "" + alarm.ID);

                client.Send(Encoding.UTF8.GetBytes(file.XML));
            }

            _clientSemaphore.Dequeue();
        }
    }
}
