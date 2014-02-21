using System.Collections.Generic;

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
            int startX = _p.X / Grid.Grid.ExtensionNeighborhood * Grid.Grid.ExtensionNeighborhood;
            int startY = _p.Y / Grid.Grid.ExtensionNeighborhood * Grid.Grid.ExtensionNeighborhood;

            for (int x = startX; x < startX + Grid.Grid.ExtensionNeighborhood; x++)
            {
                for (int y = startY; y < startY + Grid.Grid.ExtensionNeighborhood; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
