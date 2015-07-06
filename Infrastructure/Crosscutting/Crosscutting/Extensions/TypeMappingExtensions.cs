using System.Collections.Generic;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;

namespace Swaksoft.Infrastructure.Crosscutting.Extensions
{
    public static class TypeMappingExtensions
    {
        /// <summary>
        /// Project a type using a DTO
        /// </summary>
        /// <typeparam name="TProjection">The dto projection</typeparam>
        /// <param name="item">The source entity to project</param>
        /// <returns>The projected type</returns>
        public static TProjection ProjectedAs<TProjection>(this object item)
            where TProjection : class
        {
            var adapter = TypeAdapterLocator.CreateAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        /// <summary>
        /// projected a enumerable collection of items
        /// </summary>
        /// <typeparam name="TProjection">The dtop projection type</typeparam>
        /// <param name="items">the collection of entity items</param>
        /// <returns>Projected collection</returns>
        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<object> items)
            where TProjection : class
        {
            var adapter = TypeAdapterLocator.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }
    }
}
