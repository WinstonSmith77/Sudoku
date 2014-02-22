using System.Collections.Generic;
using System.Linq;

namespace Mechanics.Geometry.Range
{
    public class Range : RangeBase
    {
        private readonly List<Point> _allPoints = new List<Point>();

        protected override IEnumerable<Point> GetAllPoints()
        {
            return _allPoints;
        }

        public Range(Point p)
        {
            _allPoints.AddRange(new Vertical(p.X));
            _allPoints.AddRange(new Horizontal(p.Y));
            _allPoints.AddRange(new Neighbor(p));

            _allPoints.RemoveAll(point => point.Equals(p));

            _allPoints = _allPoints.Distinct().ToList();
        }
    }
}
