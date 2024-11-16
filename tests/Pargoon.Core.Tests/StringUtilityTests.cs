using Pargoon.Utility;
using Xunit;

namespace Pargoon.Core.Tests;

public class StringUtilityTests
{
	[Fact]
	public void IsEnglishLetter_ValidString_ReturnsTrue()
	{
		// Arrange
		var str = "abcdefg";

		// Act
		var result = str.IsEnglishLetter();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsEnglishLetter_InvalidString_ReturnsFalse()
	{
		// Arrange
		var str = "abc123";

		// Act
		var result = str.IsEnglishLetter();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void IsValidUsername_ValidUsername_ReturnsTrue()
	{
		// Arrange
		var username = "john_doe123";

		// Act
		var result = username.IsValidUsername();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsValidUsername_InvalidUsername_ReturnsFalse()
	{
		// Arrange
		var username = "john$doe";

		// Act
		var result = username.IsValidUsername();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void IsValidMobileNumber_ValidMobileNumber_ReturnsTrue()
	{
		// Arrange
		var mobile = "09123456789";

		// Act
		var result = mobile.IsValidMobileNumber();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsValidMobileNumber_InvalidMobileNumber_ReturnsFalse()
	{
		// Arrange
		var mobile = "12345";

		// Act
		var result = mobile.IsValidMobileNumber();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void IsNumeric_ValidNumericString_ReturnsTrue()
	{
		// Arrange
		var numericString = "12345";

		// Act
		var result = numericString.IsNumeric();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsNumeric_InvalidNumericString_ReturnsFalse()
	{
		// Arrange
		var numericString = "123a";

		// Act
		var result = numericString.IsNumeric();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void IsValidEmail_ValidEmail_ReturnsTrue()
	{
		// Arrange
		var email = "test@example.com";

		// Act
		var result = email.IsValidEmail();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsValidEmail_InvalidEmail_ReturnsFalse()
	{
		// Arrange
		var email = "test";

		// Act
		var result = email.IsValidEmail();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void FixEmail_EmailWithSpaces_ReturnsFixedEmail()
	{
		// Arrange
		var email = "   test@example.com   ";
		var expectedFixedEmail = "test@example.com";

		// Act
		var result = email.FixEmail();

		// Assert
		Assert.Equal(expectedFixedEmail, result);
	}

	[Fact]
	public void StripTags_StringWithTags_ReturnsTaglessString()
	{
		// Arrange
		var html = "<p>Hello, <strong>world!</strong></p>";
		var expectedStrippedString = "Hello, world!";

		// Act
		var result = html.StripTags();

		// Assert
		Assert.Equal(expectedStrippedString, result);
	}

	[Fact]
	public void FixLength_LongString_ReturnsTrimmedStringWithEllipsis()
	{
		// Arrange
		var str = "This is a long string that needs to be trimmed.";
		var expectedTrimmedString = "This is a long string that nee...";

		// Act
		var result = str.FixLength(30);

		// Assert
		Assert.Equal(expectedTrimmedString, result);
	}

	[Fact]
	public void HasValue_NullString_ReturnsFalse()
	{
		// Arrange
		string value = null;

		// Act
		var result = value.HasValue();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void HasValue_EmptyString_ReturnsFalse()
	{
		// Arrange
		var value = "";

		// Act
		var result = value.HasValue();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void HasValue_WhiteSpaceString_IgnoreWhiteSpaceOption_ReturnsFalse()
	{
		// Arrange
		var value = "   ";

		// Act
		var result = value.HasValue(ignoreWhiteSpace: true);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void HasValue_WhiteSpaceString_DoNotIgnoreWhiteSpaceOption_ReturnsTrue()
	{
		// Arrange
		var value = "   ";

		// Act
		var result = value.HasValue(ignoreWhiteSpace: false);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void ToInt_ValidNumericString_ReturnsIntValue()
	{
		// Arrange
		var numericString = "12345";
		var expectedValue = 12345;

		// Act
		var result = numericString.ToInt();

		// Assert
		Assert.Equal(expectedValue, result);
	}

	[Fact]
	public void ToDecimal_ValidNumericString_ReturnsDecimalValue()
	{
		// Arrange
		var numericString = "123.45";
		var expectedValue = 123.45m;

		// Act
		var result = numericString.ToDecimal();

		// Assert
		Assert.Equal(expectedValue, result);
	}

	[Fact]
	public void ToNumeric_IntValue_ReturnsFormattedNumericString()
	{
		// Arrange
		var value = 123456;
		var expectedFormattedString = "123,456";

		// Act
		var result = value.ToNumeric();

		// Assert
		Assert.Equal(expectedFormattedString, result);
	}

	[Fact]
	public void ToNumeric_DecimalValue_ReturnsFormattedNumericString()
	{
		// Arrange
		var value = 1234.56m;
		var expectedFormattedString = "1,235";

		// Act
		var result = value.ToNumeric();

		// Assert
		Assert.Equal(expectedFormattedString, result);
	}

	[Fact]
	public void ToCurrency_IntValue_ReturnsFormattedCurrencyString()
	{
		// Arrange
		var value = 123456;
		var expectedFormattedString = "$123,456";

		// Act
		var result = value.ToCurrency();

		// Assert
		Assert.Equal(expectedFormattedString, result);
	}

	[Fact]
	public void ToCurrency_DecimalValue_ReturnsFormattedCurrencyString()
	{
		// Arrange
		var value = 1234.56m;
		var expectedFormattedString = "$1,235";

		// Act
		var result = value.ToCurrency();

		// Assert
		Assert.Equal(expectedFormattedString, result);
	}

	[Fact]
	public void ToEnglishNumber_PersianNumericString_ReturnsEnglishNumericString()
	{
		// Arrange
		var persianNumber = "۱۲۳۴۵۶";
		var expectedEnglishNumber = "123456";

		// Act
		var result = persianNumber.ToEnglishNumber();

		// Assert
		Assert.Equal(expectedEnglishNumber, result);
	}

	[Fact]
	public void En2Fa_EnglishNumberString_ReturnsPersianNumberString()
	{
		// Arrange
		var englishNumber = "123456";
		var expectedPersianNumber = "۱۲۳۴۵۶";

		// Act
		var result = englishNumber.En2Fa();

		// Assert
		Assert.Equal(expectedPersianNumber, result);
	}

	[Fact]
	public void Fa2En_PersianNumberString_ReturnsEnglishNumberString()
	{
		// Arrange
		var persianNumber = "۱۲۳۴۵۶";
		var expectedEnglishNumber = "123456";

		// Act
		var result = persianNumber.Fa2En();

		// Assert
		Assert.Equal(expectedEnglishNumber, result);
	}

	[Fact]
	public void FixPersianChars_StringWithPersianChars_ReturnsFixedString()
	{
		// Arrange
		var str = "ﮎﮏﮐﮑكي ھ";
		var expectedFixedString = "کککککی ه";

		// Act
		var result = str.FixPersianChars();

		// Assert
		Assert.Equal(expectedFixedString, result);
	}

	[Fact]
	public void CleanString_StringWithMixedChars_ReturnsCleanedString()
	{
		// Arrange
		var str = " ﮎ ﮏ ۱۲۳ 123";
		var expectedCleanedString = "ک ک 123 123";

		// Act
		var result = str.CleanString();

		// Assert
		Assert.Equal(expectedCleanedString, result);
	}

	[Fact]
	public void NullIfEmpty_NullString_ReturnsNull()
	{
		// Arrange
		string str = null;

		// Act
		var result = str.NullIfEmpty();

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void NullIfEmpty_EmptyString_ReturnsNull()
	{
		// Arrange
		var str = "";

		// Act
		var result = str.NullIfEmpty();

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void RemoveDangerousChars_StringWithDangerousChars_ReturnsSafeString()
	{
		// Arrange
		var inputString = "Some/Path/../../SecretFile.txt";
		var expectedSafeString = "SomePath....SecretFile.txt";

		// Act
		var result = inputString.RemoveDangerousChars();

		// Assert
		Assert.Equal(expectedSafeString, result);
	}

	[Fact]
	public void CorrectFarsiChars_StringWithFarsiChars_ReturnsCorrectedString()
	{
		// Arrange
		var inputString = "کڪګڬڭڮیۍێېۑ";
		var expectedCorrectedString = "ککککککییییی";

		// Act
		var result = inputString.CorrectFarsiChars();

		// Assert
		Assert.Equal(expectedCorrectedString, result);
	}

	[Fact]
	public void CorrectFarsiChar_FarsiChar_ReturnsCorrectedChar()
	{
		// Arrange
		var inputChar = 'ک';
		var expectedCorrectedChar = 'ک';

		// Act
		var result = inputChar.CorrectFarsiChar();

		// Assert
		Assert.Equal(expectedCorrectedChar, result);
	}

	[Fact]
	public void IsValidIranianNationalCode_ValidNationalCode_ReturnsTrue()
	{
		// Arrange
		var nationalCode = "0070837813";

		// Act
		var result = nationalCode.IsValidNationalCode();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsValidNationalCode_InvalidNationalCode_ReturnsFalse()
	{
		// Arrange
		var nationalCode = "1234567890";

		// Act
		var result = nationalCode.IsValidNationalCode();

		// Assert
		Assert.False(result);
	}
}

