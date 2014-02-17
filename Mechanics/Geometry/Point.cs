namespace Mechanics.Geometry
{
    public struct Point
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public Point(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Index
        {
            get { return X + Y * Field.Field.Extension; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }
            var other = (Point)obj;
            return other.X == X && other.Y == Y;
        }


    }
}
