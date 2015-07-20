using System;
using System.Configuration;
using Swaksoft.Core.External;

namespace Swaksoft.Configuration.Social
{
    [ConfigurationCollection(typeof(SocialProvider))]
    public class SocialProviderCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "socialProvider";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }


        public override bool IsReadOnly()
        {
            return false;
        }
        

        protected override ConfigurationElement CreateNewElement()
        {
            return new SocialProvider();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SocialProvider)(element)).Type;
        }

        public SocialProvider this[int idx]
        {
            get
            {
                return (SocialProvider) BaseGet(idx);
            }
        }

        public SocialProvider this[ExternalProvider type]
        {
            get { return (SocialProvider) BaseGet(type); }
        }
    }
}