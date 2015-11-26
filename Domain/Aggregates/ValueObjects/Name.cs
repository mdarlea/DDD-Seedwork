using System;
using System.ComponentModel.DataAnnotations;

namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        #region equality
        public override bool Equals(Name other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(MiddleName, other.MiddleName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (MiddleName != null ? MiddleName.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion equality

        public Name(string firstName, string lastName, string middleName=null)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException("firstName");
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException("lastName");

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        private Name() { } //required for EF

        [Required]
        [MaxLength(100)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; private set; }

        public string MiddleName { get; private set; }
    }
}
