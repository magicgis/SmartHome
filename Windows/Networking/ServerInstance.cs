using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Networking {
    /// <summary>
    /// A server instance is a connected client on the server side
    /// </summary>
    public class ServerInstance {
        /// <summary>
        /// Gets called if the instance receives data from the client
        /// </summary>
        public static event Action<ServerInstance, byte[]> MessageReceived;
        /// <summary>
        /// Gets called if the instances connection is closed
        /// </summary>
        public static event Action<ServerInstance> InstanceClosed;

        private int _sleepTime;
        private Thread _thread;
        private Socket _socket;
        private bool _receiving;
        private int _timeout;
        private string _ip;

        /// <summary>
        /// The ip of the connected client
        /// </summary>
        public string IP {
            get {
                return _ip;
            }
        }

        /// <summary>
        /// States if the instance is currently connected to the server
        /// </summary>
        public bool Connected {
            get {
                return _socket.IsConnected();
            }
        }

        /// <summary>
        /// Creates a new connection
        /// </summary>
        /// <param name="socket">The socket that handles the connection</param>
        public ServerInstance(Socket socket) {
            _sleepTime = 50;
            _socket = socket;
            _receiving = false;
            _timeout = 1 * 60 * 60 * 1000; //1h
            _ip = _socket.RemoteEndPoint.ToString();
            _thread = new Thread(new ThreadStart(Work));
            _thread.Start();
        }

        private void Work() {
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[1024];
            int timeout = 0;
            
            while(timeout < _timeout / _sleepTime) {
                if (_socket == null)
                    break;
                if (!_socket.IsConnected())
                    break;

                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);

                while(_socket.Available > 0) {
                    _receiving = true;
                    int read = _socket.Receive(buffer);
                    if (read <= 0)
                        continue;
                    stream.Write(buffer, 0, read);
                }

                _receiving = false;

                if (stream.Length > 0) {
                    timeout = 0;
                    MessageReceived?.Invoke(this, stream.ToArray());
                } else {
                    timeout++;
                    Thread.Sleep(_sleepTime);
                }
            }    

            Close();
        }

        /// <summary>
        /// Sends a bunch of data to the client
        /// </summary>
        /// <param name="data">The data to send</param>
        public void Send(byte[] data) {
            if (_socket == null)
                return;
            if (data == null)
                return;

            while (_receiving) {
                Thread.Sleep(_sleepTime);
            }

            try {
                _socket.Send(data);
            }catch(Exception e) {
                Close();
            }
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        public void Close() {
            if (_socket == null)
                return;
            while (_receiving)
                Thread.Sleep(_sleepTime);

            try {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }catch(Exception e)
            {
                
            }
            _socket = null;

            InstanceClosed?.Invoke(this);
        }
    }
}
