using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Networking {
    /// <summary>
    /// Base Class for Restful Requests
    /// </summary>
    public abstract class HttpRESTRequest {      
        /// <summary>
        /// The requests url
        /// </summary>
        protected string _url { get; private set; }    
        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private Stream _responseStream;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The requests url</param>
        public HttpRESTRequest(string url) {
            _url = url;          
        }

        /// <summary>
        /// Creates the request
        /// </summary>
        /// <returns>A httpwebrequest</returns>
        protected abstract HttpWebRequest CreateRequest();

        /// <summary>
        /// Sends the requests
        /// </summary>
        public void Send() {
            _request = CreateRequest();
            _response = (HttpWebResponse)_request.GetResponse();
            _responseStream = _response.GetResponseStream();
        }

        /// <summary>
        /// Reads the requests response
        /// </summary>
        /// <returns>UTF-8 Response</returns>
        public string Read() {
            string result = "";
            StreamReader sr = new StreamReader(_responseStream);
            result = sr.ReadToEnd();

            _responseStream.Close();
            _responseStream.Dispose();
            _response.Close();
            _response.Dispose();

            return result;
        }
    }
}
