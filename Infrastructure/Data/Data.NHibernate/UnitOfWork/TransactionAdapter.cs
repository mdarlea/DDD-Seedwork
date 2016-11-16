using System;
using System.Threading.Tasks;
using NHibernate;
using Swaksoft.Domain.Seedwork;

namespace Swaksoft.Infrastructure.Data.NHibernate.Seedwork.UnitOfWork
{
    public class TransactionAdapter : IUnitOfWork
    {
        private readonly ITransaction transaction;

        public TransactionAdapter(ITransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            this.transaction = transaction;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
            }
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public Task<int> CommitAsync()
        {
           throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            Commit();
        }

        public void RollbackChanges()
        {
            transaction.Rollback();
        }
    }
}
