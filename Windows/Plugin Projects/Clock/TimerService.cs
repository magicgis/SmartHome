using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Networking;
using Plugin;
using System.Threading.Tasks;

namespace Clock {
	/// <summary>
	/// Handles all active alarms
	/// </summary>
	public static class TimerService {
		private enum Function{
			STARTED,
			STOPPED,
			UPDATED,
			CALLED
		}

		private static Dictionary<ServerInstance, Dictionary<Function, string>> _clients;
		private static int _debugChannel;
		private static Thread _thread;
		private static bool _running;
		private static Plugin.Semaphore _clientSemaphore;
		private static Plugin.Semaphore _timerSemaphore;

		private static Dictionary<Timer, TimeSpan> _timers;

		/// <summary>
		/// Starts the service
		/// </summary>
		public static void Start() {
			_debugChannel = Debug.AddChannel("com.projectgame.clock.timerservice");
			Debug.Log(_debugChannel, "Initializing service...");
			ServerInstance.InstanceClosed += ServerInstance_InstanceClosed;
			_clients = new Dictionary<ServerInstance, Dictionary<Function, string>> ();
			_thread = new Thread(new ThreadStart(Work));
			_running = true;
			_clientSemaphore = new Plugin.Semaphore();
			_timerSemaphore = new Plugin.Semaphore();
			_timers = new Dictionary<Timer, TimeSpan> ();

			Debug.Log(_debugChannel, "Timer loaded: " + _timers.Count);

			_thread.Start();
		}

		public static void StartTimer(Timer timer){
			_timers.Add (timer, new TimeSpan(timer.Hours, timer.Minutes, timer.Seconds));

			foreach(ServerInstance instance in _clients.Keys){
				IXPFile file = new IXPFile();
				file.NetworkFunction = _clients[instance][Function.STARTED];
				file.PutInfo("timer_id", "" + timer.ID);
				file.PutInfo("name", "" + timer.Name);

				instance.Send(Encoding.UTF8.GetBytes(file.XML));
			}
		}

		public static void StopTimer(Timer timer){
			_timers.Remove (timer);

			foreach(ServerInstance instance in _clients.Keys){
				IXPFile file = new IXPFile();
				file.NetworkFunction = _clients[instance][Function.STOPPED];
				file.PutInfo("timer_id", "" + timer.ID);
				file.PutInfo("name", "" + timer.Name);

				instance.Send(Encoding.UTF8.GetBytes(file.XML));
			}
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
		public static void RegisterClient(ServerInstance instance, string startedF, string stoppedF, string updatedF, string calledF) {
			_clientSemaphore.Enqueue();

			Debug.Log(_debugChannel, "Registering client " + instance.IP);

			if (_clients.ContainsKey (instance))
				_clients.Remove (instance);

			_clients.Add(instance, new Dictionary<Function, string>());
			_clients [instance].Add (Function.STARTED, startedF);
			_clients [instance].Add (Function.STOPPED, stoppedF);
			_clients [instance].Add (Function.UPDATED, updatedF);
			_clients [instance].Add (Function.CALLED, calledF);

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

				_timerSemaphore.Enqueue();

				DateTime now = DateTime.Now;

				foreach(Timer timer in _timers.Keys) {
					if(timer == null) {
						Debug.Log(_debugChannel, "Timer is null. This should not happen");
						continue;
					}

					_timers [timer].Subtract (new TimeSpan (0, 0, 1));

					foreach(ServerInstance instance in _clients.Keys){
						IXPFile file = new IXPFile();
						file.NetworkFunction = _clients[instance][Function.UPDATED];
						file.PutInfo("timer_id", "" + timer.ID);
						file.PutInfo("name", "" + timer.Name);
						file.PutInfo ("hours", "" + _timers[timer].Hours);
						file.PutInfo ("minutes", "" + _timers[timer].Minutes);
						file.PutInfo ("seconds", "" + _timers[timer].Seconds);

						instance.Send(Encoding.UTF8.GetBytes(file.XML));
					}

					CallTimer(timer);
				}

				_timerSemaphore.Dequeue();
			}  
		}

		private static void CallTimer(Timer timer) {
			_clientSemaphore.Enqueue();

			Debug.Log(_debugChannel, "Calling timer " + timer.Name);

			foreach(ServerInstance instance in _clients.Keys){
				IXPFile file = new IXPFile();
				file.NetworkFunction = _clients[instance][Function.CALLED];
				file.PutInfo("timer_id", "" + timer.ID);
				file.PutInfo("name", "" + timer.Name);

				instance.Send(Encoding.UTF8.GetBytes(file.XML));
			}

			_clientSemaphore.Dequeue();
		}
	}
}
