using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Repositories
{
    public interface IIdentityRepository<TUser> : IDisposable
        where TUser : IdentityUser
    {
        Task<IdentityResult> RegisterUser(TUser user, string password);
        Client FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();

        Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId);
        RefreshToken FindRefreshToken(string refreshTokenId);

        Task<TUser> FindUserAsync(string userName, string password);

        Task<TUser> FindAsync(UserLoginInfo loginInfo);
        TUser Find(UserLoginInfo loginInfo);

        TUser FindById(string userId);
        Task<TUser> FindByIdAsync(string userId);

        TUser FindByName(string userName);
        Task<TUser> FindByNameAsync(string userName);
        
        Task<IdentityResult> CreateAsync(TUser user);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}
