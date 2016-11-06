using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization
{
    public abstract class UserManager<TUser> : Microsoft.AspNet.Identity.UserManager<TUser>
         where TUser : class,IUser<string>
    {
        private readonly IUserStore<TUser> store;
        private bool disposed;

        protected UserManager(IUserStore<TUser> store) 
            : base(store)
        {
            if (store == null) throw new ArgumentNullException(nameof(store));
            this.store = store;
        }

        public virtual Client FindClient(string clientId)
        {
            ThrowIfDisposed();

            return store.FindClient(clientId);
        }

        public virtual Task<Client> FindClientAsync(string clientId)
        {
            ThrowIfDisposed();

            return store.FindClientAsync(clientId);
        }

        public virtual RefreshToken FindRefreshToken(string refreshTokenId)
        {
            ThrowIfDisposed();

            return store.FindRefreshToken(refreshTokenId);
        }
        
        public virtual Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId)
        {
            ThrowIfDisposed();

            return store.FindRefreshTokenAsync(refreshTokenId);
        }

        public virtual bool RemoveRefreshToken(string refreshTokenId)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(refreshTokenId)) throw new ArgumentNullException(nameof(refreshTokenId));

            var refreshToken = FindRefreshToken(refreshTokenId);
            if (refreshToken == null) return false;

            var result = store.RemoveRefreshToken(refreshToken);
            return result > 0;
        }

        public virtual async Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            ThrowIfDisposed();
            if (refreshToken == null) throw new ArgumentNullException(nameof(refreshToken));
            
            var result = await store.AddRefreshtokenAsync(refreshToken);
            return result > 0;
        }

        public virtual async Task<bool> RemoveRefreshTokenAsync(string refreshTokenId)
        {
            ThrowIfDisposed(); 
            if (string.IsNullOrWhiteSpace(refreshTokenId)) throw new ArgumentNullException("refreshTokenId");

            var refreshToken = await FindRefreshTokenAsync(refreshTokenId);
            if (refreshToken == null) return false;

            var result = await store.RemoveRefreshTokenAsync(refreshToken);
            return result > 0;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        /// When disposing, actually dipose the store
        /// 
        /// </summary>
        /// <param name="disposing"/>
        protected override void Dispose(bool disposing)
        {
            if (disposed) return;

            base.Dispose(disposing);
            
            if (!disposing) return;
            disposed = true;
        }
    }
}
