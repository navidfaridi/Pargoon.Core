using System.Collections.Generic;

namespace Pargoon.Utility
{
    public static class ArrayUtility
    {
        public static string MergeToString(this int[] a, string seperator = ",")
        {
            string res = string.Empty;
            foreach (var item in a)
            {
                if (res.Length > 0)
                    res = res + seperator;
                res += item.ToString();
            }
            return res;
        }

        public static string MergeToString(this string[] a, string seperator = ",")
        {
            string res = string.Empty;
            foreach (var item in a)
            {
                if (res.Length > 0)
                    res = res + seperator;
                res += item.ToString();
            }
            return res;
        }
        public static string MergeToString(this List<string> a, string seperator = ",")
        {
            return a.ToArray().MergeToString(seperator);
        }
        public static string MergeToString(this List<int> a, string seperator = ",")
        {
            string res = string.Empty;
            foreach (var item in a)
            {
                if (res.Length > 0)
                    res = res + seperator;
                res += item.ToString();
            }
            return res;
        }
    }
}
