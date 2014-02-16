using System;
using Mechanics.Cell;

namespace Mechanics.Field
{
    public interface IField : ICloneable
    {
        ICell this[int x, int y] { get; }

        IField SetCell(int x, int y, NumericValue value);
    }
}