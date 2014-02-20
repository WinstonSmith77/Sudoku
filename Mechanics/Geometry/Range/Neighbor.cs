using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Geometry.Range
{
    public class Neighbor : RangeBase
    {
        private readonly Point _p;

        public Neighbor(Point p)
        {
            _p = p;
        }

        protected override IEnumerable<Point> GetAllPoints()
        {
            int startX = _p.X / Field.Field.ExtensionNeighborhood * Field.Field.ExtensionNeighborhood;
            int startY = _p.Y / Field.Field.ExtensionNeighborhood * Field.Field.ExtensionNeighborhood;

            for (int x = startX; x < startX + Field.Field.ExtensionNeighborhood; x++)
            {
                for (int y = startY; y < startY + Field.Field.ExtensionNeighborhood; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
