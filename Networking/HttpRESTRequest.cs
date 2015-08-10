using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Networking {
    public abstract class HttpRESTRequest {
        protected string _url { get; private set; }    
        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private Stream _responseStream;

        public HttpRESTRequest(string url) {
            _url = url;          
        }

        protected abstract HttpWebRequest CreateRequest();

        public void Send() {
            _request = CreateRequest();
            _response = (HttpWebResponse)_request.GetResponse();
            _responseStream = _response.GetResponseStream();
        }

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
