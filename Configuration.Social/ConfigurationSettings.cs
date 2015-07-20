using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Swaksoft.Configuration.Social
{
    public class ConfigurationSettings
    {
        private static readonly Lazy<ConfigurationSettings> configurationSettings = new Lazy<ConfigurationSettings>();

        public static ConfigurationSettings Current
        {
            get { return configurationSettings.Value; }
        }

        public SocialSection SocialProviderConfiguration
        {
            get
            {
                return (SocialSection)ConfigurationManager.GetSection("socialSection");
            }
        }

        public SocialProviderCollection ProvidersCollection
        {
            get
            {
                return SocialProviderConfiguration.SocialProviders;
            }
        }

        public IEnumerable<SocialProvider> SocialProviders
        {
            get {
                return ProvidersCollection.Cast<SocialProvider>().Where(selement => selement != null);
            }
        }
    }
}