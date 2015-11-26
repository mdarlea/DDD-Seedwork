using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization
{
    public class AspNetUserStore<TUser> : UserStore<TUser>
         where TUser : IdentityUser
    {
        private readonly AuthorizationDbContext<TUser> context;

        public AspNetUserStore(AuthorizationDbContext<TUser> context)
            :base(context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this.context = context;
        }

        public virtual Client FindClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");

            return context.Clients.Find(clientId);
        }

        public virtual Task<Client> FindClientAsync(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");

            return context.Clients.FindAsync(clientId);
        }

        public virtual RefreshToken FindRefreshToken(string refreshTokenId)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenId)) throw new ArgumentNullException("refreshTokenId");

            return context.RefreshTokens.Find(refreshTokenId);
        }

        public virtual Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenId)) throw new ArgumentNullException("refreshTokenId");

            return context.RefreshTokens.FindAsync(refreshTokenId);
        }

        public virtual int RemoveRefreshToken(RefreshToken refreshToken)
        {
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");

            context.RefreshTokens.Remove(refreshToken);
            return context.SaveChanges();
        }

        public virtual Task<int> RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");
            
            context.RefreshTokens.Remove(refreshToken);
            return context.SaveChangesAsync();
        }

        public virtual Task<int> AddRefreshtokenAsync(RefreshToken refreshToken)
        {
            if (refreshToken == null) throw new ArgumentNullException("refreshToken");

            context.RefreshTokens.Add(refreshToken);
            return context.SaveChangesAsync();
        }
    }
}
