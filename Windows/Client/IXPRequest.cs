using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Networking;

namespace Client {
    internal class IXPRequest {
        private Networking.Client _client;
        private IXPFile _request;
        private Action<string> _responseDelegate;
        private bool _received;

        public IXPRequest(Networking.Client client, IXPFile request, Action<string> responseDelegate) {
            _request = request;
            _responseDelegate = responseDelegate;
            _client = client;
            Thread thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }

        private void Work() {
            _client.Send(Encoding.UTF8.GetBytes(_request.XML));

            _client.MessageReceived += _client_MessageReceived;

            while (!_received)
                Thread.Sleep(200);

            _client.MessageReceived -= _client_MessageReceived;    
        }

        private void _client_MessageReceived(byte[] obj) {
            IXPFile ixp = new IXPFile(Encoding.UTF8.GetString(obj));
            if (!ixp.NetworkFunction.Equals(_request.NetworkFunction))
                return;

            _received = true;

            _responseDelegate(ixp.GetInfoValue("Response"));
        }
    }
}
