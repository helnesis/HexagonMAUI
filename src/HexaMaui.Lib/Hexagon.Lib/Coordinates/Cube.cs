using System.Numerics;

namespace Hexagon.Lib.Coordinates
{

    public sealed class Hex(int q, int r, int s) : CubeInteger(q, r, s), IComparable<Hex> 
    {
        public int Identifier { get; set; }
        public (byte R, byte G, byte B) RGB { get; set; }
        public int CompareTo(Hex? other)
        {
            if (other is null) return 1;

            int thisDistance = this.Length;
            int otherDistance = other.Length;

            int distanceComparison = thisDistance.CompareTo(otherDistance);

            if (distanceComparison != 0)
            {
                return distanceComparison;
            }

            double thisAngle = Math.Atan2(R, Q);
            double otherAngle = Math.Atan2(other.R, other.Q);

            return thisAngle.CompareTo(otherAngle);
        }
    }

    /// <summary>
    /// Cube, but with floating point numbers.
    /// </summary>
    public sealed class FloatedCube(double q, double r, double s) : Cube<double>(q, r, s)
    {
        public CubeInteger Round()
        {
            int qi = (int)Math.Round(Q);
            int ri = (int)Math.Round(R);
            int si = (int)Math.Round(S);

            double qDiff = Math.Abs(qi - Q);
            double rDiff = Math.Abs(ri - R);
            double sDiff = Math.Abs(si - S);

            if (qDiff > rDiff && qDiff > sDiff)
                qi = -ri - si;
            else if (rDiff > sDiff)
                ri = -qi - si;
            else
                si = -qi - ri;

            return new CubeInteger(qi, ri, si);
        }

        public static IEnumerable<CubeInteger> Linedraw(CubeInteger a, CubeInteger b)
        {
            int N = a.Distance(b);
            double step = 1.0 / Math.Max(N, 1);

            FloatedCube nudgeA = new FloatedCube(a.Q + 1e-06, a.R + 1e-06, a.S - 2e-06);
            FloatedCube nudgeB = new FloatedCube(b.Q + 1e-06, b.R + 1e-06, b.S - 2e-06);

            for (int i = 0; i <= N; i++)
                yield return nudgeA.Lerp(nudgeB, step * i).Round();
        }

        public FloatedCube Lerp(FloatedCube other, double t)
            => new(Lerp(Q, other.Q, t), Lerp(R, other.R, t), Lerp(S, other.S, t));
        private static double Lerp(double a , double b, double t)
            => a * (1.0f - t) + b * t;
    }

    /// <summary>
    /// Cube, but with signed integers.
    /// </summary>
    public class CubeInteger(int q, int r, int s) : Cube<int>(q, r, s)
    {
        public static readonly IEnumerable<CubeInteger> Directions 
            = [ new (1, 0, -1),new (1, -1, 0),new (0, -1, 1), new (-1, 0, 1),new (-1, 1, 0),new(0, 1, -1) ];
        public static CubeInteger Direction(int direction)
            => Directions.ElementAt((6 + (direction % 6)) % 6);
        public static CubeInteger Neighbor(CubeInteger cube, int direction)
            => (CubeInteger)(cube + Direction(direction));
    }

    /// <summary>
    /// Generic cube class.
    /// </summary>
    /// <typeparam name="TNumber">Signed/Float/Double</typeparam>
    public class Cube<TNumber> : IEquatable<Cube<TNumber>> where TNumber : ISignedNumber<TNumber>, IAdditionOperators<TNumber, TNumber, TNumber>, ISubtractionOperators<TNumber, TNumber, TNumber>, IMultiplyOperators<TNumber, TNumber, TNumber>
    {
        private readonly TNumber _Q;
        private readonly TNumber _R;

        public TNumber Q { get { return _Q; } }
        public TNumber R { get { return _R; } }  
        public TNumber S { get { return -Q - R; } }

        public TNumber Length
            => (TNumber.Abs(Q) + TNumber.Abs(R) + TNumber.Abs(S)) / TNumber.CreateChecked(2);

        public TNumber Distance(Cube<TNumber> other)
            => (this - other).Length;

        public Cube(TNumber q, TNumber r, TNumber s)
        {
            if (!(typeof(TNumber) == typeof(int) || typeof(TNumber) == typeof(float) || typeof(TNumber) == typeof(double)))
                throw new ArgumentException("TNumber must be a 32 bit signed integer, float or double");

            if (q + r + s != TNumber.Zero)
                throw new ArgumentException("q + r + s must be equals to 0");

            _Q = q;
            _R = r;
        }
        public bool Equals(Cube<TNumber>? other)
        {
            return Q.Equals(other.Q) && R.Equals(other.R) && S.Equals(other.S);
        }
        public override bool Equals(object? obj)
        {
            return obj is Cube<TNumber> cube && Equals(cube);
        }

        #region Operators overloads
        public static bool operator ==(Cube<TNumber> left, Cube<TNumber> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Cube<TNumber> left, Cube<TNumber> right)
        {
            return !(left == right);
        }
        public static Cube<TNumber> operator +(Cube<TNumber> a, Cube<TNumber> b)
        {
            return new Cube<TNumber>(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }
        public static Cube<TNumber> operator-(Cube<TNumber> a, Cube<TNumber> b)
        {
            return new Cube<TNumber>(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }
        public static Cube<TNumber> operator*(Cube<TNumber> a, TNumber k)
        {
            return new Cube<TNumber>(a.Q * k, a.R * k, a.S * k);
        }
        #endregion

        #region Methods

        public override int GetHashCode()
        {
            var hq = Q.GetHashCode();
            var hr = R.GetHashCode();

            return unchecked((int)(hq ^ (hr + 0x9E3779b9 + (hq << 6) + (hq >> 2))));
        }

        public override string ToString()
            => $"Cube(q:{Q}, r:{R}, s:{S})";

        #endregion
    }
}
