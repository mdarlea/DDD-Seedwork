using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories
{
    public class IdentityRepository<TUser> : IIdentityRepository<TUser>
        where TUser : IdentityUser
    {
        private readonly AuthorizationDbContext<TUser> _context;

        private readonly UserManager<TUser> _userManager;

        public IdentityRepository(AuthorizationDbContext<TUser> context, UserManager<TUser> userManager)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (userManager == null) throw new ArgumentNullException("userManager");

            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(TUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<TUser> FindUserAsync(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _context.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
           //var existingToken = _context.RefreshTokens.FirstOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

           //var result = (existingToken == null);

           //if (!result)
           //{
           //     result = await RemoveRefreshToken(existingToken);
           //}

           //if (result)
           //{
               _context.RefreshTokens.Add(token);    
           //}
            
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _context.RefreshTokens.Remove(refreshToken);
               return await _context.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
             return await _context.SaveChangesAsync() > 0;
        }

        public RefreshToken FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = _context.RefreshTokens.Find(refreshTokenId);

            return refreshToken;
        }

        public async Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _context.RefreshTokens.ToList();
        }

        public async Task<TUser> FindAsync(UserLoginInfo loginInfo)
        {
            var user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public TUser Find(UserLoginInfo loginInfo)
        {
            var user = _userManager.Find(loginInfo);
            return user;
        }

        public TUser FindById(string userId)
        {
            var user = _userManager.FindById(userId);
            return user;
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public TUser FindByName(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user;
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

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

            _context.Dispose();
            _userManager.Dispose();
        }
        #endregion dispose
    }
}