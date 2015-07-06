
namespace Swaksoft.Infrastructure.Crosscutting.RestClient.Extensions
{
    public static class AppSettingsExtensions
    {
        public static UriBuilder GetUriBuilderForKey(this System.Collections.Specialized.NameValueCollection settings, string key)
        {
            return new UriBuilder(settings[key]);
        }
    }
}
