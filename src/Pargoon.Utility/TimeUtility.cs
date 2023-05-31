using System;

namespace Pargoon.Utility
{
    public static class TimeUtility
    {
        /// <summary>
        /// convert a time string to a 4 digit number, for example 11:04 -> 1104
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int ToIntTime(this string s)
        {
            return (s.GetHour() * 100) + s.GetMinute();
        }

        /// <summary>
        /// Extracting the hour number from a string containing hours and minutes
        /// for example :
        /// 13:15 -> 13
        /// 03:45 -> 3
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetHour(this string s)
        {
            string[] sd = s.ToUpper().Trim().Split(':');
            int hour = 0;
            if (sd.Length > 0)
            {
                hour = sd[0].ToInt();
                if (s.Contains("PM"))
                    hour += 12;
            }
            return hour;
        }

        /// <summary>
        /// Extracting the minute number from a string containing hours and minutes
        /// for example :
        /// 13:15 -> 15
        /// 05:34 -> 34
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetMinute(this string s)
        {
            string[] sd = s
                .ToLower()
                .Replace("am",String.Empty)
                .Replace("pm",String.Empty)
                .Trim()
                .Split(':');
            int min = 0;
            if (sd.Length > 1)
            {

                min= sd[1].ToInt();
                
            }
            return min;
        }

        /// <summary>
        /// Convert a number specifying a value in seconds to a string of hours, minutes and seconds
        /// for example :
        /// 61   -> 00:01:01
        /// 3665 -> 01:01:05
        /// </summary>
        /// <param name="s">hh:mm:ss</param>
        /// <returns></returns>
        public static string GetFullTime(this int seconds)
        {
            var s = seconds % 60;
            seconds = seconds / 60;
            var m = seconds % 60;
            var h = seconds / 60;
            return $"{h:00}:{m:00}:{s:00}";
        }

        /// <summary>
        /// convert a Unix MiliSecond number to DateTime
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromUnixMiliseconds(this long ms)
        {
            var sdt = ms / 1000;
            var udt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var xdt = DateTime.Now.AddSeconds(sdt - udt);
            return xdt;
        }
    }
}
