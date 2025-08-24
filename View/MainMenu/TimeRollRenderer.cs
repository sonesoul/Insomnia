using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System;
using System.Drawing.Drawing2D;

namespace Insomnia.View.MainMenu
{
    public class TimeRollRenderer : ValueRenderer
    {
        public TimeRollValue OptionValue { get; }

        private Label _label;

        private Font _font;

        private int _charWidth;

        private Color backcolor = Palette.White;

        public TimeRollRenderer(int value, TimeMetric metric, TimeRollValue optionValue, Window window) : base(window)
        {
            OptionValue = optionValue;

            _font = Fonts.Pico8Mono;

            _label = new(string.Empty, _font, Palette.Yellow, window);
            SetLabelText(value, metric);

            var charSize = _font.CharSize;

            _charWidth = charSize.X;

            Size = new(_charWidth * 4, charSize.Y);

            OptionValue.MetricChanged += OnMetricChanged;
            OptionValue.ValueChanged += OnValueChanged;
        }

        public override void Draw(Renderer renderer, Vector2 position)
        {
            int x = (int)position.X + (Size.X / 2) - (int)(_label.Size.X / 2);
            int y = (int)position.Y;

            Point pos = new(x, y);

            _label.Draw(pos, renderer);
        }

        private void OnMetricChanged(TimeMetric metric)
        {
            SetLabelText(OptionValue.Value, metric);
        }
        private void OnValueChanged(int newValue)
        {
            SetLabelText(newValue, OptionValue.Metric);
        }

        private void SetLabelText(int value, TimeMetric metric)
        {
            _label.Text = $"{value:00}{metric.ToString().ToLower()[0]}";
        }
    }
}