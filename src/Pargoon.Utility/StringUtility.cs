using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pargoon.Utility;

public static class StringUtility
{
	private static readonly HashSet<char> EnglishLetters = new HashSet<char>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

	public static bool IsEnglishLetter(this string s)
	{
		return s.All(c => EnglishLetters.Contains(c));
	}

	/// <summary>
	/// Checks if the username contains only English letters and digits.
	/// </summary>
	/// <param name="username">The username to validate.</param>
	/// <returns>True if the username is valid; otherwise, false.</returns>
	public static bool IsValidUsername(this string username)
	{
		var allowedChars = "abcdefghijklmnopqrstuvwxyz_0123456789";
		var lowercaseUsername = username.ToLower();

		return lowercaseUsername.All(c => allowedChars.Contains(c));
	}

	public static bool IsValidMobileNumber(this string mobile)
	{
		if (string.IsNullOrEmpty(mobile))
			return false;

		if (!mobile.StartsWith('0'))
			mobile = "0" + mobile;

		return Regex.IsMatch(mobile, RegXPattern.MobilePhone);
	}

	public static bool IsNumeric(this string txt)
	{
		if (string.IsNullOrEmpty(txt))
			return false;

		if (txt.Count(u => u == '.') > 1)
			return false;

		return txt.Replace(".", "").All(c => c >= '0' && c <= '9');
	}

	/// <summary>
	/// Checks the validity of an email address.
	/// </summary>
	/// <param name="email">The email address to validate.</param>
	/// <returns>True if the email address is valid; otherwise, false.</returns>
	public static bool IsValidEmail(this string email)
	{
		if (string.IsNullOrEmpty(email))
			return false;

		return Regex.IsMatch(email, @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|ir|ac|biz|info|mobi|name|aero|asia|jobs|museum)\b", RegexOptions.IgnoreCase);
	}

	public static string FixEmail(this string email)
	{
		return email.Trim().Replace(" ", "").ToLower();
	}

	public static string StripTags(this string html)
	{
		if (string.IsNullOrEmpty(html))
			return string.Empty;

		return Regex.Replace(html, "<[^>]*>", string.Empty);
	}

	public static string FixLength(this string str, int length, bool stripHtml = true, string addEndStr = "...")
	{
		if (string.IsNullOrWhiteSpace(str))
			return string.Empty;

		var text = stripHtml ? StripTags(str) : str;

		if (text.Length > length)
			return text.Substring(0, length) + addEndStr;

		return text;
	}

	/// <summary>
	/// Checks if the specified string has any content, is not empty, or contains only whitespace.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="ignoreWhiteSpace">True to ignore whitespace characters; false to consider them.</param>
	/// <returns>True if the string has any content; otherwise, false.</returns>
	public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
	{
		return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
	}

	public static int ToInt(this string value)
	{
		return int.Parse(value);
	}

	public static decimal ToDecimal(this string value)
	{
		return decimal.Parse(value);
	}

	public static string ToNumeric(this int value)
	{
		return value.ToString("N0");
	}

	public static string ToNumeric(this decimal value)
	{
		return value.ToString("N0");
	}

	/// <summary>
	/// Converts the value to a currency string using the current culture's currency symbol.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <returns>The value as a currency string.</returns>
	public static string ToCurrency(this int value)
	{
		return value.ToString("C0");
	}

	public static string ToCurrency(this decimal value)
	{
		return value.ToString("C0");
	}

	public static string ToEnglishNumber(this string txt)
	{
		if (string.IsNullOrEmpty(txt))
			return string.Empty;

		return txt.Replace("۱", "1")
				  .Replace("۲", "2")
				  .Replace("۳", "3")
				  .Replace("۴", "4")
				  .Replace("۵", "5")
				  .Replace("۶", "6")
				  .Replace("۷", "7")
				  .Replace("۸", "8")
				  .Replace("۹", "9")
				  .Replace("۰", "0")
				  .Trim();
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
				  .Replace("ھ", "ه");
	}

	public static string CleanString(this string str)
	{
		return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
	}

	public static string NullIfEmpty(this string str)
	{
		return string.IsNullOrEmpty(str) ? null : str;
	}

	/// <summary>
	/// Removes dangerous characters from a string before sending it to the database.
	/// </summary>
	/// <param name="inputString">The input string to sanitize.</param>
	/// <returns>The sanitized string.</returns>
	public static string RemoveDangerousChars(this string inputString)
	{
		if (string.IsNullOrEmpty(inputString))
			return string.Empty;

		return inputString.Replace("/", "").Replace("\\", "").Replace("--", "").Replace("=", "").Replace(">", "").Replace("<", "")
						  .Replace("#", "").Replace("%", "").Replace("'", "")
						  .Trim();
	}

