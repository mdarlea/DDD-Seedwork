namespace Swaksoft.Infrastructure.Crosscutting.Transformers
{
    public interface ITransformer<TDestination>
    {
        TDestination Transform(object source);
    }
}
