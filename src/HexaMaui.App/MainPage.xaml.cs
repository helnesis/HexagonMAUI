using Hexagon.Lib.Coordinates;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HexaMaui.App
{
    public partial class MainPage : ContentPage
    {
        private int _LayerCount = 1;
        private bool _Orientation = true;
        private bool _Color = true;
        private double _SizeX = 25;
        private double _SizeY = 25;
        private Hex? _SelectedHexagon = null;

        public MainPage()
        {
            InitializeComponent();
            sliderValue.Text = _LayerCount.ToString();
            orientation.Text += _Orientation ? "Pointe" : "Plat";
            ySize.Text = _SizeY.ToString();
            xSize.Text = _SizeX.ToString();

            hexDrawable.ApplySettings(layerCount: 1, size: new(_SizeX, _SizeY), _Orientation, _Color);

        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _LayerCount = (int)e.NewValue;
            sliderValue.Text = _LayerCount.ToString();

            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color);
            graphicsView.Invalidate();

        }

        private void orientationCbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _Orientation = e.Value;
            orientation.Text = _Orientation ? "Orientation: Pointe" : "Orientation: Plat";
            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color);
            graphicsView.Invalidate();

        }

        private void Size_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (entry is not null)
            {
                string result = Regex.Replace(e.NewTextValue, @"[^0-9]+", "");

                if (Regex.Match(result, @"^[0-9]+$").Success)
                {
                    double v = double.Parse(result);

                    if (entry.StyleId == "entryX")
                    {
                        _SizeX = v;
                    }
                    else
                    {
                        _SizeY = v;
                    }
                }

                hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color);
                graphicsView.Invalidate();
            }
        }

        private void graphicsView_StartInteraction(object sender, TouchEventArgs e)
        {
            if (hexDrawable.Layout is not null)
            {
                var layout = hexDrawable.Layout;

                PointF click = e.Touches.FirstOrDefault();

                var hexCoord 
                    = layout.Layout!.PixelToHex(new(click.X, click.Y)).Round();

                var hexagon = 
                    layout.Hexagons.FirstOrDefault(h => h.Equals(hexCoord));

                if (hexagon is not null)
                {
                    _SelectedHexagon = hexagon;
                    hexDrawableIndividual.HexSize = layout.Size;
                    hexDrawableIndividual.HexOrientation = layout.HexagonOrientation;
                    hexDrawableIndividual.HexColor = Color.FromRgb(hexagon.RGB.R, hexagon.RGB.G, hexagon.RGB.B);
                    hexDrawableIndividual.Identifier = hexagon.Identifier;

                    individualGraphicsView.Invalidate();
                }
            }
        }
    }

}
