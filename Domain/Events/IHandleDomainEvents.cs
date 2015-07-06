using System;

namespace Swaksoft.Domain.Seedwork.Events
{
    public interface IHandleDomainEvents
    {
        void Handle<T>(T domainEvent) where T:IDomainEvent;
    }
}
