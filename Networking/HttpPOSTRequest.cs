using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Networking {
    /// <summary>
    /// Creates and manages a Restful POST Request
    /// </summary>
    public class HttpPOSTRequest : HttpRESTRequest {
        private string _body;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        /// <param name="url">The requests url</param>
        /// <param name="body">The requests body</param>
        public HttpPOSTRequest(string url, string body) : base(url) {
            _body = body;
        }

        /// <summary>
        /// Creates the request
        /// </summary>
        /// <returns>A httpwebrequest</returns>
        protected override HttpWebRequest CreateRequest() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8;";
            Stream requestStream = request.GetRequestStream();
            byte[] data = Encoding.UTF8.GetBytes(_body);
            requestStream.Write(data, 0 , data.Length);
            requestStream.Flush();
            requestStream.Close();
            requestStream.Dispose();
            return request;
        }
    }
}
