using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Core;
using System.Linq.Dynamic;
using System.Runtime.InteropServices;
using System.Text;
using Swaksoft.Core.Dto;
using Swaksoft.Core.Expressions;

namespace Swaksoft.Infrastructure.Crosscutting.Extensions
{
    public static class RepositoryExtensions
    {
        public static string GetExpression(LambdaExpression expression)
        {
           return GetField(expression.Body as MemberExpression);
        }

        private static string GetField(MemberExpression propertyExpression)
        {
            var fieldName = new StringBuilder();
            var empty = true;
            if (propertyExpression != null)
            {
                var prefix = GetField(propertyExpression.Expression as MemberExpression);
                if (!string.IsNullOrWhiteSpace(prefix))
                {
                    fieldName.Append(prefix);
                    empty = false;
                }

                var propertyInfo = propertyExpression.Member;
                if (!empty)
                {
                    fieldName.Append(".");
                }
                fieldName.Append(propertyInfo.Name);
            }
            return fieldName.ToString();
        }

        public static PagedList GetPaged<TEntity>(
            this IQueryable<TEntity> set,
            string sortBy)
                where TEntity : class
        {
            return set.GetPaged(sortBy, 1, 10, true, null);
        }

        public static PagedList GetPaged<TEntity>(
            this IQueryable<TEntity> set,
            string sortBy,
            string filterBy,
            params object[] filterParameters)
            where TEntity : class
        {
            return set.GetPaged(sortBy, 1, 10, true, filterBy, filterParameters);
        }

        public static PagedList GetPaged<TEntity>(
            this IQueryable<TEntity> set,
            string sortBy,
            int page,
            int pageSize,
            bool ascending,
            string filterBy,
            params object[] filterParameters)
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(sortBy)) throw new ArgumentNullException("sortBy");

            var resultSet = (string.IsNullOrWhiteSpace(filterBy)) ? set : set.Where(filterBy, filterParameters);

            var sortDirection = (ascending) ? "asc" : "desc";
            resultSet = resultSet.OrderBy(string.Format("{0} {1}", sortBy, sortDirection));
            var results = resultSet.Skip(pageSize * (page - 1)).Take(pageSize);

            var pagedRecord = new PagedList
            {
                Content = results.ToList(),
                TotalRecords = set.Count(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public static PagedList GetPaged<TEntity,TValue>(
            this IQueryable<TEntity> set,
            int pageSize,
            Expression<Func<TEntity, TValue>> identityCallback,
            TValue minIdentity)
                where TEntity : class
        {
            return set.GetPaged(pageSize, identityCallback, minIdentity, null);
        }

        public static PagedList GetPaged<TEntity>(
            this IQueryable<TEntity> set,
            int pageSize,
            Expression<Func<TEntity, bool>> identity,
            Expression<Func<TEntity,bool>> filterBy)
            where TEntity : class
        {
            if (identity == null) throw new ArgumentNullException("identity");
            if (filterBy == null) throw new ArgumentNullException("filterBy");

            var expression = identity.And(filterBy);

            var resultSet = set.Where(expression);
            var results = resultSet.Take(pageSize);

            var pagedRecord = new PagedList
            {
                Content = results.ToList(),
                PageSize = pageSize
            };

            return pagedRecord;

        }

        public static PagedList GetPaged<TEntity,TValue>(
            this IQueryable<TEntity> set,
            int pageSize,
            Expression<Func<TEntity,TValue>> identityCallback,
            TValue minIdentity,
            string filterBy,
            params object[] filterParameters)
                where TEntity : class
        {
            IQueryable<TEntity> resultSet;

            var nullIdentity = default(TValue);
                if (!minIdentity.Equals(nullIdentity))
                {
                    
                    var filter = new StringBuilder(
                        string.Format("({0} > @{1})", 
                        GetExpression(identityCallback), 
                        (filterParameters == null) ? 0 : filterParameters.Length));

                    var args = new List<object>();

                    if (!string.IsNullOrWhiteSpace(filterBy))
                    {
                        filter.Append(" && (");
                        filter.Append(filterBy);
                        filter.Append(")");
                        if (filterParameters != null && filterParameters.Any())
                        {
                            args.AddRange(filterParameters);
                        }
                    }
                    
                    args.Add(minIdentity);
                    resultSet = set.Where(filter.ToString(), args.ToArray());
                }
                else
                {
                    resultSet = (string.IsNullOrWhiteSpace(filterBy)) ? set : set.Where(filterBy, filterParameters);
                }
                var results = resultSet.Take(pageSize);

            var pagedRecord = new PagedList
            {
                Content = results.ToList(),
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public static PagedList GetPaged<TEntity, KProperty>(
            this IQueryable<TEntity> set,
            Expression<Func<TEntity, KProperty>> orderByExpression,
            int page = 1, 
            int pageSize = 10,
            bool ascending= true)
                where TEntity: class
        {
            var results  = (ascending) ? 
                set.OrderBy(orderByExpression)
                    .Skip(pageSize * (page-1))
                    .Take(pageSize) : 
                set.OrderByDescending(orderByExpression)
                .Skip(pageSize * (page-1))
                .Take(pageSize);

            var pagedRecord = new PagedList
            {
                Content = results.ToList(),
                TotalRecords = set.Count(),
                CurrentPage = page,
                PageSize = pageSize
            };
            
            return pagedRecord;
        }

        public static PagedList ProjectedAs<TDto>(this PagedList pagedList)
            where TDto: class
        {
            return new PagedList
            {
                Content = pagedList.Content.ProjectedAsCollection<TDto>(),
                TotalRecords = pagedList.TotalRecords,
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize
            };
        }
    }
}
