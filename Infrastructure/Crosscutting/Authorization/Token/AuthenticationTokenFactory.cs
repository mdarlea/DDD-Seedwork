using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Swaksoft.Core.Crypto;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories;
namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public class AuthenticationTokenFactory<TUser> : IAuthenticationTokenFactory
        where TUser:IdentityUser
    {
        private readonly IIdentityRepository<TUser> _applicationUserRepository;
        private readonly IAuthenticationTicketFactory<TUser> _authenticationTicketFactory;

        public AuthenticationTokenFactory(
            IIdentityRepository<TUser> applicationUserRepository,
            IAuthenticationTicketFactory<TUser> authenticationTicketFactory)
        {
            if (applicationUserRepository == null) throw new ArgumentNullException("applicationUserRepository");
            if (authenticationTicketFactory == null) throw new ArgumentNullException("authenticationTicketFactory");
            _applicationUserRepository = applicationUserRepository;
            _authenticationTicketFactory = authenticationTicketFactory;
        }

        public AuthenticationTicket IssueAuthenticationTicket(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("token");

            var hashedTokenId = token;
            var refreshToken = _applicationUserRepository.FindRefreshToken(hashedTokenId);

            if (refreshToken != null && refreshToken.ExpiresUtc > DateTime.UtcNow)
            {
                var user = _applicationUserRepository.FindByName(refreshToken.Subject);

                return GetAuthenticationTicket(user, refreshToken);
            }

            return null;
        }

        public async Task<AuthenticationTicket> IssueAuthenticationTicketAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("token");

            var hashedTokenId = token;
            var refreshToken = await _applicationUserRepository.FindRefreshTokenAsync(hashedTokenId);

            if (refreshToken != null && refreshToken.ExpiresUtc > DateTime.UtcNow)
            {
                var user = await _applicationUserRepository.FindByNameAsync(refreshToken.Subject);

                return  GetAuthenticationTicket(user, refreshToken);
            }

            return null;
        }

        private AuthenticationTicket GetAuthenticationTicket(TUser user, RefreshToken refreshToken)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");

            var ticket = _authenticationTicketFactory.Create(user, refreshToken.ClientId);
            ticket.Properties.IssuedUtc = DateTime.UtcNow;
            ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(1);
                // This needs to be after Issued, not the real expiry time
            return ticket;
        }

        public async Task<string> CreateRefreshTokenAsync(AuthenticationTicket ticket, string protectedTicket)
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
            if (string.IsNullOrWhiteSpace(protectedTicket)) throw new ArgumentNullException("protectedTicket");

            var refreshTokenId = Guid.NewGuid().ToString("n");
            var refreshToken = CryptoAes.GetHash(refreshTokenId);
            
            var clientId = ticket.Properties.Dictionary["as:client_id"];

            var token = new RefreshToken
            {
                Id = refreshToken,
                ClientId = clientId,
                Subject = ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = ticket.Properties.ExpiresUtc.GetValueOrDefault().DateTime,
                ProtectedTicket = protectedTicket
            };

            var result = await _applicationUserRepository.AddRefreshToken(token);

            return (result) ? refreshToken : null;
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
            if (_applicationUserRepository != null)
            {
                _applicationUserRepository.Dispose();    
            }

            if (_authenticationTicketFactory != null)
            {
                _authenticationTicketFactory.Dispose();    
            }
        }

        #endregion
    }
}