using System;
using System.Runtime.Caching;

namespace Swaksoft.Infrastructure.Crosscutting.Caching
{
    public interface ICacheProvider
    {
        T Get<T>(string keyName, Func<T> executeIfNotExists, DateTime absoluteExpiration) where T : class;
    }

    public class CacheProvider : ICacheProvider
    {
        private readonly ObjectCache _cacheProvider;
        private readonly string _regionName;

        public CacheProvider(ObjectCache provider, string name)
        {
            _cacheProvider = provider;
            _regionName = name;

            if (_cacheProvider == null) throw new ArgumentNullException("Invalid cache provider");
            if (_cacheProvider is MemoryCache) _regionName = null;
        }


        public T Get<T>(string keyName, Func<T> executeIfNotExists, DateTime absoluteExpiration) where T:class
        {
            var data = _cacheProvider.Get(keyName, _regionName) as T;
            if (data != null)
            {
                return data;
            }
            data = executeIfNotExists();
            if (data != null)
            {
                _cacheProvider.Add(new CacheItem(keyName, data, this._regionName), new CacheItemPolicy
                {
                    AbsoluteExpiration = absoluteExpiration,
                });
            }
            return data;
        }

        public void Remove(string keyName)
        {
            _cacheProvider.Remove(keyName, _regionName);
        }
    }
}
