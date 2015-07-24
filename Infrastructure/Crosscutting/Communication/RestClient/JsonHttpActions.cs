using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class JsonHttpActions : IHttpActions
    {
        public string ContentType
        {
            get
            {
                return "application/json";
            }
        }

        public async Task<HttpResponseMessage> Post<TRequest>(HttpClient client, Uri uri, TRequest request)
        {
            return await client.PostAsJsonAsync(uri.PathAndQuery, request);
        }
    }
}
