using Swaksoft.Domain.Seedwork;
using System.Data.Entity;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork
{
    public abstract class EntityFrameworkUnitOfWork : DbContext, ITransactionUnitOfWork
    {
        private readonly IUnitOfWork _contextAdapter;

        protected EntityFrameworkUnitOfWork(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
            _contextAdapter = new DbContextAdapter(this);
        }

        public IUnitOfWork BeginTransaction()
        {
            return _contextAdapter;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var type = typeof (EntityFrameworkUnitOfWork);
            modelBuilder.Configurations.AddFromAssembly(type.Assembly);
        }
    }
}
