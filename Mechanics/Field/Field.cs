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


        public static IField CreateEmptyField()
        {
            return new Field();
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
