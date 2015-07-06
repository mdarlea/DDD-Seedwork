using System;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Core.Crypto;

namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    public class OAuthToken : ValueObject<OAuthToken>
    {
        public OAuthToken(string accessToken)
            :this(accessToken,null)
        {
        }

        public OAuthToken(string accessToken, string accessTokenSecret)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException("accessToken");

            var crypto = new CryptoAes();
            AccessToken = crypto.EncryptToString(accessToken);

            if (!string.IsNullOrWhiteSpace(accessTokenSecret))
            {
                AccessTokenSecret = crypto.EncryptToString(accessTokenSecret);    
            }
        }

        private OAuthToken() { } //required for EF

        [Required]
        public string AccessToken { get; private set; }

        public string AccessTokenSecret { get; private set; }
        
        #region equality
        public override bool Equals(OAuthToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(AccessToken, other.AccessToken) && string.Equals(AccessTokenSecret, other.AccessTokenSecret);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OAuthToken) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (AccessToken != null ? AccessToken.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AccessTokenSecret != null ? AccessTokenSecret.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion equality
    }
}
