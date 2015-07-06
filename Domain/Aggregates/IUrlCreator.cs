using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Domain.Seedwork.Aggregates
{
    public interface IUrlCreator
    {
        Url CreateUrl(object routeValues);
    }
}