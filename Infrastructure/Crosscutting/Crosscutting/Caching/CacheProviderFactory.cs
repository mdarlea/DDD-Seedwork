using System;
using System.Configuration;
using System.Runtime.Caching;

namespace Swaksoft.Infrastructure.Crosscutting.Caching
{
    public static class CacheProviderFactory
    {
        private static readonly Lazy<CacheProvider> currentCacheProvider = new Lazy<CacheProvider>(() =>
        {
            var provider = ConfigurationManager.AppSettings["Swaksoft.Infrastructure.Crosscutting.Caching.Type"] ?? "STANDARD";
            switch (provider.ToUpperInvariant())
            {
                case "STANDARD":
                    return new CacheProvider(MemoryCache.Default, null);
                case "DISTRIBUTED":
                    //TODO: implement AppFabric cache
                    throw new NotImplementedException("Not yet supported");
                default:
                    throw new InvalidOperationException(string.Format("Unknown provider type {0}", provider.ToUpperInvariant()));
            }
        });
      
        public static CacheProvider Provider
        {
            get { return currentCacheProvider.Value; }
        }
    }
}
