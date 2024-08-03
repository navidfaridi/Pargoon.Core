using System.Text.RegularExpressions;

namespace Pargoon.Utility;

public static class RegXPattern
{

    public const string username = "/^[a-z0-9_-]{3,16}$/";
    public const string password = "/^[a-z0-9_-]{6,18}$/";
    public const string Hex = "/^#?([a-f0-9]{6}|[a-f0-9]{3})$/";
    public const string Email = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
    //^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$
    //^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$
    public const string url = @"^http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$";
    public const string IpAddress = @"/^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/";
    //(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)
    public const string DisallowCertainChar = @"^[^'<>?%!\s]{1,20}$";
    public const string Time = @"^(|(0\d)|(1[0-2])):(([0-5]\d)):(([0-5]\d))\s([AP]M)$";
    //^([0-1]?\d|2[0-3]):([0-5]\d)$
    //^([0-9]|0[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9])$
    public const string Date = @"((19|20)[0-9]{2})-(([1-9])|(0[1-9])|(1[0-2]))-((3[0-1])|([0-2][0-9])|([0-9]))";
    public const string PersianDate = @"((13)[0-9]{2})/(([1-9])|(0[1-9])|(1[0-2]))/((3[0-1])|([0-2][0-9])|([0-9]))";
    public const string onlyOneSpace = @"([a-zA-Z]{1}[a-zA-Z]*[\s]{0,1}[a-zA-Z])+([\s]{0,1}[a-zA-Z]+)";
    public const string Anyinteger = @"^[-+]?\d*$";
    public const string DecimalPointOne = @"^[-+]?\d+(\.\d)?$";
    //^-?\d+([^.,])?$
    public const string UnsighnedFloatNumber = @"^\d*\.?\d*$";
    public const string SignedFloatNumber = @"^[-+]?\d*\.?\d*$";
    public const string SignedInt = @"^(\+|-)?\d+$";
    public const string DateWithSlash = @"^\d{1,2}\/\d{1,2}\/\d{4}$";
    public const string between1to999 = @"^[1-9][0-9]{0,2}$";
    public const string MobilePhone = @"^09[0|1|2|3|9][0-9]{8}$";
    public const string FixPhone = @"^0[1-9][0-9]{9}$";
    //@"09(1\d|3[1-9])-\d{3}-\d{4}$";        

    public const string PersianAlphabet = @"^[\u0600-\u06ff\s]+$|[\u0750-\u077f\s]+$|[\ufb50-\ufc3f\s]+$|[\ufe70-\ufefc\s]+$|[\u06cc\s]+$|[\u067e\s]+$|[\u06af\s]$|[\u0691\s]+$|^$";
    public const string PersianAlphabetAndNumber = @"^[\u0600-\u06ff0-9\s]+$|[\u0750-\u077f0-9\s]+$|[\ufb50-\ufc3f0-9\s]+$|[\ufe70-\ufefc0-9\s]+$|[\u06cc0-9\s]+$|[\u067e0-9\s]+$|[\u06af0-9\s]$|[\u06910-9\s]+$|^$";

    public const string EnglishAlphabetAndNumber = @"^([A-Za-z0-9]\s?)+([,]\s?([A-Za-z0-9]\s?)+)*$";
    public const string EnglishAlphabet = @"^([a-zA-Z])*$";
    /// <summary>
    /// شماره شبا می تواند 24 رقم عدد صحیح است
    /// </summary>
    public const string Sheba = @"[0-9]{24}";
    /// <summary>
    /// شماره ملی ده رقم عدد صحیح است
    /// </summary>
    public const string NationalCode = @"[0-9]{10}";
    /// <summary>
    /// شماره ملی برای اتباع ایرانی ده رقم و برای اتباع خارجی 11 رقم و عدد صحیح است
    /// </summary>
    //public const string NationalsCode = @"[0-9]{10,11}";
    public const string NationalAndSSIDCode = @"^[0-9]{10,11}$";

    /// <summary>
    /// کد پستی ده رقم عدد است
    /// </summary>
    public const string IranPostalCode = @"[1-9][0-9]{9}";
    /// <summary>
    /// شماره سریال شناسنامه 6 رقم عدد صحیح
    /// [0-9]{6}  
    /// با توجه به اینکه سریال اتباع افغانی شش عدد صفر خورده پترن به این شکل در نظر گرفته شد
    /// چنانچه فقط ایرانی مد نظر باشد باید پترن به شکل زیر باشد
    /// [1-9][0-9]{5}
    /// </summary>
    public const string CertificateSN = @"[0-9]{6}";
    //
    public const string PersonCode = @"[1-9][0-9]{8}";

    public static bool Match(string Text, string sPattern)
    {
        return Regex.IsMatch(Text, sPattern);
    }

    public static string[] invalidMobiles = { "09000000000", "09111111111", "09222222222", "09333333333", "09444444444", "09555555555"
                ,"09666666666", "09777777777", "09888888888", "09999999999", "09123456789", "09987654321", "09123123123", "09212121212"
                ,"09121212121", "09131313131", "09141414141", "09151515151", "09161616161", "09171717171", "09181818181", "09191919191"};
}
