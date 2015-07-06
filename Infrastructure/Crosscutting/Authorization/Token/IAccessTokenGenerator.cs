using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public interface IAccessTokenGenerator<in TUser> : IDisposable
        where TUser:IdentityUser
    {
        Task<JObject> GenerateLocalAccessToken(TUser user, string clientId);
    }
}