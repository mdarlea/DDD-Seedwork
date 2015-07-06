using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork.Mapping
{
    public class OAuthTokenConfiguration : ComplexTypeConfiguration<OAuthToken>
    {
        public OAuthTokenConfiguration()
        {
            Property(vo => vo.AccessToken).HasMaxLength(250).IsRequired();
            Property(vo => vo.AccessTokenSecret).HasMaxLength(250);
        }
    }
}
