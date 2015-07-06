//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Gte.Infrastructure.Data.EF.Resources;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Infrastructure.Data.Seedwork.Repositories
{
    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> :IRepository<TEntity>
        where TEntity:class
    {

        #region Members

        readonly ITransactionUnitOfWork _unitOfWork;

        protected DbContext Context
        {
            get
            {
                return _unitOfWork as DbContext;
            }
        }

        protected void Attach(TEntity item)            
        {
            //attach and set as unchanged
            Context.Entry(item).State = EntityState.Unchanged;
        }
        
        protected void SetModified(TEntity item)           
        {
            //this operation also attach item in object state manager
            Context.Entry(item).State = EntityState.Modified;
        }

        protected void ApplyCurrentValues(TEntity original, TEntity current)           
        {
            //if it is not attached, attach original and set current values
            Context.Entry(original).CurrentValues.SetValues(current);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(ITransactionUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public ITransactionUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public virtual void Add(TEntity item)
        {

            if (item != null)
                GetSet().Add(item); // add new item in this set
            else
            {
                GetLog().LogInfo(Messages.info_CannotAddNullEntity,typeof(TEntity).ToString());
            }
        }

        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                GetLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                Attach(item);
            else
            {
                GetLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }
      
        public virtual void Modify(TEntity item)
        {
            if (item != null)
                SetModified(item);
            else
            {
                GetLog().LogInfo(Messages.info_CannotModifyNullEntity, typeof(TEntity).ToString());
            }
        }
     
        public virtual TEntity Get(int id)
        {
            return id > 0 ? GetSet().Find(id) : null;
        }

       
        public virtual IQueryable<TEntity> GetAll()
        {
            return GetQuery();
        }
        
        public virtual IQueryable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetQuery().Where(specification.SatisfiedBy());
        }
      
        public virtual IQueryable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetQuery();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            return set.OrderByDescending(orderByExpression)
                .Skip(pageCount * pageIndex)
                .Take(pageCount);
        }
    
        public virtual IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetQuery().Where(filter);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return GetQuery().SingleOrDefault(filter);
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            ApplyCurrentValues(persisted, current);
        }

        #endregion

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

        #region Private Methods

        protected virtual IQueryable<TEntity> GetQuery()
        {
            return Context.Set<TEntity>();
        }
        protected IDbSet<TEntity> GetSet()
        {
            return Context.Set<TEntity>();
        }

        private ILogger GetLog()
        {
            return LoggerLocator.CreateLog(GetType());
        }

        #endregion
    }
}
