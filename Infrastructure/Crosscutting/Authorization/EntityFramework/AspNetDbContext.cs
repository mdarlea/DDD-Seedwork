using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework
{
    public class AspNetDbContext<TUser> : IdentityDbContext<TUser>
        where TUser: IdentityUser
    {
        public AspNetDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString, throwIfV1Schema: false)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
