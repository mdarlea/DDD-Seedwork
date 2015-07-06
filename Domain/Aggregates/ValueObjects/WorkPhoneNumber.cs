namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    public class WorkPhoneNumber : PhoneNumber
    {
        public string Extension { get; set; }

        #region equality
        public bool Equals(WorkPhoneNumber other)
        {
            return base.Equals(other) && string.Equals(Extension, other.Extension);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((WorkPhoneNumber) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Extension != null ? Extension.GetHashCode() : 0);
            }
        }

        #endregion equality
    }
}
