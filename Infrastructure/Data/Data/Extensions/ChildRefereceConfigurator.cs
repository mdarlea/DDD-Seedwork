using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Infrastructure.Data.Seedwork.Extensions
{
    public class DbEntityConfigurator<TEntity>
        where TEntity : Entity
    {
        private readonly DbContext _context;
        private readonly TEntity _entity;

        public DbEntityConfigurator(DbContext context, TEntity entity)
        {
            _context = context;
            _entity = entity;
        }

        public ReferecesConfiguratorFactory<TProperty> Reference<TProperty>(Expression<Func<TEntity, TProperty>> reference)
            where TProperty : Entity
        {
            _context.Entry(_entity).Reference(reference).Load();
            return new ReferecesConfiguratorFactory<TProperty>(_context, reference.Compile()(_entity));
        }
    }

    public class ReferecesConfiguratorFactory<TProperty>
        where TProperty:Entity
    {
        private readonly DbContext _context;
        private readonly TProperty _entity;

        public ReferecesConfiguratorFactory(DbContext context, TProperty entity)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (entity == null) throw new ArgumentNullException("entity");
            _context = context;
            _entity = entity;
        }

        public ReferenceConfigurator<TProperty,TEntity> For<TEntity>() where TEntity : TProperty
        {
            return new ReferenceConfigurator<TProperty,TEntity>(this, _entity as TEntity);
        }

        public ReferecesConfiguratorFactory<TProperty> Load<TEntity, T>(TEntity property, Expression<Func<TEntity, T>> reference)
            where TEntity : TProperty
            where T : class
        {
            if (property == null) throw new ArgumentNullException("property");
            if (reference == null) throw new ArgumentNullException("reference");

            _context.Entry(property).Reference(reference).Load();

            return this;
        }
    }

    public class ReferenceConfigurator<TProperty,TEntity>
        where TProperty:Entity
        where TEntity:TProperty
    {
        private readonly ReferecesConfiguratorFactory<TProperty> _factory;
        private readonly TEntity _property;

        public ReferenceConfigurator(ReferecesConfiguratorFactory<TProperty> factory, TEntity property)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _factory = factory;
            _property = property;
        }

        public ReferenceConfigurator<TProperty, TEntity> Load<T>(Expression<Func<TEntity, T>> reference)
             where T : class
        {
            if (reference == null) throw new ArgumentNullException("reference");

            if (_property != null)
            {
                _factory.Load(_property,reference);
            }

            return this;
        }

        public ReferenceConfigurator<TProperty, TOther> For<TOther>() where TOther : TProperty
        {
            return _factory.For<TOther>();
        }
    }
}
