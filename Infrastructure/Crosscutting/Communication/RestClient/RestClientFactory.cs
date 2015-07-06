using Swaksoft.Infrastructure.Crosscutting.RestClient;
using Swaksoft.Core.Serializers;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestRequestAdapter GetJsonRestRequest(IUriBuilder uriBuilder)
        {
            var jsonRequest = new RestRequest(new JsonSerializer());
            return new RestRequestAdapter(uriBuilder, jsonRequest);
        }

        public RestRequestAdapter GetXmlRestRequest(IUriBuilder uriBuilder)
        {
            var xmlRequest = new RestRequest(new XmlSerializer());
            return new RestRequestAdapter(uriBuilder, xmlRequest);
        }
    }
}
