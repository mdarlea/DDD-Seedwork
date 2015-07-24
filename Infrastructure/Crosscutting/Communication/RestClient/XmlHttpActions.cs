using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class XmlHttpActions : IHttpActions
    {
        public string ContentType
        {
            get
            {
                return "application/xml";
            }
        }

        public async Task<HttpResponseMessage> Post<TRequest>(HttpClient client, Uri uri, TRequest request)
        {
            return await client.PostAsXmlAsync(uri.PathAndQuery, request);
        }
    }
}
