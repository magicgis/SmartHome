using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking {
    /// <summary>
    /// Handles communication with the server over ixp files
    /// </summary>
    public static class Request {

        /// <summary>
        /// A Request that will give no response
        /// </summary>
        /// <param name="file">The file to send</param>
        public static void NoResponseRequest(this Client client, IXPFile file) {
            client.Send(Encoding.UTF8.GetBytes(file.XML));
        }

        #region Simple Request

        private static string _simpleRequestResponse = null;
        private static string _simpleRequestRequest = null;

        /// <summary>
        /// A Request that gives back a single string
        /// </summary>
        /// <param name="file">The file to send</param>
        /// <returns>The servers response</returns>
        public static string SimpleRequest(this Client client, IXPFile file) {
            _simpleRequestRequest = file.NetworkFunction;
            client.MessageReceived += SimpleRequest_Client_MessageReceived;
            client.Send(Encoding.UTF8.GetBytes(file.XML));

            while (_simpleRequestResponse == null)
                Thread.Sleep(200);

            client.MessageReceived -= SimpleRequest_Client_MessageReceived;

            string response = _simpleRequestResponse;
            _simpleRequestResponse = null;
            return response;
        }

        private static void SimpleRequest_Client_MessageReceived(byte[] obj) {
            IXPFile response = new IXPFile(Encoding.UTF8.GetString(obj));

            if (!response.NetworkFunction.Equals(_simpleRequestRequest))
                return;

            _simpleRequestResponse = response.GetInfoValue("Response");
        }

        #endregion
        #region IXP Request
        private static IXPFile _ixpRequestResponse = null;
        private static string _ixpRequestRequest = null;

        /// <summary>
        /// A Request that gives back an IXP file
        /// </summary>
        /// <param name="file">The file to send</param>
        /// <returns>The servers response</returns>
        public static IXPFile IXPRequest(this Client client, IXPFile file) {
            _ixpRequestRequest = file.NetworkFunction;
            client.MessageReceived += IXPRequest_Client_MessageReceived;
            client.Send(Encoding.UTF8.GetBytes(file.XML));

            while (_ixpRequestResponse == null)
                Thread.Sleep(200);

            client.MessageReceived -= IXPRequest_Client_MessageReceived;

            IXPFile response = _ixpRequestResponse;
            _ixpRequestResponse = null;
            return response;
        }

        private static void IXPRequest_Client_MessageReceived(byte[] obj) {
            IXPFile file = new IXPFile(Encoding.UTF8.GetString(obj));

            if (!file.NetworkFunction.Equals(_ixpRequestRequest))
                return;

            _ixpRequestResponse = file;
        }
        #endregion
    }
}
