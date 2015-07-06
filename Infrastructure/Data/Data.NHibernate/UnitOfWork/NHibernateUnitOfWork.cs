using System;
using NHibernate;
using Swaksoft.Domain.Seedwork;

namespace Swaksoft.Infrastructure.Data.NHibernate.Seedwork.UnitOfWork
{
    public abstract class NHibernateUnitOfWork : ITransactionUnitOfWork
    {
        private readonly string _nameOrConnectionString;
        
        private ISession _context;

        protected NHibernateUnitOfWork(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public ISession Session
        {
            get
            {
                return (_context == null || !_context.IsOpen) 
                    ? (_context = DataContextFactory.GetContextForConnection(_nameOrConnectionString)) 
                    : _context;
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IUnitOfWork BeginTransaction()
        {
            return new TransactionAdapter(Session.BeginTransaction());
        }
    }
}
