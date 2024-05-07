using HexaMaui.Lib.Hexagon.Enums;
using Microsoft.Maui.Graphics.Platform;
using System.Numerics;

namespace HexaMaui.Lib.Hexagon
{

    public sealed class Hexagon : IDrawable
    {
        const int MIN_WEDGE = 0;
        const int MAX_WEDGE = 5;

        #region Attributes

        private float _Circumradius = 25.0f;

        private Orientation _Orientation = Orientation.PointyTop;

        private readonly List<PointF> _Points = new(6);

        #endregion

        #region Properties
        public float Circumradius => _Circumradius;
        public Orientation Orientation => _Orientation;

        #endregion

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            List<PointF> points = new(6);

            float centerX = dirtyRect.Width / 2;
            float centerY = dirtyRect.Height / 2;

            for (int i = MIN_WEDGE; i < MAX_WEDGE; i++)
            {
                float angleDegree =
                    Orientation == Orientation.FlatTop ? 60.0f * i : 60.0f * i - 30;

                float angleRad = MathF.PI / 180.0f * angleDegree;

                float pX = centerX + Circumradius * MathF.Cos(angleRad);
                float pY = centerY + Circumradius * MathF.Sin(angleRad);

                points.Add(new(pX, pY));
            }
        }

 


        public sealed class HexagonBuilder
        {
            private readonly Hexagon _Hexagon = new();


            public HexagonBuilder WithRadius(float circumradius)
            {
                if (circumradius <= 0)
                    throw new ArgumentException("Circumradius must be greater than 0", nameof(circumradius));

                _Hexagon._Circumradius = circumradius;
                return this;
            }
            public HexagonBuilder WithOrientation(Orientation orientation)
            {
                _Hexagon._Orientation = orientation;
                return this;
            }

            public Hexagon Build()
                => _Hexagon;
        }
    }
}
