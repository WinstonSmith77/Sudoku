using System.Collections;
using Mechanics.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics.Geometry;

namespace Mechanics.Field
{
    [Serializable]
    public class Field : IField
    {
        public ICell this[Point p]
        {
            get
            {
                return _field[p.X, p.Y];
            }
        }

        private Field(IField field)
        {
            _field = new ICell[Extension, Extension];
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    var p = new Point(x, y);
                    _field[x,y] = field[p];
                }
            }
        }


        public override bool Equals(object obj)
        {
            var other = obj as Field;
            return other != null && SameContent(this, other);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    hashCode ^= _field[x, y].GetHashCode();
                }
            }

            return hashCode;
        }


        private static bool SameContent(Field field, Field other)
        {
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    if (!field._field[x, y].Equals(other._field[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IField SetCell(Point p, NumericValue toSet)
        {
            var clone = new Field(this);

            foreach (var value in Cell.Cell._allNumericValues)
            {
                if (value == toSet)
                {
                    continue;
                }
                clone._field[p.X, p.Y] = clone._field[p.X, p.Y].ExcludeValue(value);
            }

            return clone;
        }

        public IField ExcludeValueFromCell(Point p, NumericValue value)
        {
            var clone = new Field(this);

            clone._field[p.X, p.Y] = clone._field[p.X, p.Y].ExcludeValue(value);

            return clone;
        }

        public static IField CreateEmptyField()
        {
            return new Field();
        }

        private Field(ICell[,] field)
        {
            _field = field;
        }

        private Field()
        {
            for (int x = 0; x < Extension; x++)
            {
                for (int y = 0; y < Extension; y++)
                {
                    _field[x, y] = Factory.Instance.CreateEmptyCell();
                }
            }
        }

        internal const int Extension = ExtensionNeighborhood * ExtensionNeighborhood;
        internal const int ExtensionNeighborhood = 3;

        private readonly ICell[,] _field = new ICell[Extension, Extension];
    }
}
