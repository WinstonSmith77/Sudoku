using System;
using Mechanics.Cell;

namespace Mechanics.Field
{
    public interface IField : ICloneable
    {
        ICell this[Point p] { get; }

        IField SetCell(Point p, NumericValue value);
    }
}