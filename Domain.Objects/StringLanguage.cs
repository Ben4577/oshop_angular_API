using Domain.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Objects
{
    public static class StringLanguage
    {
        public static bool IsNotNullOrWhiteSpace(this string[] strings)
        {
            var allHaveAValue = strings.ToList().TrueForAll(s => !s.IsNotNullOrWhiteSpace());
            return allHaveAValue;
        }

        public static bool IsNullEmptyOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool IsValidGuid(this string s)
        {
            Guid guid;
            return s.IsNotNullOrWhiteSpace() && Guid.TryParse(s, out guid);
        }

        public static bool IsEqualTo(this string first, string second, bool ignoreCaseCulture = true)
        {
            if (ignoreCaseCulture) return string.Equals(first, second, StringComparison.InvariantCultureIgnoreCase);
            return string.Equals(first, second);
        }
    }
}
