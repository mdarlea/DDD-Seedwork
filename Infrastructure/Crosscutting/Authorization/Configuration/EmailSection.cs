using System.Configuration;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Configuration
{
    public class EmailSection : ConfigurationSection
    {
        [ConfigurationProperty("email")]
        public EmailElement Email
        {
            get { return ((EmailElement)(base["email"])); }
            set { base["email"] = value; }
        }

        [ConfigurationProperty("login")]
        public LoginElement Login
        {
            get { return ((LoginElement)(base["login"])); }
            set { base["login"] = value; }
        }
    }
}
