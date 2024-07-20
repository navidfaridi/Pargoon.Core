using System;
using System.Linq;
using System.Text;

namespace Pargoon.Utility;

public class RandomUtils
{
    static Random random = new Random();
    public static int RandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }
    static string randomStringRange = "abcdefghjkmnpqrstuvwxyz23456789";
    static string randomPasswordRange = "abcdefghjkmnpqrstuvwxyzABCDEFGHJLKMNPQRSTUVWXYZ23456789";
    public static string RandomString(int len = 6)
    {
        string res = string.Empty;
        while (res.Length < len)
        {
            var r = RandomNumber(0, randomStringRange.Length);
            res += randomStringRange[r];
        }
        return res;
    }

    public static string RandomPassword(int len = 6)
    {
        string res = string.Empty;
        while (res.Length < len)
        {
            var r = RandomNumber(0, randomPasswordRange.Length);
            res += randomPasswordRange[r];
        }
        return res;
    }

    public static string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        var dontwantChars = "iIloO0uvVU".ToArray();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            while (dontwantChars.Contains(ch))
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();

        return builder.ToString().ToUpper();
    }

    public static string GenerateRandomCode(int len = 6)
    {
        string s = "";
        while (s.Length < len)
        {
            int tas = RandomNumber(0, 10);
            if (tas > 0 && tas < 10)
                s = s + tas.ToString();
        }
        return s;
    }
}
