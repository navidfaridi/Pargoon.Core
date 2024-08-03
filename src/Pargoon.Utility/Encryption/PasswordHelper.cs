// Ignore Spelling: Pargoon passphrase

using System;
using System.Security.Cryptography;
using System.Text;

namespace Pargoon.Utility;

public static class PasswordHelper
{
    public static string EncryptPasswordMd5(string pass)
    {
        using (var md5 = MD5.Create())
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(pass);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            StringBuilder sb = new StringBuilder(encodedBytes.Length * 2);
            for (int i = 0; i < encodedBytes.Length; i++)
            {
                sb.Append(encodedBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }

    public static string EncryptString(string message, string passphrase)
    {
        byte[] encryptedBytes;
        using (var aes = Aes.Create())
        {
            aes.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(message);

            ICryptoTransform encryptor = aes.CreateEncryptor();
            encryptedBytes = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

            aes.Clear();
        }

        var res = Convert.ToBase64String(encryptedBytes);
        return res.Replace("-", "_dash_");
    }

    public static string DecryptString(string message, string passphrase)
    {
        byte[] decryptedBytes;
        message = message.Replace("_dash_", "-");
        using (var aes = Aes.Create())
        {
            aes.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            byte[] dataToDecrypt = Convert.FromBase64String(message);

            ICryptoTransform decryptor = aes.CreateDecryptor();
            decryptedBytes = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);

            aes.Clear();
        }

        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public static string EncryptWithExpiration(string message, string passphrase, TimeSpan expiration)
    {
        string encryptedMessage = EncryptString(message, passphrase);
        var ticks = DateTime.UtcNow.Add(expiration).Ticks;
        var encryptedTicks = EncryptString(ticks.ToString(), passphrase);
        string encryptedWithExpiration = $"{encryptedMessage}-{encryptedTicks}";

        return encryptedWithExpiration;
    }

    public static string DecryptWithExpiration(string message, string passphrase)
    {
        string[] parts = message.Split('-');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid encrypted message format.");
        }

        string encryptedMessage = parts[0];
        var ticks = DecryptString(parts[1], passphrase);
        long expirationTicks = long.Parse(ticks);
        DateTime expirationTime = new DateTime(expirationTicks, DateTimeKind.Utc);

        if (DateTime.UtcNow > expirationTime)
        {
            throw new InvalidOperationException("The encrypted message has expired.");
        }

        return DecryptString(encryptedMessage, passphrase);
    }
}