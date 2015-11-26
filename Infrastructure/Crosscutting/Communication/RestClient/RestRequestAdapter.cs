using System;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class RestRequestConfiguration<TRequest>
    {
        private readonly IHttpClientRequest _httpClientRequest;
        private readonly TRequest _data;
        private readonly Uri _url;

        public RestRequestConfiguration(IHttpClientRequest httpClientRequest, Uri url, TRequest data)
        {
            if (httpClientRequest == null) throw new ArgumentNullException("httpClientRequest");
            if (url == null) throw new ArgumentNullException("url");

            _httpClientRequest = httpClientRequest;
            _data = data;
            _url = url;
        }

        public TResponse AndReturn<TResponse>()
        {
            return _httpClientRequest.Post<TRequest, TResponse>(_data, _url);
        }
    }

    public class RestRequestAdapter
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IHttpClientRequest _httpClientRequest;

        public RestRequestAdapter(IUriBuilder uriBuilder)
            : this(uriBuilder, new HttpClientRequest())
        {
        }

        public RestRequestAdapter(IUriBuilder uriBuilder, IHttpClientRequest httpClientRequest)
        {
            if (uriBuilder == null) throw new ArgumentNullException("uriBuilder");
            if (httpClientRequest == null) throw new ArgumentNullException("httpClientRequest");
            _uriBuilder = uriBuilder;
            _httpClientRequest = httpClientRequest;
        }

        public TResponse Post<TRequest, TResponse>(TRequest data)
        {
            return Post<TRequest, TResponse>(data, null);
        }

        public RestRequestConfiguration<TRequest> Post<TRequest>(TRequest data)
        {
            return Post(data, null);
        }

        public RestRequestConfiguration<TRequest> Post<TRequest>(TRequest data, object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);   
            return new RestRequestConfiguration<TRequest>(_httpClientRequest, uri, data);
        }

        public TResponse Post<TRequest, TResponse>(TRequest data, object parameters)
        {
            var uri = (parameters==null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _httpClientRequest.Post<TRequest, TResponse>(data, uri);
        }

        public TResponse Get<TResponse>(object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _httpClientRequest.Get<TResponse>(uri);
        }
    }
}
