using System;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AccessToken
    {
        public bool HasRegistered { get; set; }
        public string ExternalUserName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
    }
}
