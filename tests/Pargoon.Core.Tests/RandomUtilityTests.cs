using Pargoon.Utility;
using System.Linq;
using Xunit;

namespace Pargoon.Core.Tests;


public class RandomUtilsTests
{
    [Fact]
    public void RandomNumber_ShouldReturnNumberWithinRange()
    {
        int min = 1;
        int max = 10;
        int result = RandomUtils.RandomNumber(min, max);
        Assert.True(result >= min && result <= max);
    }

    [Fact]
    public void RandomString_ShouldReturnStringOfSpecifiedLength()
    {
        int len = 8;
        string result = RandomUtils.RandomString(len);
        Assert.Equal(len, result.Length);
    }

    [Fact]
    public void RandomPassword_ShouldReturnPasswordOfSpecifiedLength()
    {
        int len = 12;
        string result = RandomUtils.RandomPassword(len);
        Assert.Equal(len, result.Length);
    }

    [Fact]
    public void RandomString_WithLowerCaseOption_ShouldReturnLowercaseString()
    {
        int size = 10;
        bool lowerCase = true;
        string result = RandomUtils.RandomString(size, lowerCase);
        Assert.Equal(size, result.Length);
        Assert.True(result.All(char.IsLower));
    }

    [Fact]
    public void RandomString_WithUpperCaseOption_ShouldReturnMixedCaseString()
    {
        int size = 10;
        bool lowerCase = false;
        string result = RandomUtils.RandomString(size, lowerCase);
        Assert.Equal(size, result.Length);
        Assert.True(result.Any(char.IsUpper));
    }

    [Fact]
    public void GenerateRandomCode_ShouldReturnNumericStringOfSpecifiedLength()
    {
        int len = 6;
        string result = RandomUtils.GenerateRandomCode(len);
        Assert.Equal(len, result.Length);
        Assert.True(result.All(char.IsDigit));
    }
}
