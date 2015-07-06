namespace Swaksoft.Infrastructure.Crosscutting.Transformers
{
    public abstract class Transformer<TSource, TDestination>
     : ITransformer<TDestination>
    {
        public abstract TDestination Transform(TSource source);

        public TDestination Transform(object source)
        {
            return Transform((TSource) source);
        }
    }
}
