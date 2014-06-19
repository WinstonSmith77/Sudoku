using Mechanics.Cell;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Values
    {
        private readonly object[] _testCases = new object[]
            {
                new object[]  {NumericValue.One,  1},
                new object[]  {NumericValue.Two,  2},
                new object[]  {NumericValue.Three,  3},
                new object[]  {NumericValue.Four,  4},
                new object[]  {NumericValue.Five,  5},
                new object[]  {NumericValue.Six,  6},
                new object[]  {NumericValue.Seven,  7},
                new object[]  {NumericValue.Eight,  8},
                new object[]  {NumericValue.Nine,  9},
            };

        [Test, TestCaseSource("_testCases")]
        public void CheckNumericValueTiInt(NumericValue value, int valueAsInt)
        {
            Assert.AreEqual((int)value, valueAsInt);
        }

    }
}
