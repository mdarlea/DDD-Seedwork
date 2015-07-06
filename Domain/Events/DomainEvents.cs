using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Swaksoft.Domain.Seedwork.Events
{
  public static class DomainEvents
 { 
    //[ThreadStatic] //so that each thread has its own callbacks
    private static readonly ThreadLocal<List<Delegate>> actions = new ThreadLocal<List<Delegate>>(() => new List<Delegate>());
    private static readonly ThreadLocal<IDictionary<Type, IHandle>> handlers = new ThreadLocal<IDictionary<Type, IHandle>>(() => new Dictionary<Type, IHandle>());

    private static readonly object thisObject = new object();
    private static IHandleDomainEvents _domainEventsHandler;

    //Registers a callback for the given domain event
    public static void Register<T>(Action<T> callback) where T : IDomainEvent
    {
        var value = actions.Value;
        value.Add(callback);
    }
    public static void Register<T>(IHandle<T> handler) where T : IDomainEvent
    {
        var value = handlers.Value;
        value.Add(typeof(T), handler);
    }
      
    public static void SetCurrent(IHandleDomainEvents domainEventsHandler)
    {
        lock (thisObject)
        {
            _domainEventsHandler = domainEventsHandler;
        }
    }

    //Clears callbacks passed to Register on the current thread
     public static void ClearCallbacks ()
     {
        actions.Value.Clear();
    }
   
     //Raises the given domain event
     public static void Raise<T>(T args) where T : IDomainEvent
     {
         if (_domainEventsHandler != null)
         {
            _domainEventsHandler.Handle(args);    
         }

         if (handlers != null)
             foreach (var handler in handlers.Value.OfType<IHandle<T>>())
                 handler.Handle(args);

         if (actions == null) return;
         foreach (var action in actions.Value.OfType<Action<T>>())
             action(args);
     }
 } 
}
