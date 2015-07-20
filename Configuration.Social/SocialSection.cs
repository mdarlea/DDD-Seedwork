using System.Configuration;

namespace Swaksoft.Configuration.Social
{
    public class SocialSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public SocialProviderCollection SocialProviders
        {
            get { return ((SocialProviderCollection)(base["providers"])); }
            set { base["providers"] = value; }
        }
    }
}