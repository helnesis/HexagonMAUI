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
                //Regular expressions are used to match whether the user input contains numbers
                string result = Regex.Replace(e.NewTextValue, @"[^0-9]+", "");
                if (Regex.Match(result, @"^[0-9]+$").Success)
                {
                    if (entry.StyleId == "entryX")
                    {
                        _SizeX = double.Parse(result);
                    }
                    else
                    {
                        _SizeY = double.Parse(result);
                    }
                }

                hexDrawable.ApplySettings(layerCount: _LayerCount, size: new(_SizeX, _SizeY), orientation: _Orientation, hasColor: _Color);
                graphicsView.Invalidate();
            }
        }

        private void xSize_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
