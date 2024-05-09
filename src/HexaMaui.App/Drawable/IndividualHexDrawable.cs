using Hexagon.Lib.Coordinates;
using Hexagon.Lib.Enums;

namespace HexaMaui.App.Drawable
{
    public class IndividualHexDrawable : IDrawable
    {
        // Size
        public Hexagon.Lib.Point? HexSize { get; set; }

        // Identifier
        public int? Identifier { get; set; }

        // Color
        public Color? HexColor { get; set; }

        // Orientation
        public bool HexOrientation { get; set; }
        
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var center = dirtyRect.Center;

            if (HexSize is not null && HexColor is not null)
            {
                canvas.FillColor = HexColor;

                var baseCube = new CubeInteger(0, 0, 0);


                //@TODO: maybe size must be hardcoded to avoid issue with window width and height

                Hexagon.Lib.Layout layout = 
                    new(orientation: HexOrientation ? Orientation.Pointy : Orientation.Flat, size: HexSize, origin: new(center.X, center.Y));

                using var pathf = new PathF();

                var p = layout.PolygonCorners(baseCube).ToList();
                var hexCenter = layout.HexToPixel(baseCube);

                // center
                float cX = (float)hexCenter.X;
                float cY = (float)hexCenter.Y;

                // origin
                float oX = (float)p[0].X;
                float oY = (float)p[0].Y;

                pathf.MoveTo(oX, oY);

                for(int i = 1; i < p.Count; i++)
                {
                    pathf.LineTo((float)p[i].X, (float)p[i].Y);
                }

                pathf.Close();

                canvas.FillPath(pathf);
                canvas.DrawPath(pathf);


                canvas.FontColor = Colors.Black;

                if (Identifier is not null)
                    canvas.DrawString(Identifier.ToString(), cX, cY, HorizontalAlignment.Center);


            }
        }
    }
}
