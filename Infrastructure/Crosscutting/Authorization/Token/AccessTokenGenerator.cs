using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AccessTokenGenerator<TUser> : IAccessTokenGenerator<TUser>
        where TUser: IdentityUser
    {
        private readonly IAuthenticationTicketFactory<TUser> authenticationTicketFactory;
        private readonly ISecureDataFormat<AuthenticationTicket> secureDataFormat;
        private readonly IAuthenticationTokenFactory authenticationTokenFactory;

        public AccessTokenGenerator(
            IAuthenticationTicketFactory<TUser> authenticationTicketFactory,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat,
            IAuthenticationTokenFactory authenticationTokenFactory)
        {
            this.authenticationTicketFactory = authenticationTicketFactory;
            this.secureDataFormat = secureDataFormat;
            this.authenticationTokenFactory = authenticationTokenFactory;
        }

        public async Task<JObject> GenerateLocalAccessToken(TUser user, string clientId, TimeSpan tokenExpiration)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");

            //await _signInManager.SignInAsync(user, isPersistent: true, rememberBrowser: false);

            var ticket = authenticationTicketFactory.Create(user, clientId,tokenExpiration);
            var protectedTicket = secureDataFormat.Protect(ticket);

            var refreshToken = await authenticationTokenFactory.CreateRefreshTokenAsync(ticket, protectedTicket);

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return null;
            }

            var expiration = ticket.Properties.ExpiresUtc.GetValueOrDefault().DateTime;

            return new JObject(new JProperty("hasRegistered", true),
                               new JProperty("externalUserName", user.UserName),
                               new JProperty("userName", user.UserName),
                               //new JProperty("access_token", protectedTicket),
                               new JProperty("access_token", refreshToken),
                               new JProperty("token_type", "bearer"),
                               new JProperty("expires_in", expiration.Second.ToString()),
                               new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                               new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));
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
            if (authenticationTicketFactory != null)
            {
                authenticationTicketFactory.Dispose();    
            }
            if (authenticationTokenFactory != null)
            {
                authenticationTokenFactory.Dispose();    
            }
        }

        #endregion
    }
}