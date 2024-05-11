using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static string EnsureStartsWith(this string source, string start)
        {
            if (source.StartsWith(start))
                return source;
            return start + source;
        }

        public static string EnsureEndsWith(this string source, string end)
        {
            if (source.EndsWith(end))
                return source;
            return source + end;
        }

        public static string RemoveLeadingChars(this string source, string start)
        {
            if (!source.StartsWith(start))
                return source;
            return start.TrimStart(source.ToCharArray());
        }

        public static string RemoveTrailingChars(this string source, string end)
        {
            if (!source.EndsWith(end))
                return source;
            return source.TrimEnd(end.ToCharArray());
        }

        public static bool HasAnyValue(this string str)
        {
            str = str != null ? str.Trim() : str;
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }
    }
}
