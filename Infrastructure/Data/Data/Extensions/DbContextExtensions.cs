using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Infrastructure.Data.Seedwork.Extensions
{
    public static class DbContextExtensions
    {
        public static DbEntityConfigurator<TEntity> Entity<TEntity>(this DbContext context, TEntity entity)
            where TEntity : Entity
        {
            return new DbEntityConfigurator<TEntity>(context,entity);
        }

        public static ReferecesConfiguratorFactory<TProperty> Property<TEntity,TProperty>(
            this DbContext context, TEntity entity, Expression<Func<TEntity, TProperty>> reference)
            where TEntity : Entity
            where TProperty : Entity
        {
            context.Entry(entity).Reference(reference).Load();
            return new ReferecesConfiguratorFactory<TProperty>(context, reference.Compile()(entity));
        }

        public static ReferecesConfiguratorFactory<TProperty> Property<TProperty>(this DbContext context, TProperty entity)
            where TProperty : Entity
        {
            return new ReferecesConfiguratorFactory<TProperty>(context, entity);
        }
    }
}
