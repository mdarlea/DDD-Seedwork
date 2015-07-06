using System;
using Swaksoft.Infrastructure.Crosscutting.RestClient;
using UriBuilder = System.UriBuilder;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public class RestRequestConfiguration<TRequest>
    {
        private readonly IRestRequest _restRequest;
        private readonly TRequest _data;
        private readonly Uri _url;

        public RestRequestConfiguration(IRestRequest restRequest, Uri url, TRequest data)
        {
            if (restRequest == null) throw new ArgumentNullException("restRequest");
            if (url == null) throw new ArgumentNullException("url");

            _restRequest = restRequest;
            _data = data;
            _url = url;
        }

        public TResponse AndReturn<TResponse>()
        {
            return _restRequest.Post<TRequest, TResponse>(_data, _url);
        }
    }

    public class RestRequestAdapter
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IRestRequest _restRequest;
        
        public RestRequestAdapter(IUriBuilder uriBuilder, IRestRequest restRequest)
        {
            if (uriBuilder == null) throw new ArgumentNullException("uriBuilder");
            if (restRequest == null) throw new ArgumentNullException("restRequest");
            _uriBuilder = uriBuilder;
            _restRequest = restRequest;
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
            return new RestRequestConfiguration<TRequest>(_restRequest, uri, data);
        }

        public TResponse Post<TRequest, TResponse>(TRequest data, object parameters)
        {
            var uri = (parameters==null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _restRequest.Post<TRequest, TResponse>(data, uri);
        }

        public TResponse Get<TResponse>(object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _restRequest.Get<TResponse>(uri);
        }
    }
}
