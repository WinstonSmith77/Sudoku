using System.Collections;
using System.Collections.Generic;

namespace Mechanics.Geometry.Range
{
    public abstract class RangeBase : IRange
    {
        public IEnumerator<Point> GetEnumerator()
        {
            return GetAllPoints().GetEnumerator();
        }

        protected abstract IEnumerable<Point> GetAllPoints();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}