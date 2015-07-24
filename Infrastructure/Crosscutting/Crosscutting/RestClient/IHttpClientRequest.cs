using System;
using System.Threading.Tasks;

namespace Swaksoft.Infrastructure.Crosscutting.RestClient
{
    public interface IHttpClientRequest
    {
        bool IsAuthenticated();

        TResponse Post<TRequest, TResponse>(TRequest request, Uri uri);
        string Post<TRequest>(TRequest request, Uri uri);
        Task<Uri> PostAsync<TRequest>(TRequest request, Uri uri);

        TResponse Get<TResponse>(Uri uri);
        Task<TResponse> GetAsync<TResponse>(Uri uri);
    }
}
