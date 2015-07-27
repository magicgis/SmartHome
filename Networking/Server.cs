using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Networking {
    /// <summary>
    /// A Server manages client connections
    /// </summary>
    public class Server {
        /// <summary>
        /// Gets called if a client receives a message. Forwards <see cref="ServerInstance.MessageReceived"/>
        /// </summary>
        public static event Action<ServerInstance, byte[]> MessageReceived;
        /// <summary>
        /// Gets called if a client gets connected
        /// </summary>
        public static event Action<ServerInstance> ClientConnected;
        /// <summary>
        /// Gets called if a client gets disconnected
        /// </summary>
        public static event Action<ServerInstance> ClientDisconnected;

        private int _port;
        private TcpListener _listener;
        private int _sleepTime;
        private Thread _thread;
        private List<ServerInstance> _clients;
        private bool _running;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Server() {
            _port = 10250;
            _listener = new TcpListener(IPAddress.Any, _port);
            _sleepTime = 50;
            _clients = new List<ServerInstance>();
            _running = false;
            ServerInstance.MessageReceived += ServerInstance_MessageReceived;
            ServerInstance.InstanceClosed += ServerInstance_InstanceClosed;
        }

        /// <summary>
        /// Closes the connection to one client
        /// </summary>
        /// <param name="instance">The connection to close</param>
        public void Close(ServerInstance instance) {
            instance.Close();
        }

        private void ServerInstance_InstanceClosed(ServerInstance obj) {
            _clients.Remove(obj);
            ClientDisconnected?.Invoke(obj);
        }

        private void ServerInstance_MessageReceived(ServerInstance arg1, byte[] arg2) {
            MessageReceived?.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        public void Start() {
            _running = true;
            _thread = new Thread(new ThreadStart(Work));
            _thread.Start();
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public void Stop() {
            _running = false;
        }

        private void Work() {
            try {
                _listener.Start();

                while (_running) {
                    while (!_listener.Pending()) {
                        if (!_running)
                            break;
                        Thread.Sleep(_sleepTime);
                    }

                    if (!_running)
                        break;

                    Socket newSocket = _listener.AcceptSocket();

                    if (newSocket == null)
                        continue;

                    ServerInstance newConnection = new ServerInstance(newSocket);
                    _clients.Add(newConnection);

                    ClientConnected?.Invoke(newConnection);
                }

                foreach (ServerInstance instance in _clients)
                    instance.Close();

                _listener.Stop();
                _thread.Abort();
            } catch (Exception e) {

            }
        }
    }
}
