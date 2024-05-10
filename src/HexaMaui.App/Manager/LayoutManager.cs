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
        #region Attributes

        private Layout? _Layout;

        private readonly Point _Size;

        private readonly bool _HasColor;

        private readonly int _LayerCount;

        private readonly bool _Orientation;

        private readonly bool _DisplayNumber;

        #endregion


        #region Properties

        /// <summary>
        /// The layout of the hexagon.
        /// </summary>
        public Layout? Layout { get { return _Layout; } }

        /// <summary>
        /// The size of the hexagon (inner radius).
        /// </summary>
        public Point Size { get { return _Size; } }

        /// <summary>
        /// Number of layers.
        /// </summary>
        public int LayerCount { get { return _LayerCount; } }

        /// <summary>
        /// Hexagon has color.
        /// </summary>
        public bool HasColor { get { return _HasColor; } }


        /// <summary>
        /// Hexagon orientation
        /// </summary>
        public bool HexagonOrientation { get { return _Orientation; } }

        public List<Hex> Hexagons { get; init; } = [];

        #endregion

        /// <summary>
        /// Create a hexagon shape.
        /// </summary>
        /// <param name="layerCount">Layer count</param>
        /// <param name="size">Size (radius)</param>
        /// <param name="origin">Origin</param>
        /// <param name="orientation">Hexagon layout, set to true for pointy, false for flat.</param>
        /// <param name="color">Color</param>
        /// <exception cref="ArgumentException"></exception>
        public LayoutManager(int layerCount, Point size, bool orientation, bool color = false, bool displayNumber = true)
        {
            if (layerCount < 1)
                throw new ArgumentException("Layer count must be greater than 0");

            _LayerCount = layerCount;
            _Size = size;
            _Orientation = orientation;
            _HasColor = color;
            _DisplayNumber = displayNumber;
        }

        public void GenerateShape(ICanvas canvas, RectF dirtyRect)
        {
            Generate(canvas, dirtyRect);
        }

        #region Private methods
        private void Generate(ICanvas canvas, RectF dirtyRect)
        {
            _Layout = 
                new Layout(_Orientation ? Orientation.Pointy : Orientation.Flat, _Size, new(dirtyRect.Center.X, dirtyRect.Center.Y));

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
                    canvas.FillColor = Color.FromRgb(shape.RGB.R, shape.RGB.G, shape.RGB.B);
                }

                shape.Identifier = hexIndex++;

                Create(shape, canvas);
                Hexagons.Add(shape);
            }

        }
        private void Create(Hex coordinate, ICanvas canvas)
        {
            if (_Layout is null)
                throw new Exception("Layout cannot be null. It determines the shape of each hexagon!");

            using var pathf = new PathF();

            var points =
                _Layout!.PolygonCorners(coordinate).ToList();

            var center = _Layout!.HexToPixel(coordinate);


            // Hexagon position
            float centerX = (float)center.X;
            float centerY = (float)center.Y;

            // Move to the first point.
            float oX = (float)points[0].X;
            float oY = (float)points[0].Y;

            pathf.MoveTo(oX, oY);

            for (int i = 1; i < points.Count; i++)
            {
                pathf.LineTo((float)points[i].X, (float)points[i].Y);
            }


            pathf.Close();

            
            canvas.FillPath(pathf);
            canvas.DrawPath(pathf);

            canvas.FontColor = Colors.Black;

            if (_DisplayNumber)
                canvas.DrawString(coordinate.Identifier.ToString(), centerX, centerY, HorizontalAlignment.Center);
        }

        #endregion

    }
}
