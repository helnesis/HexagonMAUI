namespace Hexagon.Lib.Enums
{
    public class Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
    {
        private readonly double _F0 = f0;
        private readonly double _F1 = f1;
        private readonly double _F2 = f2;
        private readonly double _F3 = f3;

        private readonly double _B0 = b0;
        private readonly double _B1 = b1;
        private readonly double _B2 = b2;
        private readonly double _B3 = b3;

        private readonly double _StartAngle = startAngle;

        public double F0 { get { return _F0; } }
        public double F1 { get { return _F1; } }
        public double F2 { get { return _F2; } }
        public double F3 { get { return _F3; } }
        public double B0 { get { return _B0; } }
        public double B1 { get { return _B1; } }
        public double B2 { get { return _B2; } }
        public double B3 { get { return _B3; } }
        public double StartAngle { get { return _StartAngle; } }


        public static readonly Orientation Pointy = new(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
        public static readonly Orientation Flat = new(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);
    }
}
