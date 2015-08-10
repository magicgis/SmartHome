using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Networking {
    public class HttpPUTRequest : HttpRESTRequest {
        private string _body;

        public HttpPUTRequest(string url, string body) : base(url) {
            _body = body;
        }

        protected override HttpWebRequest CreateRequest() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8;";
            Stream requestStream = request.GetRequestStream();
            byte[] data = Encoding.UTF8.GetBytes(_body);
            requestStream.Write(data, 0, data.Length);
            requestStream.Flush();
            requestStream.Close();
            requestStream.Dispose();
            return request;
        }
    }
}
