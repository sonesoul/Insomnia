using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;

namespace Insomnia.View.MainMenu
{
    public class SwitchRenderer : ValueRenderer
    {
        public SwitchValue Value { get; }

        private readonly Label _label;

        public string PositiveString { get; set; }
        public string NegativeString { get; set; }

        public SwitchRenderer(string positive, string negative, Window window, SwitchValue value) : base(window)
        {
            PositiveString = positive;
            NegativeString = negative;

            Value = value;

            value.TurnedOn += () => SetLabel(isOn: true);
            value.TurnedOff += () => SetLabel(isOn: false);

            _label = new(string.Empty, Fonts.Pico8Mono, window);
            Point charSize = Fonts.Pico8Mono.CharSize;
            Size = new Point(charSize.X * 4, charSize.Y);

            SetLabel(value.IsOn);
        }

        public override void Draw(Renderer renderer, Vector2 position)
        {
            int x = (int)position.X + (Size.X / 2) - (int)(_label.Size.X / 2);
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
