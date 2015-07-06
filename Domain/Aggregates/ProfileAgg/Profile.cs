namespace Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg
{
    /// <summary>
    /// Aggregate root for providers configuration
    /// </summary>
    public abstract class Profile : Entity
    {
        public string Name { get; set; }
    }
}
