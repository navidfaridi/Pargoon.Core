// Ignore Spelling: Pargoon

using System;
using System.Security.Cryptography;
using System.Text;

namespace Pargoon.Utility;

public static class CECEnDecryption
{
    public static string EncryptStringToBase64(string plainText, string keyStr, string ivStr)
    {
        byte[] key = GenerateKey(keyStr);
        byte[] iv = GenerateIV(ivStr);

        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = key;
            rijAlg.IV = iv;
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;

            byte[] encryptedBytes;
            using (var encryptor = rijAlg.CreateEncryptor())
            {
                encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);
            }

            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public static string DecryptStringFromBase64(string cipherText, string keyStr, string ivStr)
    {
        byte[] key = GenerateKey(keyStr);
        byte[] iv = GenerateIV(ivStr);

        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = key;
            rijAlg.IV = iv;
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;

            byte[] encryptedBytes = Convert.FromBase64String(cipherText);
            string decryptedText;
            using (var decryptor = rijAlg.CreateDecryptor())
            {
                decryptedText = Encoding.UTF8.GetString(decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
            }

            return decryptedText;
        }
    }

    private static byte[] GenerateKey(string keyStr)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(keyStr, Encoding.UTF8.GetBytes(keyStr), 1000))
        {
            return deriveBytes.GetBytes(32); // 32 bytes = 256 bits
        }
    }

    private static byte[] GenerateIV(string ivStr)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(ivStr, Encoding.UTF8.GetBytes(ivStr), 1000))
        {
            return deriveBytes.GetBytes(16); // 16 bytes = 128 bits
        }
    }
}
