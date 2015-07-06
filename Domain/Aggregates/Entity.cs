using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swaksoft.Domain.Seedwork.Aggregates
{
    public abstract class Entity : Entity<int>
    {
        protected Entity()
        {
            Id = -1;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public override int Id { get; set; }

        public override bool IsTransient()
        {
            return (Id < 1);
        }
    }

    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class Entity<TId>
    {
        int? _requestedHashCode;

        /// <summary>
        /// Get the persisten object identifier
        /// </summary>
        [Key]
        public virtual TId Id
        {
            get; set; 
        }

        /// <summary>
        /// Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public virtual bool IsTransient()
        {
            return Id.Equals(default(TId));
        }       

        /// <summary>
        /// Change current identity for a new non transient identity
        /// </summary>
        /// <param name="identity">the new identity</param>
        public void ChangeCurrentIdentity(TId identity)
        {
            if (!IsTransient())
                Id = identity;
        }

        #region Overrides Methods

        /// <summary>
        /// <see cref="M:System.Object.Equals"/>
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals"/></param>
        /// <returns><see cref="M:System.Object.Equals"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = (Entity<TId>)obj;

            return ((!item.IsTransient() && !IsTransient())) && item.Id.Equals(Id);
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
