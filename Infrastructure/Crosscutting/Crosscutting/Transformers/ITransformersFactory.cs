namespace Swaksoft.Infrastructure.Crosscutting.Transformers
{
    public interface ITransformersFactory<TBase, TDestination>
    {
        ITransformer<TDestination> Create<TSource>() where TSource : TBase;
    }
}