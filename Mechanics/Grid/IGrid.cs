using Mechanics.Cell;
using Mechanics.Geometry;

namespace Mechanics.Grid
{
    public interface IGrid 
    {
        ICell this[Point p] { get; }

        IGrid SetCell(Point p, NumericValue value);
        IGrid ExcludeValueFromCell(Point p, NumericValue value);
    }
}