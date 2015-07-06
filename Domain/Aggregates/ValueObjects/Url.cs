using System;
using System.ComponentModel.DataAnnotations;

namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    public class Url : ValueObject<Url>
    {
        public Url()
        {
        }

        public Url(string path)
        {
            Path = path;
        }

        [Required]
        public string Path { get; private set; }

        public Url Replace(string oldValue, string newValue)
        {
            return new Url(Path.Replace(oldValue,newValue));
        }
        public virtual Url ToUrl(object routeValues)
        {
            if (!HasValue()) return null;
            if (routeValues == null) return new Url(Path);
            
            var uriBuilder = new UriBuilder(Path);
            return uriBuilder.GetUriFor(routeValues);
        }

        public virtual Uri ToUri()
        {
            return this;
        }

        public override string ToString()
        {
            return Path;
        }

        public bool HasValue()
        {
            return !string.IsNullOrWhiteSpace(Path);
        }

        #region cast
        public static implicit operator Url(Uri uri)
        {
            return (uri == null) ? null : new Url(uri.AbsoluteUri);
        }

        public static implicit operator Uri(Url path)
        {
            return (path == null) ? null : new Uri(path.Path);
        }

        public static implicit operator Url(string uri)
        {
            return (string.IsNullOrWhiteSpace(uri)) ? null : new Url(uri);
        }

        public static implicit operator string(Url path)
        {
            return (path == null) ? null : path.ToString();
        }
        #endregion cast
    }
}
