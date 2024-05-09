namespace Hexagon.Lib
{
    public partial class Point(double x, double y)
    {
        public Point(double x) : this(x, x) { }

        private readonly double _X = x;
        private readonly double _Y = y;
        public double X { get { return _X; } }
        public double Y { get { return _Y; } }
    }
}
