using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.Menu.Values;
using Insomnia.View.Elements;

namespace Insomnia.Menu.Renderers
{
    public class SwitchRenderer : ValueRenderer
    {
        public SwitchValue OptionValue { get; }

        private readonly Label _label;

        public string PositiveString { get; set; } 
        public string NegativeString { get; set; }

        public const string DefaultPositive = "On";
        public const string DefaultNegative = "Off";

        public SwitchRenderer(SwitchValue optionValue, Window window) : this(DefaultPositive, DefaultNegative, optionValue, window)
        {
            
        }
        public SwitchRenderer(string positive, string negative, SwitchValue optionValue, Window window) : base(window)
        {
            PositiveString = positive;
            NegativeString = negative;

            OptionValue = optionValue;

            optionValue.TurnedOn += () => SetLabel(isOn: true);
            optionValue.TurnedOff += () => SetLabel(isOn: false);

            _label = new(string.Empty, Fonts.Pico8, window);
            Point charSize = Fonts.Pico8.CharSize;
            Size = new Point(charSize.X * 4, charSize.Y);

            SetLabel(optionValue.IsOn);
        }

        public override void Draw(Renderer renderer, Vector2 position)
        {
            int x = (int)position.X + Size.X / 2 - (int)(_label.Size.X / 2);
            int y = (int)position.Y;

            Point pos = new(x, y);

            _label.Draw(pos, renderer);
        }

        private void SetLabel(bool isOn)
        {
            _label.Text = isOn ? PositiveString : NegativeString;
            _label.Color = isOn ? Palette.Green : Palette.Red;
        }
    }
}
