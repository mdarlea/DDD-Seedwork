using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using System.Data.Entity.ModelConfiguration;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork.Mapping
{
    public class StateConfiguration : ComplexTypeConfiguration<State>
    {
        public StateConfiguration()
        {
            Property(e => e.Abbreviation).HasMaxLength(2);
        }
    }
}
