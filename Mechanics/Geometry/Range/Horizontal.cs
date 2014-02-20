using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Geometry.Range
{
    public class Horizontal : RangeBase
    {
        private readonly Point _p;

        public Horizontal(Point p)
        {
            _p = p;
        }

        protected override IEnumerable<Point> GetAllPoints()
        {
            for (int y = 0; y < Field.Field.Extension; y++)
            {
                yield return new Point(_p.X, y);
            }
        }
    }
}
