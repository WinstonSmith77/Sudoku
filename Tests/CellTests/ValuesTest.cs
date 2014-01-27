using Mechanics.Cell;
using NUnit.Framework;

namespace Tests.CellTests
{
    [TestFixture]
    public class Values
    {
        [TestCase(Value.One, 1)]
        [TestCase(Value.Two, 2)]
        [TestCase(Value.Three, 3)]
        [TestCase(Value.Four, 4)]
        [TestCase(Value.Five, 5)]
        [TestCase(Value.Six, 6)]
        [TestCase(Value.Seven, 7)]
        [TestCase(Value.Eight, 8)]
        [TestCase(Value.Nine, 9)]
        public void SomeTest(Value value, int valueAsInt)
        {
            Assert.AreEqual((int)value, valueAsInt);
        }

    }
}
