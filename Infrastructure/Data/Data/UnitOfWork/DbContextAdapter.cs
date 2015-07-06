using System.Data.Entity.Infrastructure;
using System.Linq;
using Swaksoft.Domain.Seedwork;
using System;
using System.Data.Entity;

namespace Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork
{
    public class DbContextAdapter : IUnitOfWork
    {
        private readonly DbContext _context;

        public DbContextAdapter(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    _context.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            _context.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            //commit on dispose if any changes have not been committed or rolled back
            var manager = ((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager;
            if (manager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted).Any())
            {
                Commit();
            }
        }
        #endregion
    }
}
