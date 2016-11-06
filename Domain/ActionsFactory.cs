using System;
using System.Collections.Generic;
using System.Linq;

namespace Swaksoft.Domain.Seedwork
{
    public interface IActionsFactory<TActions>
    {
        TActions Resolve(string key);
    }

    public interface IActionsFactory<in TKey, TActions>
    {
        TActions Resolve(TKey key);
        T Resolve<T>(TKey key) where T:TActions;
    }


    public abstract class ActionsFactory<TActions> : IActionsFactory<TActions>
    {
        private readonly IDictionary<string, Func<TActions>> _actionsRegistryByKey = new Dictionary<string, Func<TActions>>();

        public TActions Resolve(string key)
        {
            var action = _actionsRegistryByKey.SingleOrDefault(d => d.Key == key);
            if (action.Value == null)
                throw new ArgumentNullException(string.Format("Could not find an operation of {0} type", key));
            return action.Value();
        }

        protected void Register(string key, Func<TActions> action)
        {
            _actionsRegistryByKey.Add(key, action);
        }
    }

    public abstract class ActionsFactoryFor<TActions> : ActionsFactory<TActions>
        where TActions : new() 
    {
        protected void RegisterFor(string key)
        {
            Register(key,()=>new TActions());
        }
    }

    public abstract class ActionsFactory<TKey, TActions> : IActionsFactory<TKey, TActions>
    {
        private readonly IDictionary<Type, Func<object,TActions>> _actionsRegistry = new Dictionary<Type, Func<object,TActions>>();
    
        public TActions Resolve(TKey key)
        {
            var type = key.GetType();
            var action = _actionsRegistry.SingleOrDefault(d => d.Key == type);
            if (action.Value == null)
                throw new ArgumentNullException(string.Format("Could not find an operation of {0} type", type));
            return action.Value(key);
        }

        public T Resolve<T>(TKey key)
            where T : TActions
        {
            return (T) Resolve(key);
        }
    
        protected void Register<T>(Func<T,TActions> action) where T:TKey
        {
            _actionsRegistry.Add(typeof(T), key => action((T)key));
        }
    }
}
