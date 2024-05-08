using ColorHelper;
using Hexagon.Lib;
using Hexagon.Lib.Coordinates;
using Hexagon.Lib.Enums;
using Layout = Hexagon.Lib.Layout;
using Point = Hexagon.Lib.Point;

namespace HexaMaui.App.Manager
{
    public sealed class LayoutManager
    {
        private readonly Layout _Layout;

        private readonly Point _Size;

        private readonly Point _Origin;

        private PathF _Path;

        private readonly bool _HasColor;

        private readonly ICanvas _Canvas;

        private readonly int _LayerCount;

        /// <summary>
        /// The layout of the hexagon.
        /// </summary>
        public Layout Layout { get { return _Layout; } }

        /// <summary>
        /// The size of the hexagon (inner radius).
        /// </summary>
        public Point Size { get { return _Size; } }

        /// <summary>
        /// The origin of the hexagon.
        /// </summary>
        public Point Origin { get { return _Origin; } }

        /// <summary>
        /// Number of layers.
        /// </summary>
        public int LayerCount { get { return _LayerCount; } }

        /// <summary>
        /// Hexagon has color.
        /// </summary>
        public bool HasColor { get { return _HasColor; } }

        /// <summary>
        /// Create a hexagon shape.
        /// </summary>
        /// <param name="layerCount">Layer count</param>
        /// <param name="size">Size (radius)</param>
        /// <param name="origin">Origin</param>
        /// <param name="orientation">Hexagon layout, set to true for pointy, false for flat.</param>
        /// <param name="startColor">Hexagon start color.</param>
        /// <exception cref="ArgumentException"></exception>
        public LayoutManager(int layerCount, Point size, Point origin, bool orientation, ICanvas canvas, bool color = false)
        {
            if (layerCount < 1)
                throw new ArgumentException("Layer count must be greater than 0");

            _LayerCount = layerCount;
            _Size = size;
            _Origin = origin;
            _HasColor = color;
            _Canvas = canvas;

            _Path = new PathF();
            _Layout = new Layout(orientation ? Orientation.Pointy : Orientation.Flat, size, origin);

            Generate();
        }


        private void Generate()
        {
            var shapeCoordinates = Shapes.Hexagons(_LayerCount, true);

            var hexIndex = 0;
            var count = shapeCoordinates.Count;

            byte s = 100, l = 50;
            int h = 0;
            int step = 360 / count;

            foreach (var shape in shapeCoordinates)
            {
                if (_HasColor)
                {
                    HSL hsl = new(h, s, l);
                    RGB rgb = ColorHelper.ColorConverter.HslToRgb(hsl);
                    h += step;

                    shape.RGB = new(rgb.R, rgb.G, rgb.B);
                    _Canvas.FillColor = Color.FromRgb(shape.RGB.R, shape.RGB.G, shape.RGB.B);
                }

                shape.Identifier = hexIndex++;
                Create(shape);
            }

        }

        private void Create(CubeInteger coordinate)
        {
            _Path = new PathF();

            var points =
                _Layout.PolygonCorners(coordinate).ToList();

            // Move to the first point.
            float oX = (float)points[0].X;
            float oY = (float)points[0].Y;

            _Path.MoveTo(oX, oY);

            for (int i = 1; i < points.Count; i++)
            {
                _Path.LineTo((float)points[i].X, (float)points[i].Y);
            }


            _Path.Close();

            _Canvas.DrawPath(_Path);
            _Canvas.FillPath(_Path);

        }

    }
}
