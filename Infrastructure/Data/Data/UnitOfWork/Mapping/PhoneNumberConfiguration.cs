using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork.Mapping
{
    public class PhoneNumberConfiguration : ComplexTypeConfiguration<PhoneNumber>
    {
        public PhoneNumberConfiguration()
        {
            Property(e => e.CountryCode).HasMaxLength(5);
            Property(e => e.AreaCode).HasMaxLength(3);
            Property(e => e.Number).HasMaxLength(15);
        }
    }
}
