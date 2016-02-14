using System;
using System.Collections.Generic;
using Networking;
using Networking;

namespace Clock{
	public class PTimer : Plugin.Plugin{
		/// <summary>
		/// <see cref="Plugin.Plugin.Name"/>
		/// </summary>
		public override string Name {
			get {
				return "com.projectgame.clock.timer";
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
			TimerDbConnector.Init(GetDatabaseConnection);
			TimerService.Start();
		}

		/// <summary>
		/// <see cref="Plugin.Plugin.OnPluginUnload"/>
		/// </summary>
		public override void OnPluginUnload() {
			TimerService.Stop();
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.registertimer")]
		public int NetworkRegisterTimer(
			string name,
			int hours, int minutes, int seconds) {
			return TimerDbConnector.AddTimer(name, hours, minutes, seconds);
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.unregistertimer")]
		public void NetworkUnregisterTimer(int timer_id) {
			TimerDbConnector.RemoveTimer(timer_id);  
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.gettimers")]
		public IXPFile NetworkGetTimers() {
			IXPFile response = new IXPFile();
			response.NetworkFunction = "com.projectgame.clock.timer.gettimers";

			List<int> timers = TimerDbConnector.GetTimers();
			response.PutInfo("timer_count", "" + timers.Count);

			for(int i = 0; i < timers.Count; i++) {
				response.PutInfo("timer_" + i, "" + timers[i]);
			}

			return response;
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.gettimer")]
		public IXPFile NetworkGetTimer(int timer_id) {
			IXPFile response = new IXPFile();
			response.NetworkFunction = "com.projectgame.clock.timer.gettimer";

			Timer timer = TimerDbConnector.GetTimer(timer_id);

			response.PutInfo("id", "" + timer.ID);
			response.PutInfo("name", "" + timer.Name);
			response.PutInfo("hours", "" + timer.Hours);
			response.PutInfo("minutes", "" + timer.Minutes);
			response.PutInfo("seconds", "" + timer.Seconds);

			return response;
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.starttimer")]
		public void NetworkStartTime(int timer_id){
			TimerService.StartTimer (TimerDbConnector.GetTimer (timer_id));
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.stoptimer")]
		public void NetworkStopTimer(int timer_id){
			TimerService.StopTimer (TimerDbConnector.GetTimer (timer_id));
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.registertotimerservice")]
		public void NetworkRegisterToTimerService(Networking.ServerInstance instance,
			string startedFunction, string stoppedFunction, string updatedFunction, string calledFunction) {
			TimerService.RegisterClient (instance, startedFunction, stoppedFunction, updatedFunction, calledFunction);
		}

		[Plugin.NetworkFunction("com.projectgame.clock.timer.unregisterfromtimerservice")]
		public void NetworkUnregisterFromTimerService(Networking.ServerInstance instance) {
			TimerService.UnregisterClient(instance);
		}
	}
}

