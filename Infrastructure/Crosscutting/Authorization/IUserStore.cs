using System.Threading.Tasks;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization
{
    public interface IUserStore<TUser> : Microsoft.AspNet.Identity.IUserStore<TUser>
        where TUser : class, Microsoft.AspNet.Identity.IUser<string>
    {
        Client FindClient(string clientId);
        Task<Client> FindClientAsync(string clientId);
        RefreshToken FindRefreshToken(string refreshTokenId);
        Task<RefreshToken> FindRefreshTokenAsync(string refreshTokenId);
        int RemoveRefreshToken(RefreshToken refreshToken);
        Task<int> RemoveRefreshTokenAsync(RefreshToken refreshToken);
        Task<int> AddRefreshtokenAsync(RefreshToken refreshToken);
    }
}