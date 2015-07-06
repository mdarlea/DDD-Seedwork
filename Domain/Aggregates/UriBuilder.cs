using System;
using System.Linq;

namespace Swaksoft.Domain.Seedwork.Aggregates
{
    public class UriBuilder
    {
        private readonly string _uriTemplate;
        
        public UriBuilder(string uriTemplate)
        {
            if (uriTemplate == null) throw new ArgumentNullException("uriTemplate");
            _uriTemplate = uriTemplate;
        }

        public Uri GetUri()
        {
            return new Uri(_uriTemplate);
        }

        public Uri GetUriFor(object parameters)
        {
            var type = parameters.GetType();
            var properties = type.GetProperties();
            var url = properties
                .Aggregate(_uriTemplate,
                           (current, property) => 
                            current.Replace("{" + property.Name + "}", property.GetValue(parameters, null).ToString()));

            //removes the placeholders that have no value
            var value = url;
            var foundLeft = value.IndexOf("{", StringComparison.Ordinal);
            var foundRight = value.IndexOf("}", StringComparison.Ordinal);
            while (foundLeft >= 0 && foundRight > foundLeft)
            {
                value = value.Substring(0, foundLeft) + value.Substring(foundRight + 1);
                foundLeft = value.IndexOf("{", StringComparison.Ordinal);
                foundRight = value.IndexOf("}", StringComparison.Ordinal);
            }
            url = value;

            return new Uri(url);
        }
    }
}
