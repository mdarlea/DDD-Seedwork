using System;

namespace Swaksoft.Domain.Seedwork.Aggregates.ValueObjects
{
    /// <summary>
    /// State value object
    /// </summary>
    public class State : ValueObject<State>
    {
        public State(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation)) throw new ArgumentNullException("abbreviation");

            Abbreviation = abbreviation.ToUpper();
        }

        /// <summary>
        /// The state abbreviation
        /// </summary>
        /// <example><i>Example:</i> FL</example>
        public string Abbreviation { get; private set; }

        #region equality
        public override bool Equals(State other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(Abbreviation, other.Abbreviation);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Abbreviation != null ? Abbreviation.GetHashCode() : 0);
            }
        }
        #endregion equality

        #region cast operators
        /// <summary>
        /// Allow direct assignment from string:
        /// State state = "FL";
        /// </summary>
        public static implicit operator State(string value)
        {
            return new State(value);
        }

        /// <summary>
        /// Allow direct assigment to string
        /// string str = state;
        /// </summary>
        public static implicit operator string(State state)
        {
            return state.Abbreviation;
        }
        #endregion cast operators
    }
}
