using System;

namespace Swaksoft.Core.External
{
    public class ExternalProviderCredentials
    {
        public ExternalProviderCredentials(ExternalProvider type, string consumerKey, string consumerSecret)
        {
            if (string.IsNullOrWhiteSpace(consumerKey)) throw new ArgumentNullException("consumerKey");
            if (string.IsNullOrWhiteSpace(consumerSecret)) throw new ArgumentNullException("consumerSecret");

            Type = type;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public ExternalProvider Type { get; private set; }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }
    }
}
