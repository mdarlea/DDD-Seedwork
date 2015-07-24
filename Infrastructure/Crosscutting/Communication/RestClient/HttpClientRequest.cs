using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Swaksoft.Infrastructure.Crosscutting.RestClient;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class HttpClientRequest : IHttpClientRequest
    {
        private readonly IHttpActions _httpActions;

        public HttpClientRequest() : this(new JsonHttpActions())
        {
        }

        public HttpClientRequest(IHttpActions httpActions)
        {
            if (httpActions == null) throw new ArgumentNullException("httpActions");
            _httpActions = httpActions;
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        public TResponse Post<TRequest, TResponse>(TRequest request, Uri uri)
        {
            using (var client = GetHttpClient(uri))
            {
                var response = _httpActions.Post(client, uri, request).Result;
                response.EnsureSuccessStatusCode();    // Throw if not a success code.

                var responseContent = response.Content;

                // Get the URI of the created resource.
                return responseContent.ReadAsAsync<TResponse>().Result;
            }
        }

        public string Post<TRequest>(TRequest request, Uri uri)
        {
            using (var client = GetHttpClient(uri))
            {
                var response = _httpActions.Post(client, uri, request).Result;
                response.EnsureSuccessStatusCode();    // Throw if not a success code.

                var responseContent = response.Content;

                // Get the URI of the created resource.
                return responseContent.ReadAsStringAsync().Result;
            }
        }

        public async Task<Uri> PostAsync<TRequest>(TRequest request, Uri uri)
        {
            using (var client = GetHttpClient(uri))
            {
                var response = await _httpActions.Post(client, uri, request);
                response.EnsureSuccessStatusCode();    // Throw if not a success code.
                
                // Get the URI of the created resource.
                return response.Headers.Location;
            }
        }

        public TResponse Get<TResponse>(Uri uri)
        {
            using (var client = GetHttpClient(uri))
            {
                var response = client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();    // Throw if not a success code.
                
                var responseContent = response.Content;
                return responseContent.ReadAsAsync<TResponse>().Result;
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(Uri uri)
        {
            using (var client = GetHttpClient(uri))
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();    // Throw if not a success code.
                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        private HttpClient GetHttpClient(Uri uri)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(uri.Host)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_httpActions.ContentType));

            return client;
        }

        private async Task<HttpResponseMessage> GetResponseMessage<TRequest>(HttpClient client,Uri uri, TRequest request)
        {
            return await client.PostAsJsonAsync(uri.PathAndQuery, request);
        }
    }
}
