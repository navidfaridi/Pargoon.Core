namespace Pargoon.Utility;

public static class ConvertUtility
{

	public static int ToInt(this object s)
	{
		if (s == null)
			return 0;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return 0;

		var valid = int.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : 0;
	}

	public static int? ToIntNull(this object? s)
	{

		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = int.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : null;
	}

	public static long ToLong(this object s)
	{
		if (s == null)
			return 0;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return 0;

		var valid = long.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : 0;
	}

	public static long? ToLongNull(this object? s)
	{

		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = long.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : null;
	}

	public static byte ToByte(this object s)
	{

		if (s == null)
			return 0;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return 0;

		var valid = byte.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : (byte)0;
	}

	public static byte? ToByteNull(this object? s)
	{

		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = byte.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : null;
	}

	public static decimal ToDecimal(this object s)
	{
		if (s == null)
			return 0;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return 0;

		var valid = decimal.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : 0;
	}

	public static decimal? ToDecimalNull(this object? s)
	{
		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = decimal.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : null;
	}

	public static double ToDouble(this object s)
	{
		if (s == null)
			return 0;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return 0;

		var valid = double.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : 0;
	}

	public static double? ToDoubleNull(this object? s)
	{
		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = double.TryParse(data.RemoveComma().RemoveDash(), out var result);
		return valid ? result : null;
	}

	public static bool ToBool(this object s)
	{
		if (s == null)
			return false;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return false;

		var valid = bool.TryParse(data, out var result);
		return valid && result;
	}

	public static bool? ToBoolNull(this object? s)
	{
		if (s == null)
			return null;

		var data = s.ToString();
		if (string.IsNullOrEmpty(data))
			return null;

		var valid = bool.TryParse(data, out var result);
		return valid ? result : null;
	}
}
