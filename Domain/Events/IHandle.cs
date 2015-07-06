
using System;

namespace Swaksoft.Domain.Seedwork.Events
{
    public interface IHandle : IDisposable
    {
    }

    public interface IHandle<T> :IHandle where T : IDomainEvent
    {
        void Handle(T args);
    }
}
