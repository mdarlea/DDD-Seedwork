using Newtonsoft.Json;
using System;
using System.Net;
using Swaksoft.Core.Serializers;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class HttpClientRequest : RestRequest
    {
        public HttpClientRequest(ISerializer serializer) : base(serializer)
        {
        }

        public override TResponse Get<TResponse>(Uri uri)
        {
            using (var webClient = new WebClient())
            {
                return JsonConvert.DeserializeObject<TResponse>(
                    webClient.DownloadString(uri)
                );
            }
        }
    }
}
