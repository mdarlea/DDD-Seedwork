using Swaksoft.Infrastructure.Crosscutting.RestClient;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public interface IRestClientFactory
    {
        RestRequestAdapter GetJsonRestRequest(IUriBuilder uriBuilder);
        RestRequestAdapter GetXmlRestRequest(IUriBuilder uriBuilder);
    }
}