﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mechanics.Geometry.Range
{
    public class Range : RangeBase
    {
        private readonly IEnumerable<Point> _allPoints;

        protected override IEnumerable<Point> GetAllPoints()
        {
            return _allPoints;
        }

        public Range(Point p)
        {
            var allPoints = new List<Point>();

            allPoints.AddRange(new Vertical(p));
            allPoints.AddRange(new Horizontal(p));
            allPoints.AddRange(new Neighbor(p));

            allPoints.Remove(p);

            _allPoints = allPoints.Distinct().ToList();
        }

    }
}
