using Mechanics.Cell;
using System;
using Mechanics.Geometry;

namespace Mechanics.Grid
{
    [Serializable]
    public class Grid : IGrid
    {
        public ICell this[Point p]
        {
            get
            {
                return _grid[p.X, p.Y];
            }
        }

        private Grid(IGrid grid)
        {
            _grid = new ICell[Extension, Extension];
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    var p = new Point(x, y);
                    _grid[x,y] = grid[p];
                }
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Grid;
            return other != null && SameContent(this, other);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    hashCode ^= _grid[x, y].GetHashCode();
                }
            }

            return hashCode;
        }

        private static bool SameContent(Grid grid, Grid other)
        {
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    if (!grid._grid[x, y].Equals(other._grid[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IGrid SetCell(Point p, NumericValue toSet)
        {
            var clone = new Grid(this);

            foreach (var value in Cell.Cell._allNumericValues)
            {
                if (value == toSet)
                {
                    continue;
                }
                clone._grid[p.X, p.Y] = clone._grid[p.X, p.Y].ExcludeValue(value);
            }

            return clone;
        }

        public IGrid ExcludeValueFromCell(Point p, NumericValue value)
        {
            var clone = new Grid(this);

            clone._grid[p.X, p.Y] = clone._grid[p.X, p.Y].ExcludeValue(value);

            return clone;
        }

        public static IGrid CreateEmptyGrid()
        {
            return new Grid();
        }

        private Grid()
        {
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    _grid[x, y] = Factory.Instance.CreateEmptyCell();
                }
            }
        }

        internal const int Extension = ExtensionNeighborhood * ExtensionNeighborhood;
        internal const int ExtensionNeighborhood = 3;

        private readonly ICell[,] _grid = new ICell[Extension, Extension];
    }
}
