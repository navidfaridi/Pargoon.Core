using Pargoon.Utility;
using Xunit;

namespace Pargoon.Core.Tests
{
    public class TimeUtilityTests
    {
        [Theory]
        [InlineData("10:10", 10, 10)]
        [InlineData("10:10 AM", 10, 10)]
        [InlineData("10:10 PM", 22, 10)]
        public void ShouldParseStringToHourAndMinute(string input, int expectedHour, int expectedMinute)
        {
            var min = input.GetMinute();
            var hour = input.GetHour();

            Assert.Equal(expectedHour, hour);
            Assert.Equal(expectedMinute, min);
        }


        [Theory]
        [InlineData(12, "00:00:12")]
        [InlineData(2, "00:00:02")]
        [InlineData(62, "00:01:02")]
        [InlineData(3602, "01:00:02")]
        [InlineData(3662, "01:01:02")]
        public void ShouldConvertSecondsToFullTimeString(int seconds, string expectedTimeString)
        {
            var t = seconds.GetFullTime();
            Assert.Equal(t, expectedTimeString);
        }


        [Theory]
        [InlineData(1662185398000, 2022, 9, 3,6,9,58)]
        [InlineData(1641251398000,2022,01,3,23,9,58)]
        public void ShouldConvertUNixMilisecondToDateTime(long ms,
            int expectedYear, int expectedMonth, int expectedDay,
            int expectedHour, int expectedMin, int expectedSecond)
        {
            var dt = ms.GetDateTimeFromUnixMiliseconds();
            dt = dt.ToUniversalTime();
            Assert.Equal(dt.Year, expectedYear);
            Assert.Equal(dt.Month, expectedMonth);
            Assert.Equal(dt.Day, expectedDay);
            Assert.Equal(dt.Hour, expectedHour);
            Assert.Equal(dt.Minute, expectedMin);
            Assert.Equal(dt.Second, expectedSecond);

        }

        [Theory]
        [InlineData("11:04", 1104)]
        [InlineData("16:30", 1630)]
        [InlineData("12:14", 1214)]
        [InlineData("01:04", 104)]
        public void ShouldReturnCorrespondingNumberForSpecificTimeString(string specificTime,int expectedNumber)
        {
            var result = specificTime.ToIntTime();

            Assert.Equal(expectedNumber, result);
        }
    }
}
