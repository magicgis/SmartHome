using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Networking {
    public class HttpGETRequest : HttpRESTRequest {
        private string _extension;

        public HttpGETRequest(string url, string extension) : base(url) {
            _extension = extension;
        }

        protected override HttpWebRequest CreateRequest() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url + "/" + _extension);
            return request;
        }
    }
}
