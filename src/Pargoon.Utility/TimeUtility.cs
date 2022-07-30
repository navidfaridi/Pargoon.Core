using System;

namespace Pargoon.Utility
{
    public static class TimeUtility
    {
        /// <summary>
        /// تبدیل یک رشته حاوی ساعت به یک عدد صحیح چهار رقمی
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int ToIntTime(this string s)
        {
            return (s.GetHour() * 100) + s.GetMinute();
        }

        /// <summary>
        /// استخراج عدد ساعت از یک رشته حاوی ساعت و دقیقه
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetHour(this string s)
        {
            string[] sd = s.Split(':');
            if (sd.Length > 0)
                return sd[0].ToInt();
            else return 0;
        }

        /// <summary>
        /// استخراج عدد دقیقه از یک رشته حاوی ساعت و دقیقه
        /// </summary>
        /// <param name="s">hh:mm</param>
        /// <returns></returns>
        public static int GetMinute(this string s)
        {
            string[] sd = s.Split(':');
            if (sd.Length > 1)
                return sd[1].ToInt();
            else return 0;
        }

        /// <summary>
        /// نمایش زمان به صورت ساعت، دقیقه و ثانیه
        /// </summary>
        /// <param name="s">hh:mm:ss</param>
        /// <returns></returns>
        public static string GetFullTime(this int seconds)
        {
            var s = seconds % 60;
            seconds = seconds / 60;
            var m = seconds % 60;
            var h = seconds / 60;
            //var h = seconds % 24;
            //return h.ToString() + ":" + m.ToString() + ":" + s.ToString();
            return $"{h:00}:{m:00}:{s:00}";
        }

        public static DateTime GetDateTimeFromUnixMiliseconds(long ms)
        {
            var sdt = ms / 1000;
            var udt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var xdt = DateTime.Now.AddSeconds(sdt - udt);
            return xdt;
        }
    }
}
