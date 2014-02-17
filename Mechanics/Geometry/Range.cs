using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.Geometry
{
    public class Range : IEnumerable<Point>
    {
        private readonly List<Point> _allPoints = new List<Point>();

        public Range(Point p)
        {
            _allPoints.AddRange(Vertical(p));
            _allPoints.AddRange(Horizontal(p));
            _allPoints.AddRange(Neighborhood(p));
        }

        private IEnumerable<Point> Neighborhood(Point p)
        {
            var result = new List<Point>();

            int startX = p.X / Field.Field.ExtensionNeighborhood * Field.Field.ExtensionNeighborhood;
            int startY = p.Y / Field.Field.ExtensionNeighborhood * Field.Field.ExtensionNeighborhood;

            for (int x = startX; x < startX + Field.Field.ExtensionNeighborhood; x++)
            {
                for (int y = startY; y < startY + Field.Field.ExtensionNeighborhood; y++)
                {
                    if (x != p.X && y != p.Y)
                    {
                        result.Add(new Point(x, y));
                    }
                }
            }

            return result;
        }

        private IEnumerable<Point> Horizontal(Point p)
        {
            var result = new List<Point>();

            for (int x = 0; x < Field.Field.Extension; x++)
            {
                if (x != p.X)
                {
                    result.Add(new Point(x, p.Y));
                }
            }

            return result;
        }

        private IEnumerable<Point> Vertical(Point p)
        {
            var result = new List<Point>();

            for (int y = 0; y < Field.Field.Extension; y++)
            {
                if (y != p.Y)
                {
                    result.Add(new Point(p.X, y));
                }
            }

            return result;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _allPoints.Distinct().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
