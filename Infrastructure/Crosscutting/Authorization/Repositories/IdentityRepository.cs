using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories
{
    public class IdentityRepository<TUser> : IIdentityRepository<TUser>
        where TUser : IdentityUser
    {
        private readonly AspNetUserManager<TUser> userManager;

        public IdentityRepository(AspNetUserManager<TUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException("userManager");
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(TUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<TUser> FindUserAsync(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }

        public Client FindClient(string clientId)
        {
            return userManager.FindClient(clientId);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            return await userManager.AddRefreshTokenAsync(token);

            //var existingToken = _context.RefreshTokens.FirstOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

           //var result = (existingToken == null);

           //if (!result)
           //{
           //     result = await RemoveRefreshToken(existingToken);
           //}

           //if (result)
           //{
            
           //}
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return await userManager.RemoveRefreshTokenAsync(refreshTokenId);
        }
        
        public RefreshToken FindRefreshToken(string refreshTokenId)
        {
            return userManager.FindRefreshToken(refreshTokenId);
        }

        public async Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId)
        {
            return await userManager.FindRefreshTokenAsync(refreshTokenId);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            throw new NotImplementedException();
        }

        public async Task<TUser> FindAsync(UserLoginInfo loginInfo)
        {
            var user = await userManager.FindAsync(loginInfo);

            return user;
        }

        public TUser Find(UserLoginInfo loginInfo)
        {
            var user = userManager.Find(loginInfo);
            return user;
        }

        public TUser FindById(string userId)
        {
            var user = userManager.FindById(userId);
            return user;
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            return user;
        }

        public TUser FindByName(string userName)
        {
            var user = userManager.FindByName(userName);
            return user;
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await userManager.AddLoginAsync(userId, login);

            return result;
        }
        
        #region dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            
            userManager.Dispose();
        }
        #endregion dispose
    }
}