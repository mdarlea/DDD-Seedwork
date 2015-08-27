using System;

namespace Swaksoft.Infrastructure.Crosscutting.Transformers
{
    public interface ITransformersFactory<TBase, TDestination>
    {
        ITransformer<TDestination> Create<TSource>() where TSource : TBase;
        ITransformer<TDestination> Create(Type sourceType);
    }
}