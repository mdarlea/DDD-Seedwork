using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork.Mapping
{
    public class UrlConfiguration : ComplexTypeConfiguration<Url>
    {
        public UrlConfiguration()
        {
            Property(vo => vo.Path).HasMaxLength(100);
        }
    }
}
