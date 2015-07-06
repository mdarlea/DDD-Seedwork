using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AuthenticationTicketFactory<TUser> : IAuthenticationTicketFactory<TUser>
        where TUser:IdentityUser
    {
        private readonly UserManager<TUser> _userManager;

        public AuthenticationTicketFactory(UserManager<TUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            _userManager = userManager;
        }

        public AuthenticationTicket Create(TUser user, string clientId)
        {
            var userName = user.UserName;

            var identity = _userManager.CreateIdentity(user, OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("client_id", clientId));
            identity.AddClaim(new Claim("role", "user"));

            var properties = CreateProperties(userName, clientId);

            return new AuthenticationTicket(identity, properties);
        }
        
        private static AuthenticationProperties CreateProperties(string userName, string clientId)
        {
            var tokenExpiration = TimeSpan.FromDays(1);

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "as:client_id", clientId }
            };

            return new AuthenticationProperties(data)
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration)
            };
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
            }
        }
    }
}