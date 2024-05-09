using HexaMaui.App.Manager;
using Windows.Devices.Bluetooth.Advertisement;

namespace HexaMaui.App.Drawable
{
    public class HexDrawable : IDrawable
    {
        private LayoutManager? _LayoutMgr;

        public LayoutManager? Layout { get { return _LayoutMgr; } }

        public void ApplySettings(int layerCount, Hexagon.Lib.Point size, bool orientation = true, bool hasColor = true)
        {
            _LayoutMgr = new(layerCount, size, orientation, hasColor );
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _LayoutMgr?.GenerateShape(canvas, dirtyRect);
        }
    }
}
