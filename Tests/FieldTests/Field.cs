using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics.Cell;
using Mechanics.Field;
using NUnit.Framework;
using Mechanics;

namespace Tests.FieldTests
{
    [TestFixture]
    public class Field
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
                    if (x >= 0 && x < _width && y >= 0 && y < _width)
                    {
                        Assert.That(emptyCell.Equals(field[x, y]));
                    }
                    else
                    {
                        Assert.Throws<IndexOutOfRangeException>(() => emptyCell.Equals(field[x, y]));
                    }
                }
            }
        }

        [Test]
        public void Clone()
        {
            var fieldA = Factory.Instance.CreateEmptyField();
            var fieldB = (IField)fieldA.Clone();

            Assume.That(fieldA.Equals(fieldB));

            var fieldC = fieldB.SetCell(5, 6, NumericValue.Eight);
            Assume.That(!fieldA.Equals(fieldC));
        }

        private const int _width = 9;



    }
}
