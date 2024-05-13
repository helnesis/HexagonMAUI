using Hexagon.Lib.Coordinates;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HexaMaui.App
{
    public partial class MainPage : ContentPage
    {
        private int _LayerCount = 5;
        private bool _Orientation = true;
        private bool _Color = true;
        private double _SizeX = 25;
        private double _SizeY = 25;
        private int _LayerMax = 10;
        private bool _DisplayIdentifier = true;
        private bool _HasSelectedHexagon = false;
        private bool _SortHexagon = true;


        private int? _SelectedHexagonIdentifier = 0;
        private Color? _SelectedHexagonColor = null;

        public MainPage()
        {
            InitializeComponent();
            layerMax.Text = _LayerMax.ToString();
            layerSlide.Maximum = _LayerMax;
            layerSlide.Value = _LayerCount;
            sliderValue.Text = _LayerCount.ToString();

            sortHexagon.IsChecked = _SortHexagon;
            sortHexagonText.Text = "Oui";
            displayIdentifierText.Text = "Oui";
            displayIdentifier.IsChecked = _DisplayIdentifier;
            orientation.Text += _Orientation ? "Pointe" : "Plat";
            orientationCbx.IsChecked = _Orientation;

            ySize.Text = _SizeY.ToString();
            xSize.Text = _SizeX.ToString();

            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);

        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _LayerCount = (int)e.NewValue;
            sliderValue.Text = _LayerCount.ToString();

            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);

            ResetSelectedHexagon();
            graphicsView.Invalidate();

        }

        private void orientationCbx_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _Orientation = e.Value;
            orientation.Text = _Orientation ? "Pointe" : "Plat";
            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);

            if (_HasSelectedHexagon)
            {
                hexDrawableIndividual.HexOrientation = _Orientation;
                individualGraphicsView.Invalidate();
            }

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

                hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);
                graphicsView.Invalidate();
            }
        }

        private void displayIdentifier_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            _DisplayIdentifier = e.Value;
            displayIdentifierText.Text = e.Value ? "Oui" : "Non";

            if (_DisplayIdentifier)
            {
                hexDrawableIndividual.Identifier = _SelectedHexagonIdentifier;
            }
            else
            {
                hexDrawableIndividual.Identifier = null;
            }

            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);

            individualGraphicsView.Invalidate();
            graphicsView.Invalidate();

        }

        private void GraphicsView_StartInteraction(object sender, TouchEventArgs e)
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

                    hexDrawableIndividual.HexSize = layout.Size;
                    hexDrawableIndividual.HexOrientation = layout.HexagonOrientation;
                    hexDrawableIndividual.HexColor = Color.FromRgb(hexagon.RGB.R, hexagon.RGB.G, hexagon.RGB.B);
                    hexDrawableIndividual.Identifier = _DisplayIdentifier ? hexagon.Identifier : null;

                    // We need to keep this variable for interactivity.
                    _HasSelectedHexagon = true;
                    _SelectedHexagonIdentifier = hexagon.Identifier;
                    _SelectedHexagonColor = Color.FromRgb(hexagon.RGB.R, hexagon.RGB.G, hexagon.RGB.B);

                    individualGraphicsView.Invalidate();
                }
            }
        }

        private void ResetSelectedHexagon()
        {

            hexDrawableIndividual.HexSize = null;
            hexDrawableIndividual.HexOrientation = true;
            hexDrawableIndividual.HexColor = null;
            hexDrawableIndividual.Identifier = 0;
            individualGraphicsView.Invalidate();

        }

        private void layerMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            string result = Regex.Replace(e.NewTextValue, @"[^0-9]+", "");

            if (Regex.Match(result, @"^[0-9]+$").Success)
            {
                int v = int.Parse(result);
                _LayerMax = v;
                layerSlide.Maximum = _LayerMax;
            }
        }

        private void sortHexagon_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _SortHexagon = e.Value;
            sortHexagonText.Text = _SortHexagon ? "Oui" : "Non";

            hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color, displayNumber: _DisplayIdentifier, sortHexagons: _SortHexagon);
            graphicsView.Invalidate();
        }
    }

}
