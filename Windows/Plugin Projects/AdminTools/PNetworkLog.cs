using System;
using System.Collections.Generic;
using Plugin;
using Networking;

namespace AdminTools{
	public class PNetworkLog : Plugin.Plugin{
		/// <summary>
		/// <see cref="Plugin.Plugin.Name"/>
		/// </summary>
		public override string Name {
			get {
				return "com.projectgame.admintools.networklogging";
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

		private int _debugChannel;
		private Dictionary<ServerInstance, int> _clients;

		public override void OnPluginLoad (){
			_debugChannel = Debug.AddChannel ("com.projectgame.admintools.networklogging");
			_clients = new Dictionary<ServerInstance, int> ();
			ServerInstance.InstanceClosed += ServerInstance_InstanceClosed;
		}

		void ServerInstance_InstanceClosed (ServerInstance obj){
			if(_clients.ContainsKey (obj))
				NetworkUnregisterLogger (obj);
		}

		[Plugin.NetworkFunction("com.projectgame.admintools.networklogging.registerlogger")]
		public void NetworkRegisterLogger(ServerInstance instance, string name) {
			if(_clients.ContainsKey (instance)){
				Debug.Log (_debugChannel, "Client " + instance.IP + " already owns a channel");
				return;
			}

			_clients.Add (instance, Debug.AddChannel ("Network | " + name));
		}

		[Plugin.NetworkFunction("com.projectgame.admintools.networklogging.unregisterlogger")]
		public void NetworkUnregisterLogger(ServerInstance instance) {
			if(!_clients.ContainsKey (instance)){
				Debug.Log (_debugChannel, "Client " + instance.IP + " has no channel");
				return;
			}

			Debug.DeleteChannel (_clients[instance]);
			_clients.Remove (instance);
		}

		[Plugin.NetworkFunction("com.projectgame.admintools.networklogging.log")]
		public void NetworkLog(ServerInstance instance, string message) {
			if(!_clients.ContainsKey (instance)){
				Debug.Log (_debugChannel, "Client " + instance.IP + " has no channel");
				return;
			}

			Debug.Log (_clients[instance], message);
		}
	}
}

