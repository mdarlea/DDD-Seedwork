using System;
using System.Configuration;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Configuration
{
    public class LoginElement : ConfigurationElement
    {
        [ConfigurationProperty("user", IsRequired = true)]
        public String User
        {
            get
            {
                return (String)this["user"];
            }
            set
            {
                this["user"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public String Password
        {
            get
            {
                return (String)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }
}
