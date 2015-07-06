using System;
using System.Collections.Generic;
using System.Linq;

namespace Swaksoft.Infrastructure.Crosscutting.Transformers
{
    public abstract class TransformersFactory<TBase,TDestination> : ITransformersFactory<TBase, TDestination>
    {
        private readonly IDictionary<Type, Func<ITransformer<TDestination>>> _actionsRegistry 
            = new Dictionary<Type, Func<ITransformer<TDestination>>>();

        protected TransformersFactory()
        {
            
        }

        public ITransformer<TDestination> Create<TSource>()
            where TSource:TBase
        {
            return Create(typeof (TSource));
        }

        public ITransformer<TDestination> Create(Type sourceType)
        {
            var action = (from p in _actionsRegistry where p.Key == sourceType select p.Value).FirstOrDefault();
            if (action == null)
            {
                throw new ArgumentNullException(string.Format("Could not find an operation of {0} type", sourceType));
            }
            return action(); 
        }
        
        protected void Initialize<TSource,TTransformer>() 
            where TSource : TBase
            where TTransformer : Transformer<TSource, TDestination>, new()
        {
            _actionsRegistry.Add(typeof(TSource), () => new TTransformer());
        }
    }
}
