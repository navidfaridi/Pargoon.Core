namespace Pargoon.Utility;

public class AESEncryption
{
    public static string DefaultEncryptionKey = "navid*@#*#faridi";

    public static string Encrypt(string text, string version)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        if (string.IsNullOrEmpty(version))
            return CECEnDecryption.EncryptStringToBase64(text, DefaultEncryptionKey, DefaultEncryptionKey);

        string key = GetVersionKey(version);
        string iv = GetIVKey(version);
        if (!string.IsNullOrEmpty(key))
            return CECEnDecryption.EncryptStringToBase64(text, key, iv);

        return string.Empty;
    }

    private static string GetVersionKey(string version)
    {
        if (!string.IsNullOrEmpty(version) && version.Length > 32)
            return version.Substring(0, 32);

        return DefaultEncryptionKey;
    }

    private static string GetIVKey(string version)
    {
        if (!string.IsNullOrEmpty(version) && version.Length > 16)
            return version.Substring(0, 16);

        return DefaultEncryptionKey;
    }

    public static string EncriptForUrl(string text, string version)
    {
        string encryptedText = Encrypt(text, version);
        if (string.IsNullOrEmpty(encryptedText))
            return string.Empty;

        return encryptedText
            .Replace("+", "-aplus-")
            .Replace("/", "-bdivid-")
            .Replace("\\", "-cfolder-")
            .Replace("'", "-quote-")
            .Replace("<", "-lt-")
            .Replace(">", "-gt-")
            .Replace("=", "-equal-")
            .Replace("\"", "-dqoute-");
    }

    public static string DecryptForUrl(string text, string version)
    {
        string decryptedText = text
            .Replace("-aplus-", "+")
            .Replace("-bdivid-", "/")
            .Replace("-cfolder-", "\\")
            .Replace("-quote-", "'")
            .Replace("-lt-", "<")
            .Replace("-gt-", ">")
            .Replace("-equal-", "=")
            .Replace("-dqoute-", "\"");

        return Decrypt(decryptedText, version);
    }

    public static string Decrypt(string text, string version)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        string key = string.Empty;
        string iv = string.Empty;
        if (!string.IsNullOrEmpty(version))
        {
            key = GetVersionKey(version);
            iv = GetIVKey(version);
        }
        else
        {
            key = DefaultEncryptionKey;
            iv = key;
        }

        if (!string.IsNullOrEmpty(key))
            return CECEnDecryption.DecryptStringFromBase64(text, key, iv);

        return string.Empty;
    }
}