	/// <summary>
	/// Converts a series of Persian characters to a standardized code.
	/// </summary>
	/// <param name="inputString">The input string to convert.</param>
	/// <returns>The converted string.</returns>
	public static string CorrectFarsiChars(this string inputString)
	{
		if (string.IsNullOrEmpty(inputString))
			return inputString;

		string tmp = inputString.Replace("  ", " ").Trim();

		if (string.IsNullOrEmpty(tmp))
			return tmp;

		tmp = tmp.Replace('\u0643', '\u06A9') //ك
				 .Replace('\u06AA', '\u06A9') //ڪ
				 .Replace('\u06AB', '\u06A9') //ګ
				 .Replace('\u06AC', '\u06A9') //ڬ
				 .Replace('\u06AD', '\u06A9') //ڭ
				 .Replace('\u06AE', '\u06A9') //ڮ
				 .Replace('\u06CC', 'ی') //ی
				 .Replace('\u06CD', 'ی') //ۍ
				 .Replace('\u06CE', 'ی') //ێ
				 .Replace('\u06D0', 'ی') //ې
				 .Replace('\u06D1', 'ی') //ۑ
				 .Replace('ي', 'ی')
				 .Replace('\u06B0', '\u06AF') //ڰ
				 .Replace('\u06B1', '\u06AF') //ڱ
				 .Replace('\u06B2', '\u06AF') //ڲ
				 .Replace('\u06B3', '\u06AF') //ڳ
				 .Replace('\u06B4', '\u06A9'); //ڴ

		return tmp;
	}

	/// <summary>
	/// Converts a single Persian character to a standardized code.
	/// </summary>
	/// <param name="inputChar">The input character to convert.</param>
	/// <returns>The converted character.</returns>
	public static char CorrectFarsiChar(this char inputChar)
	{
		string tmp = inputChar.ToString().RemoveDangerousChars();

		if (string.IsNullOrEmpty(tmp))
			return inputChar;

		tmp = tmp.Replace('\u0643', '\u06A9') //ك
				 .Replace('\u06AA', '\u06A9') //ڪ
				 .Replace('\u06AB', '\u06A9') //ګ
				 .Replace('\u06AC', '\u06A9') //ڬ
				 .Replace('\u06AD', '\u06A9') //ڭ
				 .Replace('\u06AE', '\u06A9') //ڮ
				 .Replace('\u06CC', 'ی') //ی
				 .Replace('\u06CD', 'ی') //ۍ
				 .Replace('\u06CE', 'ی') //ێ
				 .Replace('\u06D0', 'ی') //ې
				 .Replace('\u06D1', 'ی') //ۑ
				 .Replace('\u06B0', '\u06AF') //ڰ
				 .Replace('\u06B1', '\u06AF') //ڱ
				 .Replace('\u06B2', '\u06AF') //ڲ
				 .Replace('\u06B3', '\u06AF') //ڳ
				 .Replace('\u06B4', '\u06A9'); //ڴ

		return Convert.ToChar(tmp[0]);
	}

	/// <summary>
	/// Checks the validity of an Iranian national code (Melli Code).
	/// </summary>
	/// <param name="nationalCode">The national code to validate.</param>
	/// <returns>True if the national code is valid; otherwise, false.</returns>
	public static bool IsValidNationalCode(this string nationalCode)
	{
		if (string.IsNullOrEmpty(nationalCode))
			return false;

		if (nationalCode[0] == 'F' || nationalCode.Length == 11)
			return true;

		if (nationalCode.Length < 10)
			nationalCode = nationalCode.PadLeft(10, '0');

		if (!Regex.IsMatch(nationalCode, RegXPattern.NationalCode))
			return false;

		if (nationalCode == "1111111111" || nationalCode == "0000000000" || nationalCode == "2222222222" || nationalCode == "3333333333" || nationalCode == "4444444444" ||
			nationalCode == "5555555555" || nationalCode == "6666666666" || nationalCode == "7777777777" || nationalCode == "8888888888" || nationalCode == "9999999999")
		{
			return false;
		}

		int c = int.Parse(nationalCode[9].ToString());
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

		return (r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r);
	}

	public static string RemoveComma(this string s)
	{
		return string.IsNullOrEmpty(s) ? "" : s.Replace(",", "");
	}
	public static string RemoveDash(this string s)
	{
		return string.IsNullOrEmpty(s) ? "" : s.Replace("-", "");
	}
	public static string? ToLowerFirstChar(this string? str)
	{
		if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
			return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

		return str;
	}

	public static string? ToUpperFirstChar(this string? str)
	{
		if (!string.IsNullOrEmpty(str) && char.IsLower(str[0]))
			return str.Length == 1 ? char.ToUpper(str[0]).ToString() : char.ToUpper(str[0]) + str[1..];

		return str;
	}
}
