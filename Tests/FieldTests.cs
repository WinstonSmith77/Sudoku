using System;
using Mechanics.Cell;
using Mechanics.Field;
using Mechanics.Geometry;
using NUnit.Framework;
using Mechanics;

namespace Tests
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void CheckForCells()
        {
            var emptyCell = Factory.Instance.CreateEmptyCell();
            var field = Factory.Instance.CreateEmptyField();

            for (int x = -_width; x < 2 * _width; x++)
            {
                for (int y = _width; y < 2 * _width; y++)
                {
                    var p = new Point(x, y);
                    if (x >= 0 && x < _width && y >= 0 && y < _width)
                    {
                        Assert.That(emptyCell.Equals(field[p]));
                    }
                    else
                    {
                        Assert.Throws<IndexOutOfRangeException>(() => emptyCell.Equals(field[p]));
                    }
                }
            }
        }

        [Test]
        public void Identity()
        {
            var fieldA = Factory.Instance.CreateEmptyField();
            var fieldB = Factory.Instance.CreateEmptyField();

            Assume.That(fieldA.Equals(fieldB));

            var fieldC = fieldB.SetCell(new Point(5, 6), NumericValue.Eight);
            Assume.That(!fieldA.Equals(fieldC));

            Assume.That(fieldA.Equals(fieldB));
        }

        private const int _width = 9;
    }
}
