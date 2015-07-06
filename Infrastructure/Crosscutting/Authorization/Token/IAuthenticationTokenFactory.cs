using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.Token
{
    public interface IAuthenticationTokenFactory : IDisposable
    {
        AuthenticationTicket IssueAuthenticationTicket(string token);
        Task<AuthenticationTicket> IssueAuthenticationTicketAsync(string token);
        Task<string> CreateRefreshTokenAsync(AuthenticationTicket ticket, string protectedTicket);
    }
}