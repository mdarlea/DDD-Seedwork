using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public interface IHttpActions
    {
        string ContentType { get; }
        Task<HttpResponseMessage> Post<TRequest>(HttpClient client, Uri uri, TRequest request);
    }
}