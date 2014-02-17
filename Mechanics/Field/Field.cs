using Mechanics.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Field
{
    [Serializable]
    public class Field : IField
    {
        public ICell this[Point p]
        {
            get { return (ICell)_field[p.X, p.Y].Clone(); }
        }

        public object Clone()
        {
            var field = new ICell[Width,Width];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    field[x, y] = (ICell)_field[x,y].Clone();
                }
            }

            return new Field(field);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Field;
            return other != null && SameContent(this, other);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    hashCode ^= _field[x, y].GetHashCode();
                }
            }

            return hashCode;
        }


        private static bool SameContent(Field field, Field other)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
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
            var clone = (Field)Clone();

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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    _field[x, y] = Factory.Instance.CreateEmptyCell();
                }
            }
        }

        internal const int Width = 9;

        private readonly ICell[,] _field = new ICell[Width, Width];
    }
}
