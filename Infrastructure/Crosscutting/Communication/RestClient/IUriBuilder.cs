using System;

namespace Swaksoft.Infrastructure.Crosscutting.Communication.RestClient
{
    public interface IUriBuilder
    {
        Uri GetUri();
        Uri GetUriFor(object parameters);
    }

    public interface IUriBuilder<T> : IUriBuilder
    {
        Uri GetUriFor(T parameters);
    }
}
