using System.Collections.Generic;

namespace Pargoon.Utility
{
    public static class ArrayUtility
    {
        public static string MergeToString<T>(this IEnumerable<T> collection, string separator = ",")
        {
            return string.Join(separator, collection);
        }
    }
}
