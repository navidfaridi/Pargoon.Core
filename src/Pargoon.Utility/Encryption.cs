using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pargoon.Utility
{
    public static class PasswordHelper
    {
        public static string EncryptPasswordMd5(string pass) //Encrypt using MD5   
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message">Data to Encrypt</param>
        /// <param name="Passphrase"></param>
        /// <returns></returns>
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
    public class CECEnDecryption
    {
        public static string encryptStrAndToBase64(string enStr, string keyStr, string ivStr)
        {
            byte[] bytes = encrypt(enStr, keyStr, ivStr);
            return Convert.ToBase64String(bytes, Base64FormattingOptions.None);
        }

        public static string encryptStr(string enStr, string keyStr, string ivStr)
        {
            byte[] bytes = encrypt(enStr, keyStr, ivStr);
            return Encoding.UTF8.GetString(bytes);// Convert.ToBase64String(bytes);
        }
        public static string decryptStrAndFromBase64(string deStr, string keyStr, string ivStr)
        {
            return decrypt(Convert.FromBase64String(deStr), keyStr, ivStr);
        }

        public static string decryptStr(string deStr, string keyStr, string ivStr)
        {
            return decrypt(Encoding.UTF8.GetBytes(deStr), keyStr, ivStr);
        }

        private static byte[] encrypt(string p, string keyStr, string ivStr)
        {
            if (keyStr.Length < 32)
                while (keyStr.Length < 32)
                    keyStr = keyStr + "1";

            if (ivStr.Length < 16)
                while (ivStr.Length < 16)
                    ivStr = ivStr + "2";
            return EncryptStringToBytes(p, Encoding.UTF8.GetBytes(keyStr), Encoding.UTF8.GetBytes(ivStr));
        }

        private static string decrypt(byte[] p, string keyStr, string ivStr)
        {
            if (keyStr.Length < 32)
                while (keyStr.Length < 32)
                    keyStr = keyStr + "1";

            if (ivStr.Length < 16)
                while (ivStr.Length < 16)
                    ivStr = ivStr + "2";
            var key = Encoding.UTF8.GetBytes(keyStr);
            var iv = Encoding.UTF8.GetBytes(ivStr);
            return DecryptStringFromBytes(p, key, iv);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                try
                {
                    rijAlg.Mode = CipherMode.CBC;
                    rijAlg.Padding = PaddingMode.PKCS7;
                    rijAlg.FeedbackSize = 128;

                    rijAlg.Key = key;
                    rijAlg.IV = iv;

                    // Create a decrytor to perform the stream transform.
                    var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                    return encrypted;
                }
                catch (Exception ex)
                {

                }

            }

            // Return the encrypted bytes from the memory stream.
            return null;
        }
    }
    public class AESEncryption
    {
        #region Encryption

        public static string DefaultEncryptionkey = "navid*@#*#faridi";
        public static bool EncryptionMode = true;

        public static string Encrypt(string text, string version)
        {
            try
            {
                if (!EncryptionMode)
                    return text;

                if (string.IsNullOrEmpty(version))
                {
                    return CECEnDecryption.encryptStrAndToBase64(text, DefaultEncryptionkey, DefaultEncryptionkey);
                }
                string key = GetVersionKey(version);
                string ivk = GetIVKey(version);
                if (!string.IsNullOrEmpty(key))
                    return CECEnDecryption.encryptStrAndToBase64(text, key, ivk);
            }
            catch (Exception ex) { }

            return "";

        }

        private static string GetVersionKey(string version)
        {
            if (!string.IsNullOrEmpty(version))
                if (version.Length > 32)
                    return version.Substring(0, 32);

            return DefaultEncryptionkey;
        }

        private static string GetIVKey(string version)
        {
            if (!string.IsNullOrEmpty(version))
                if (version.Length > 16)
                    return version.Substring(0, 16);
            return DefaultEncryptionkey;
        }

        public static string EncriptForUrl(string text, string version)
        {
            var x = Encrypt(text, version);
            x = x.Replace("+", "-aplus-");
            x = x.Replace("/", "-bdivid-");
            x = x.Replace("\\", "-cfolder-");
            x = x.Replace("'", "-quote-");
            x = x.Replace("<", "-lt-");
            x = x.Replace(">", "-gt-");
            x = x.Replace("=", "-equal-");
            x = x.Replace("\"", "-dqoute-");

            return x;
        }
        public static string DecryptForUrl(string text, string version)
        {
            var x = text;
            x = x.Replace("-aplus-", "+");
            x = x.Replace("-bdivid-", "/");
            x = x.Replace("-cfolder-", "\\");

            x = x.Replace("-quote-", "'");
            x = x.Replace("-lt-", "<");
            x = x.Replace("-gt-", ">");
            x = x.Replace("-equal-", "=");
            x = x.Replace("-dqoute-", "\"");

            return Decrypt(x, version);
        }
        public static string Decrypt(string text, string version)
        {
            try
            {
                if (!EncryptionMode)
                    return text;

                if (!string.IsNullOrEmpty(text))
                {
                    string key = string.Empty;
                    string ivk = string.Empty;
                    if (!string.IsNullOrEmpty(version))
                    {
                        key = GetVersionKey(version);
                        ivk = GetIVKey(version);
                    }
                    else
                    {
                        key = DefaultEncryptionkey;
                        ivk = key;
                    }

                    if (!string.IsNullOrEmpty(key))
                        return CECEnDecryption.decryptStrAndFromBase64(text, key, ivk);
                }
            }
            catch (Exception ex) { }

            return "";
        }
        #endregion
    }

    public class SimpleEncrypt
    {
        string salt = "abcdefghijklmnopqrstuvwxyz";
        string key = ")(*&%^#$!@~QAZXSWEDCVFRTGBNHYUJM<IKOL>:?";
        bool ValidateDefaults()
        {
            var s = salt.ToArray().Distinct().Count();
            if (s != salt.Length)
                return false;

            s = key.ToArray().Distinct().Count();
            if (s != key.Length)
                return false;

            return true;
        }
        public static string Encrypt(int number)
        {
            return Encrypt(number.ToString());
        }
        public static string Encrypt(string s)
        {
            var se = new SimpleEncrypt();
            if (!se.ValidateDefaults())
                return s;

            var ca = s.ToCharArray();
            var sa = "";
            for (var i = 0; i < ca.Length; i++)
            {
                var j = ((int)ca[i]) - 48;
                sa = sa + se.salt[j] + ((int)se.key[j]).ToString();
            }
            return sa;
        }

        public static string Decrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            var se = new SimpleEncrypt();
            var s = "";
            foreach (var c in str.ToCharArray())
            {
                if (se.salt.Contains(c))
                    s = s + se.salt.IndexOf(c).ToString();
            }
            return s;
        }
    }
}
