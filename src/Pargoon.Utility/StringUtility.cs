using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pargoon.Utility
{
    public static class StringUtility
    {
        public static bool IsEnglishLetter(this string s)
        {
            var en = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            foreach (var c in s.ToCharArray())
            {
                if (!en.Any(x => x == c))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// بررسی نام کاربری، فقط می تواند اعداد وحروف انگلیسی باشد
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsValidUsername(this string username)
        {
            var en = "abcdefghijklmnopqrstuvwxyz_0123456789".ToCharArray();
            var s = username.ToLower();
            foreach (var c in s.ToCharArray())
            {
                if (!en.Any(x => x == c))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidMobileNumber(this string mobile)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(mobile))
            {
                try
                {
                    if (!mobile.StartsWith('0'))
                        mobile = "0" + mobile;
                    Regex reg = new Regex(RegXPattern.MobilePhone);
                    rs = reg.IsMatch(mobile);
                }
                catch (Exception)
                {
                    rs = false;
                }
            }
            return rs;
        }

        public static bool IsNumeric(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return false;
            bool rs = true;
            if (txt.Count(u => u == '.') > 1)
                return false;

            foreach (var c in txt.ToCharArray().Where(u => u != '.'))
            {
                if (c < '0' || c > '9')
                    rs = false;
            }
            return rs;
        }

        /// <summary>
        /// معتبر بودن ایمیل را چک می کند
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            bool rs = false;
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    Regex reg = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|ir|ac|biz|info|mobi|name|aero|asia|jobs|museum)\b");
                    rs = reg.IsMatch(email.ToLower());
                }
                catch (Exception)
                {
                    rs = false;
                }
            }
            return rs;
        }
        public static string FixEmail(this string email)
        {
            return email.Trim().Replace(" ", "").ToLower();
        }
        public static string StripTags(string HTML)
        {
            // Removes tags from passed HTML           
            if (string.IsNullOrEmpty(HTML))
                return string.Empty;
            Regex objRegEx = new Regex("<[^>]*>");

            return objRegEx.Replace(HTML, "");
        }
        public static string FixLength(this string str, int len, bool striphtml = true, string addEndStr = "...")
        {
            if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str))
            {
                var txt = str;
                if (striphtml)
                    txt = StripTags(txt);
                if (txt.Length > len)
                    txt = txt.Substring(0, len) + addEndStr;
                return txt;
            }
            return string.Empty;
        }

        /// <summary>
        /// Verify whether the specified string contains any characters, is empty, or just contains space.
        /// return true if specified string contains any characters except space
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreWhiteSpace"></param>
        /// <returns></returns>
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }



        /// <summary>
        /// fa-IR => current culture currency symbol => ریال
        /// 123456 => "123,123ریال"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCurrency(this int value)
        {

            return value.ToString("C0");
        }

        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }

        public static string En2Fa(this string str)
        {
            return str.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }

        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        public static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
        }

        public static string? NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }

        /// <summary>
        /// حذف کاراکترهای خطرناک در ارسال پارامتر به پایگاه داده
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveDangerousChars(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            string tmp = inputString.Replace("/", "").Replace("\\", "").Replace("--", "").Replace("=", "").Replace(">", "").Replace("<", "");
            tmp = tmp.Replace("#", "").Replace("%", "").Replace("'", "");
            tmp = tmp.Trim().TrimEnd().TrimStart();
            return tmp;
        }

        /// <summary>
        /// تبدیل یک سری حروف فارسی به یک کد استاندارد
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CorrectFarsiChars(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                string tmp = inputString.Replace("  ", " ").Trim();

                if (!string.IsNullOrEmpty(tmp))
                {
                    #region Correct 'ک'
                    tmp = tmp.Replace('\u0643', '\u06A9'); //ك 
                    tmp = tmp.Replace('\u06AA', '\u06A9'); //ڪ
                    tmp = tmp.Replace('\u06AB', '\u06A9'); //ګ
                    tmp = tmp.Replace('\u06AC', '\u06A9'); //ڬ
                    tmp = tmp.Replace('\u06AD', '\u06A9'); //ڭ
                    tmp = tmp.Replace('\u06AE', '\u06A9'); //ڮ
                    #endregion

                    #region Correct 'ي'
                    //tmp = tmp.Replace('\u0626', '\u064A'); //ئ
                    tmp = tmp.Replace('\u06CC', 'ی'); //ی  
                    tmp = tmp.Replace('\u06CD', 'ی'); //ۍ
                    tmp = tmp.Replace('\u06CE', 'ی'); //ێ
                    tmp = tmp.Replace('\u06D0', 'ی'); //ې
                    tmp = tmp.Replace('\u06D1', 'ی'); //ۑ
                    tmp = tmp.Replace('ي', 'ی');
                    #endregion

                    #region Correct 'گ'
                    tmp = tmp.Replace('\u06B0', '\u06AF'); //ڰ 
                    tmp = tmp.Replace('\u06B1', '\u06AF'); //ڱ
                    tmp = tmp.Replace('\u06B2', '\u06AF'); //ڲ
                    tmp = tmp.Replace('\u06B3', '\u06AF'); //ڳ
                    tmp = tmp.Replace('\u06B4', '\u06A9'); //ڴ
                    #endregion
                }
                return tmp;
            }
            return inputString;
        }

        /// <summary>
        /// تبدیل یک  حرف فارسی به یک کد استاندارد
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static char CorrectFarsiChar(this char inputChar)
        {
            if (!string.IsNullOrEmpty(inputChar.ToString()))
            {
                string tmp = (inputChar.ToString().RemoveDangerousChars());

                if (!string.IsNullOrEmpty(tmp))
                {
                    #region Correct 'ک'
                    tmp = tmp.Replace('\u0643', '\u06A9'); //ك 
                    tmp = tmp.Replace('\u06AA', '\u06A9'); //ڪ
                    tmp = tmp.Replace('\u06AB', '\u06A9'); //ګ
                    tmp = tmp.Replace('\u06AC', '\u06A9'); //ڬ
                    tmp = tmp.Replace('\u06AD', '\u06A9'); //ڭ
                    tmp = tmp.Replace('\u06AE', '\u06A9'); //ڮ
                    #endregion

                    #region Correct 'ي'                    
                    tmp = tmp.Replace('\u06CC', 'ی'); //ی
                    tmp = tmp.Replace('\u06CD', 'ی'); //ۍ
                    tmp = tmp.Replace('\u06CE', 'ی'); //ێ
                    tmp = tmp.Replace('\u06D0', 'ی'); //ې
                    tmp = tmp.Replace('\u06D1', 'ی'); //ۑ
                    tmp = tmp.Replace('ي', 'ی');
                    #endregion

                    #region Correct 'گ'
                    tmp = tmp.Replace('\u06B0', '\u06AF'); //ڰ 
                    tmp = tmp.Replace('\u06B1', '\u06AF'); //ڱ
                    tmp = tmp.Replace('\u06B2', '\u06AF'); //ڲ
                    tmp = tmp.Replace('\u06B3', '\u06AF'); //ڳ
                    tmp = tmp.Replace('\u06B4', '\u06A9'); //ڴ
                    #endregion
                }
                return Convert.ToChar(tmp[0]);
            }
            return inputChar;
        }

        /// <summary>
        /// معتبر بودن کد ملی را چک می کند
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <returns></returns>
        public static bool IsValidNationalCode(this string nationalCode)
        {

            if (!string.IsNullOrEmpty(nationalCode))
            {
                if (nationalCode[0] == 'F' || nationalCode.Length == 11)
                    return true;

                if (nationalCode.Length < 10)
                {
                    while (nationalCode.Length == 10)
                        nationalCode = "0" + nationalCode;
                }

                if (!Regex.IsMatch(nationalCode, RegXPattern.NationalCode))
                    return false;

                if (nationalCode == "1111111111" || nationalCode == "0000000000" || nationalCode == "2222222222" || nationalCode == "3333333333" || nationalCode == "4444444444" ||
                    nationalCode == "5555555555" || nationalCode == "6666666666" || nationalCode == "7777777777" || nationalCode == "8888888888" || nationalCode == "9999999999")
                {
                    return false;
                }
                int c = int.Parse((nationalCode[9]).ToString());
                int n = int.Parse(nationalCode[0].ToString()) * 10 +
                    int.Parse(nationalCode[1].ToString()) * 9 +
                     int.Parse(nationalCode[2].ToString()) * 8 +
                     int.Parse(nationalCode[3].ToString()) * 7 +
                    int.Parse(nationalCode[4].ToString()) * 6 +
                     int.Parse(nationalCode[5].ToString()) * 5 +
                     int.Parse(nationalCode[6].ToString()) * 4 +
                     int.Parse(nationalCode[7].ToString()) * 3 +
                     int.Parse(nationalCode[8].ToString()) * 2;
                int r = n - (n / 11) * 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
