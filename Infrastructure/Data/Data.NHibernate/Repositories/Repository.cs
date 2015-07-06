using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Infrastructure.Data.NHibernate.Seedwork.UnitOfWork;

namespace Swaksoft.Infrastructure.Data.NHibernate.Seedwork.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class
    {
        private readonly NHibernateUnitOfWork _unitOfWork;

        public Repository(ITransactionUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = (NHibernateUnitOfWork)unitOfWork;
        }

        protected ISession Session
        {
            get
            {
                return _unitOfWork.Session;
            }
        }

        public ITransactionUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public void Add(TEntity item)
        {
            
        }

        public void Remove(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Modify(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void TrackItem(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Merge(TEntity persisted, TEntity current)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            return Session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public IQueryable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return Session.Query<TEntity>().Where(specification.SatisfiedBy());
        }

        public IQueryable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool @ascending)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return Session.Query<TEntity>().Where(filter);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return Session.Query<TEntity>().SingleOrDefault(filter);
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
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion
    }
}