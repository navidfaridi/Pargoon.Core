using System.Text;

namespace Pargoon.Utility
{
    public class SimpleEncrypt
    {
        private static readonly string salt = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string key = ")(*&%^#$!@~QAZXSWEDCVFRTGBNHYUJM<IKOL>:?";

        public static string Encrypt(int number)
        {
            return Encrypt(number.ToString());
        }

        public static string Encrypt(string s)
        {
            StringBuilder encryptedString = new StringBuilder();
            foreach (char c in s)
            {
                int index = c - '0';
                if (index >= 0 && index < salt.Length)
                {
                    encryptedString.Append(salt[index]);
                    encryptedString.Append((int)key[index]);
                }
            }
            return encryptedString.ToString();
        }

        public static string Decrypt(string str)
        {
            StringBuilder decryptedString = new StringBuilder();
            foreach (char c in str)
            {
                int index = salt.IndexOf(c);
                if (index >= 0 && index < key.Length)
                {
                    decryptedString.Append(index);
                }
            }
            return decryptedString.ToString();
        }
    }

}
