using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Core.Serializers;
using Swaksoft.Infrastructure.Crosscutting.RestClient;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class RestRequest : IRestRequest
    {
        private readonly ISerializer _serializer;

        public RestRequest() : this(new JsonSerializer())
        {
        }

        public RestRequest(ISerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            _serializer = serializer;
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public TResponse Post<TRequest, TResponse>(TRequest request, Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            //webRequest.ProtocolVersion = HttpVersion.Version10;
            //webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Method = "POST";
            webRequest.ContentType = _serializer.ContentType;
            webRequest.KeepAlive = true;

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(_serializer.Serialize(request));

            webRequest.ContentLength = bytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                return GetHttpWebResponse<TResponse>(webRequest);
            } 
        }

        public string Post<TRequest>(TRequest request, Uri uri)
        {
            return Post<TRequest, string>(request, uri);
        }

        public virtual TResponse Get<TResponse>(Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.ContentType = _serializer.ContentType;
            return GetHttpWebResponse<TResponse>(webRequest);
        }

        public async Task<TResponse> GetAsync<TResponse>(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return (await response.Content.ReadAsAsync<TResponse>());
            }
        }

        private TResponse GetHttpWebResponse<TResponse>(HttpWebRequest webRequest)
        {
            using (var response = webRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));

                return _serializer.Desearialize<TResponse>(response.GetResponseStream());
            }
        }
    }
}
