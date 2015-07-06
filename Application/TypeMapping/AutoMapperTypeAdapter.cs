using AutoMapper;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;

namespace Swaksoft.Application.Seedwork.TypeMapping
{
    public class AutoMapperTypeAdapter : ITypeAdapter
    {
        public TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);
        }
    }
}
