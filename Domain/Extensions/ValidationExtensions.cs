using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork.Resources;

namespace Swaksoft.Domain.Seedwork.Extensions
{
    public class EntityValidation<T>
    {
        private readonly T _source;
        private readonly List<ValidationResult> _validationResults;

        public EntityValidation(T source) : this(source,new List<ValidationResult>())
        {
        }

        public EntityValidation(T source, List<ValidationResult> validationResults)
        {
            _source = source;
            _validationResults = validationResults;
        }

        public EntityValidation<T> NotNull<TValue>(Expression<Func<T, TValue>> propertyExpression)
            where TValue:class
        {
            return Validate(propertyExpression, value => value == null, Messages.validation_entity_Required);
        }

        public EntityValidation<T> NotNullOrEmpty(Expression<Func<T, string>> propertyExpression)
        {
            return Validate(propertyExpression, string.IsNullOrWhiteSpace, Messages.validation_entity_Required);
        }

        public EntityValidation<T> AddRange(IEnumerable<ValidationResult> validationResults)
        {
            _validationResults.AddRange(validationResults);
            return this;
        }

        public EntityValidation<T> Validate<TValue>(Expression<Func<T, TValue>> propertyExpression, Predicate<TValue> validationPredicate, string message)
        {
            //get hte property name
            var body = propertyExpression.Body as MemberExpression;
            var propertyName = (body != null) ? body.Member.Name : string.Empty;

            var value = propertyExpression.Compile()(_source);
            if (validationPredicate(value))
            {
                _validationResults.Add(new ValidationResult(string.Format(message,propertyName), new[] { propertyName }));
            }
            return this;
        }

        public IEnumerable<ValidationResult> Execute()
        {
            return _validationResults;
        }
    }

    public static class ValidationExtensions
    {
        public static EntityValidation<T> ValidationResults<T>(this T source)
        {
            return source.ValidationResults(new List<ValidationResult>());
        }

        public static EntityValidation<T> ValidationResults<T>(this T source, IEnumerable<ValidationResult> validationResults)
        {
            return new EntityValidation<T>(source, validationResults.ToList());
        }
    }


}
