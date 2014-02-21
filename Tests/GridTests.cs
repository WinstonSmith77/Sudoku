using System;
using Mechanics.Cell;
using Mechanics.Geometry;
using NUnit.Framework;
using Mechanics;

namespace Tests
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void CheckForCells()
        {
            var emptyCell = Factory.Instance.CreateEmptyCell();
            var grid = Factory.Instance.CreateEmptyGrid();

            for (int x = -_width; x < 2 * _width; x++)
            {
                for (int y = _width; y < 2 * _width; y++)
                {
                    var p = new Point(x, y);
                    if (x >= 0 && x < _width && y >= 0 && y < _width)
                    {
                        Assert.That(emptyCell.Equals(grid[p]));
                    }
                    else
                    {
                        Assert.Throws<IndexOutOfRangeException>(() => emptyCell.Equals(grid[p]));
                    }
                }
            }
        }

        [Test]
        public void Identity()
        {
            var gridA = Factory.Instance.CreateEmptyGrid();
            var gridB = Factory.Instance.CreateEmptyGrid();

            Assume.That(gridA.Equals(gridB));

            var gridC = gridB.SetCell(new Point(5, 6), NumericValue.Eight);
            Assume.That(!gridA.Equals(gridC));

            Assume.That(gridA.Equals(gridB));
        }

        private const int _width = 9;
    }
}
