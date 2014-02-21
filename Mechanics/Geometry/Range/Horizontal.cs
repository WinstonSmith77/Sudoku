using System.Collections.Generic;

namespace Mechanics.Geometry.Range
{
    public class Horizontal : RangeBase
    {
        private readonly int _y;

        public Horizontal(int y)
        {
            _y = y;
        }

        protected override IEnumerable<Point> GetAllPoints()
        {
            for (int x = 0; x < Grid.Grid.Extension; x++)
            {
                yield return new Point(x, _y);
            }
        }
    }
}
