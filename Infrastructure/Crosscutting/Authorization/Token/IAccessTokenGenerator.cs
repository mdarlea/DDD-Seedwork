using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public interface IAccessTokenGenerator<in TUser> : IDisposable
        where TUser:IdentityUser
    {
        Task<AccessToken> GenerateLocalAccessToken(TUser user, string clientId, TimeSpan tokenExpiration);
    }
}