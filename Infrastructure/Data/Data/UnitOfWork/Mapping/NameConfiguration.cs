using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork.Mapping
{
    public class NameConfiguration : ComplexTypeConfiguration<Name>
    {
        public NameConfiguration()
        {
            Property(vo => vo.FirstName).HasMaxLength(150);
            Property(vo => vo.LastName).HasMaxLength(150);
            Property(vo => vo.MiddleName).HasMaxLength(150);
        }
    }
}
