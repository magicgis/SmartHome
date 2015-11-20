using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Networking {
    /// <summary>
    /// Creates and manages a Restful GET Request
    /// </summary>
    public class HttpGETRequest : HttpRESTRequest {
        private string _extension;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="url">The base url for the request</param>
        /// <param name="extension">The extension for specific GET</param>
        public HttpGETRequest(string url, string extension) : base(url) {
            _extension = extension;
        }

        /// <summary>
        /// Creates the request
        /// </summary>
        /// <returns>A HttpWebRequest</returns>
        protected override HttpWebRequest CreateRequest() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url + "/" + _extension);
            return request;
        }
    }
}
