using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private const int _width = 9;



    }
}
