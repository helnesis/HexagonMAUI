using Hexagon.Lib.Coordinates;
using Hexagon.Lib.Enums;
using System.Drawing;


namespace Hexagon.Lib
{
    public class Layout(Orientation orientation, Point size, Point origin) : IEquatable<Layout>
    {
        private readonly Orientation _Orientation = orientation;
        private readonly Point _Size = size;
        private readonly Point _Origin = origin;
        public Orientation Orientation { get { return _Orientation; } }
        public Point Size { get { return _Size; } }


        public Point Origin
        {
            get
            {
                return _Origin;
            }
        }

        public Point HexToPixel(CubeInteger cube)
        {
            double x = (Orientation.F0 * cube.Q + Orientation.F1 * cube.R) * Size.X;
            double y = (Orientation.F2 * cube.Q + Orientation.F3 * cube.R) * Size.Y;

            return new Point(x + Origin.X, y + Origin.Y);
        }

        
        /*
        public FloatedCube PixelToHex(Point p)
        {
            Point pt = new((p.X - Origin.X) / Size.X, 
                                 (p.Y - Origin.Y) / Size.Y);

            double q = Orientation.B0 * pt.X + Orientation.B1 * pt.Y;
            double r = Orientation.B2 * pt.X + Orientation.B3 * pt.Y;

            return new FloatedCube(q, r, -q - r);
        }
        */

        public Hex PixelToHex(Point p)
        {
            Point pt = new((p.X - Origin.X) / Size.X,
                     (p.Y - Origin.Y) / Size.Y);

            int q = (int)(Orientation.B0 * pt.X + Orientation.B1 * pt.Y);
            int r = (int)(Orientation.B2 * pt.X + Orientation.B3 * pt.Y);

            return new Hex(q, r, -q - r);
        }

        public Point HexCornerOffset(int corner)
        {
            Point s = Size;
            double a = 2.0 * Math.PI * (Orientation.StartAngle - corner) / 6.0;

            return new Point(s.X * Math.Cos(a), s.Y * Math.Sin(a));
        }

        public IEnumerable<Point> PolygonCorners(CubeInteger cube)
        {
            Point center = HexToPixel(cube);

            for(int i = 0; i < 6; i++)
            {
                Point offset = HexCornerOffset(i);
                yield return new Point(center.X + offset.X, center.Y + offset.Y);
            }
        }

        public bool Equals(Layout? other)
        {
            if (other is null)
                return false;

            return Origin.X == other.Origin.X && Origin.Y == other.Origin.Y;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Layout);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HashCode.Combine(Origin.X, Origin.Y) ^ 28);
        }
    }
}
