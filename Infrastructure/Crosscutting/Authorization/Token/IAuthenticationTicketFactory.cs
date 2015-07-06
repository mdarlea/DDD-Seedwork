using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public interface IAuthenticationTicketFactory<in TUser> : IDisposable
        where TUser:IdentityUser
    {
        AuthenticationTicket Create(TUser user, string clientId);
    }
}