using System;
using NHibernate;
using Swaksoft.Domain.Seedwork;

namespace Swaksoft.Infrastructure.Data.NHibernate.Seedwork.UnitOfWork
{
    public class TransactionAdapter : IUnitOfWork
    {
        private readonly ITransaction _transaction;

        public TransactionAdapter(ITransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            _transaction = transaction;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void CommitAndRefreshChanges()
        {
            Commit();
        }

        public void RollbackChanges()
        {
            _transaction.Rollback();
        }
    }
}
