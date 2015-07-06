﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AccessTokenGenerator<TUser> : IAccessTokenGenerator<TUser>
        where TUser: IdentityUser
    {
        private readonly IAuthenticationTicketFactory<TUser> _authenticationTicketFactory;
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;
        private readonly IAuthenticationTokenFactory _authenticationTokenFactory;

        public AccessTokenGenerator(
            IAuthenticationTicketFactory<TUser> authenticationTicketFactory,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat,
            IAuthenticationTokenFactory authenticationTokenFactory)
        {
            _authenticationTicketFactory = authenticationTicketFactory;
            _secureDataFormat = secureDataFormat;
            _authenticationTokenFactory = authenticationTokenFactory;
        }

        public async Task<JObject> GenerateLocalAccessToken(TUser user,string clientId)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");

            //await _signInManager.SignInAsync(user, isPersistent: true, rememberBrowser: false);

            var ticket = _authenticationTicketFactory.Create(user, clientId);
            var protectedTicket = _secureDataFormat.Protect(ticket);

            var refreshToken = await _authenticationTokenFactory.CreateRefreshTokenAsync(ticket, protectedTicket);

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return null;
            }

            var tokenExpiration = ticket.Properties.ExpiresUtc.GetValueOrDefault().DateTime;

            return new JObject(new JProperty("hasRegistered", true),
                               new JProperty("externalUserName", user.UserName),
                               new JProperty("userName", user.UserName),
                               //new JProperty("access_token", protectedTicket),
                               new JProperty("access_token", refreshToken),
                               new JProperty("token_type", "bearer"),
                               new JProperty("expires_in", tokenExpiration.Second.ToString()),
                               new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                               new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));
        }

        public void Dispose()
        {
            _authenticationTicketFactory.Dispose();
            _authenticationTokenFactory.Dispose();
        }
    }
}