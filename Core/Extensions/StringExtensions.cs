using System.Collections.Generic;
using System.Linq;

namespace Swaksoft.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrBlank(this string input)
        {
            return string.IsNullOrEmpty(input) || input.Trim().Length == 0;
        }

        public static bool AreNullOrBlank(this IEnumerable<string> values)
        {
            if (!values.Any() || values == null)
            {
                return false;
            }

            return values.Aggregate(true, (current, value) => current & value.IsNullOrBlank());
        }
    }
}