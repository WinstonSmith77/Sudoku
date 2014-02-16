using Mechanics.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Field
{
    public class Field : IField
    {
        public ICell this[int x, int y]
        {
            get { return _field[x, y]; }
        }

        public object Clone()
        {
            var field = new ICell[_width,_width];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
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

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    hashCode ^= _field[x, y].GetHashCode();
                }
            }

            return hashCode;
        }


        private static bool SameContent(Field field, Field other)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    if (!field._field[x, y].Equals(other._field[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IField SetCell(int x, int y, NumericValue toSet)
        {
            var clone = (Field)Clone();

            foreach (var value in Cell.Cell._allNumericValues)
            {
                if (value == toSet)
                {
                    continue;
                }
                clone._field[x,y] = clone._field[x,y].ExcludeValue(value);
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
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    _field[x, y] = Factory.Instance.CreateEmptyCell();
                }
            }
        }

        private const int _width = 9;

        private readonly ICell[,] _field = new ICell[_width, _width];
    }
}
