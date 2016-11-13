using System;
using System.Configuration;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Configuration
{
    public class EmailElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = false)]
        //[StringValidator(MinLength = 2, MaxLength = 60)]
        public string Name
        {
            get
            {
                return (String)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        //[RegexStringValidator("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        public String Address
        {
            get
            {
                return (String)this["address"];
            }
            set
            {
                this["address"] = value;
            }
        }
    }
}
