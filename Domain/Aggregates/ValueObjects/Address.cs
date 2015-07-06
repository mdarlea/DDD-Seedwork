
namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        #region equality

        public override bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(StreetAddress1, other.StreetAddress1) && string.Equals(City, other.City) && Equals(State, other.State) && string.Equals(Zip, other.Zip);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (StreetAddress1 != null ? StreetAddress1.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (City != null ? City.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Zip != null ? Zip.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion equality

        public string StreetAddress1 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
    }
}
