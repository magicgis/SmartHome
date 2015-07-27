using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin;
using Networking;
using System.Threading;
using IXP;

namespace Clock {
    /// <summary>
    /// Handles automatic updating of time on clients
    /// </summary>
    public static class TimeService {
        private static Dictionary<ServerInstance, string> _clients;
        private static int _debugChannel;
        private static Thread _thread;
        private static bool _running;
        private static Plugin.Semaphore _semaphore;

        /// <summary>
        /// Starts the service
        /// </summary>
        public static void Start() {
            _debugChannel = Debug.AddChannel("com.projectgame.clock.timeservice");
            Debug.Log(_debugChannel, "Starting Service...");
            _clients = new Dictionary<ServerInstance, string>();
            _running = true;
            _semaphore = new Plugin.Semaphore();
            ServerInstance.InstanceClosed += ServerInstance_InstanceClosed;
            _thread = new Thread(new ThreadStart(Work));
            _thread.Start();
        }

        private static void ServerInstance_InstanceClosed(ServerInstance obj) {
            Unregister(obj);
        }

        /// <summary>
        /// Stops the service
        /// </summary>
        public static void Stop() {
            Debug.Log(_debugChannel, "Stopping Service...");
            ServerInstance.InstanceClosed -= ServerInstance_InstanceClosed;
            _running = false;
        }

        /// <summary>
        /// Registers a new client
        /// </summary>
        /// <param name="instance">The client</param>
        /// <param name="function">The clients function</param>
        public static void Register(ServerInstance instance, string function) {
            _semaphore.Enqueue();

            if (_clients.ContainsKey(instance))
                _clients[instance] = function;
            else
                _clients.Add(instance, function);

            Debug.Log(_debugChannel, "Registered client " + instance.IP + " with function " + function);

            _semaphore.Dequeue();
        }

        /// <summary>
        /// Unregisters a client
        /// </summary>
        /// <param name="instance">The client</param>
        public static void Unregister(ServerInstance instance) {
            if (!_clients.ContainsKey(instance))
                return;
            
            _semaphore.Enqueue();


            _clients.Remove(instance);

            Debug.Log(_debugChannel, "Unregistered client " + instance.IP);

            _semaphore.Dequeue();
        }

        private static void Work() {
            while (_running) {
                Thread.Sleep(1000);

                _semaphore.Enqueue();

                foreach(ServerInstance client in _clients.Keys) {
                    IXPFile file = new IXPFile();
                    file.NetworkFunction = _clients[client];
                    file.PutInfo("hours", "" + DateTime.Now.Hour);
                    file.PutInfo("minutes", "" + DateTime.Now.Minute);
                    file.PutInfo("seconds", "" + DateTime.Now.Second);

                    client.Send(Encoding.UTF8.GetBytes(file.XML)); 
                }

                _semaphore.Dequeue();
            } 
        }
    }
}
