using System;
using Mechanics.Cell;
using Mechanics.Geometry;

namespace Mechanics.Field
{
    public interface IField 
    {
        ICell this[Point p] { get; }

        IField SetCell(Point p, NumericValue value);
        IField ExcludeValueFromCell(Point p, NumericValue value);
    }
}