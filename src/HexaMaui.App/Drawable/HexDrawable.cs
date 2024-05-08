using Hexagon.Lib;
using Hexagon.Lib.Coordinates;
using Hexagon.Lib.Enums;
using HexaMaui.App.Manager;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Platform;
using System.Diagnostics;
using static Hexagon.Lib.Layout;

namespace HexaMaui.App.Drawable
{
    public class HexDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _ = new LayoutManager(layerCount: 10, size: new(30), origin: new(dirtyRect.Center.X, dirtyRect.Center.Y), orientation: true, canvas: canvas, color: true);
        }
    }
}
