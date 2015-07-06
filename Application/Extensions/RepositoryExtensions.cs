using System;
using Swaksoft.Application.Seedwork.Validation;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Infrastructure.Crosscutting.Validation;

namespace Swaksoft.Application.Seedwork.Extensions
{
    public static class RepositoryExtensions
    {
        public static T SaveEntity<TEntity,T>(this IRepository<TEntity> repository, T entity)
            where TEntity : class
            where T: class, TEntity
        {
            if (entity == null) throw new ArgumentNullException("entity");

            var entityValidator = EntityValidatorLocator.CreateValidator();

            if (entityValidator.IsValid(entity))
            {
                using (var transaction = repository.BeginTransaction())
                {
                    repository.Add(entity);
                    transaction.Commit();    
                }
            }
            else
            {
                //ToDo: Do not throw validation exception. Return an action result
                throw new ValidationErrorsException(entityValidator.GetInvalidMessages(entity));
            }

            return entity;
        }

        public static IUnitOfWork BeginTransaction<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class
        {
            return repository.UnitOfWork.BeginTransaction();
        }
    }
}
