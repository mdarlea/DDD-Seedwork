
using System;

namespace Swaksoft.Domain.Seedwork
{
    public interface ITransactionUnitOfWork : IDisposable
    {
        IUnitOfWork BeginTransaction();
    }
}
