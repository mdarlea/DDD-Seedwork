using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AuthenticationTicketFactory<TUser> : IAuthenticationTicketFactory<TUser>
        where TUser:IdentityUser
    {
        private readonly UserManager<TUser> userManager;

        public AuthenticationTicketFactory(UserManager<TUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            this.userManager = userManager;
        }

        public AuthenticationTicket Create(TUser user, string clientId, TimeSpan tokenExpiration)
        {
            if (user == null) throw new ArgumentNullException("user");

            var userName = user.UserName;

            var identity = userManager.CreateIdentity(user, OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("client_id", clientId));
            identity.AddClaim(new Claim("role", "user"));

            var properties = CreateProperties(userName, clientId, tokenExpiration);

            return new AuthenticationTicket(identity, properties);
        }

        private static AuthenticationProperties CreateProperties(string userName, string clientId, TimeSpan tokenExpiration)
        {
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

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (userManager != null)
            {
                userManager.Dispose();
            }
        }

        #endregion
    }
}