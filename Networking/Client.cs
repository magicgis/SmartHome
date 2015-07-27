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
    /// A client manages the connection to the server
    /// </summary>
    public class Client {
        /// <summary>
        /// Gets called if this client gets disconnected from the server
        /// </summary>
        public event Action Disconnected;
        /// <summary>
        /// Gets called if a message from the server is received
        /// </summary>
        public event Action<byte[]> MessageReceived;

        private Socket _socket;
        private IPEndPoint _endpoint;
        private bool _running;
        private Thread _thread;
        private bool _receiving;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="address">The address to connect to</param>
        /// <param name="port">The port to use for the connection</param>
        public Client(string address, int port) {
            try {
                IPHostEntry hostInfo = Dns.GetHostByName(address);
                _endpoint = new IPEndPoint(hostInfo.AddressList[0], port);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _running = false;
                _thread = new Thread(new ThreadStart(Work));
            }catch(Exception e) {

            }
        }

        /// <summary>
        /// Connects the client to the server
        /// </summary>
        public bool Connect() {
            try {
                _running = true;
                _socket.Connect(_endpoint);
                _thread.Start();
            }catch(Exception e) {
                _running = false;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Disconnects the client from the server
        /// </summary>
        public void Disconnect() {
            if (_socket == null)
                return;
            while (_receiving) {
                Thread.Sleep(50);
            }

            _running = false;
            _socket.Shutdown(SocketShutdown.Both); 
            _socket.Close();
            _socket = null;
            Disconnected?.Invoke();
        }

        /// <summary>
        /// Sends a bunch of data to the server
        /// </summary>
        /// <param name="data">The data to send</param>
        public void Send(byte[] data) {
            if (_socket == null)
                return;
            if (data == null)
                return;

            while (_receiving) {
                Thread.Sleep(200);
            }

            try {
                _socket.Send(data);
            } catch (Exception e) {
                Disconnect();
            }
        }

        private void Work() {
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[1024];

            while (_running) {
                if (!_socket.IsConnected())
                    break;

                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);

                while (_socket.Available > 0) {
                    _receiving = true;
                    int read = _socket.Receive(buffer);
                    if (read <= 0)
                        continue;
                    stream.Write(buffer, 0, read);
                }
                _receiving = false;

                if (stream.Length > 0) {   
                    MessageReceived?.Invoke(stream.ToArray());
                } else {                   
                    Thread.Sleep(200);
                }
            }

            Disconnect();
            _thread.Abort();
        }
    }
}
