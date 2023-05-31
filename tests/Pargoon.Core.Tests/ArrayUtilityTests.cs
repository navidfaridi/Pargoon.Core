using Pargoon.Utility;
using Xunit;

namespace Pargoon.Core.Tests
{
    public class ArrayUtilityTests
    {
        [Fact]
        public void MergeToStringMethodShouldBeSameAsStringJoin()
        {
            string[] sampleArray1 = { "item 1","item 2","item 3"};
            int[] sampleArray2 = { 1,2,3,4,5 };

            var res1 = sampleArray1.MergeToString(",");
            var joinResult1 = string.Join(",", sampleArray1);


            var res2 = sampleArray2.MergeToString(",");
            var joinResult2 = string.Join(",", sampleArray2);

            
            Assert.Equal(joinResult1, res1);
            Assert.Equal(res2, joinResult2);
        }
    }
}
