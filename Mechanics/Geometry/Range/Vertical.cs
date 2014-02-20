using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Geometry.Range
{
    public class Vertical : RangeBase
    {
        private readonly int _x;

        public Vertical(int x)
        {
            _x = x;
        }

        protected override IEnumerable<Point> GetAllPoints()
        {
            for (int y = 0; y < Field.Field.Extension; y++)
            {
                yield return new Point(_x, y);
            }
        }
    }
}
