using System;

namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    /// <summary>
    /// Supports only US phone numbers
    /// </summary>
    public class PhoneNumber : ValueObject<PhoneNumber>, IFormattable
    {
        #region equality
        public override bool Equals(PhoneNumber other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(CountryCode, other.CountryCode) && string.Equals(AreaCode, other.AreaCode) && string.Equals(Number, other.Number);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (CountryCode != null ? CountryCode.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AreaCode != null ? AreaCode.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Number != null ? Number.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion equality

        public string CountryCode { get; set; }

        public string AreaCode { get; set; }

        public string Number { get; set; }

        public string GetExtension()
        {
            throw new NotImplementedException();
        }

        public bool HasValue()
        {
            return !(string.IsNullOrWhiteSpace(CountryCode) && string.IsNullOrWhiteSpace(AreaCode) && string.IsNullOrWhiteSpace(Number));
        }

        //ToDo: currently supports only US phone numbers. The method needs improvement
        public static bool TryParse(string source, out PhoneNumber phoneNumber)
        {
            phoneNumber = null;

            var value = GetTrimValue(source);
            if (string.IsNullOrEmpty(value)) return false;

            value = value.Replace(" ", string.Empty).Replace("-",string.Empty).Replace("(",string.Empty).Replace(")",string.Empty).Replace("+",string.Empty);
            if (string.IsNullOrEmpty(value)) return false;

            var length = value.Length;
            const int digits = 10;
            phoneNumber = new PhoneNumber();
            if (length > 10)
            {
                var start = length - digits;
                phoneNumber.CountryCode = value.Substring(0, start);
                phoneNumber.SetPhonePartValue(value, start);
            }
            else
            {
                phoneNumber.SetPhonePartValue(value, 0);
                if (!string.IsNullOrWhiteSpace(phoneNumber.AreaCode))
                {
                    phoneNumber.CountryCode = "1";       
                }
            }

            return true;
        }
        
        private void SetPhonePartValue(string value, int start)
        {
            const int areaCodeLength = 3;
            if (value.Length > areaCodeLength)
            {
                AreaCode = value.Substring(start, 3);
                Number = value.Substring(start + 3);
            }
            else
            {
                Number = (start > 0) ? value.Substring(start) : value;
            }
        }

        public override string ToString()
        {
            if (!HasValue()) return string.Empty;

            var number = GetTrimValue(Number);
            if (number.Length > 3)
            {
                number = string.Format("{0}-{1}", number.Substring(0, 3), number.Substring(3));
            }
            return string.Format("({0}) {1}", GetTrimValue(AreaCode), number);
        }
        
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return (!HasValue()) 
                    ? string.Empty 
                    : (format=="twilio") 
                        ? string.Format("+{0}{1}{2}", GetTrimValue(CountryCode), GetTrimValue(AreaCode), GetTrimValue(Number))
                        : ToString();
        }

        private static string GetTrimValue(string value)
        {
            return (value == null) ? string.Empty : value.Trim();
        }
    }
}
