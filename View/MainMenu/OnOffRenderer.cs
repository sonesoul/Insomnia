using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using static System.Windows.Forms.Design.AxImporter;

namespace Insomnia.View.MainMenu
{
    public class OnOffRenderer : ValueRenderer
    {
        public OnOffValue Value { get; }

        private readonly Label _label;
        private Point _position;

        public OnOffRenderer(Window window, OnOffValue value) : base(window)
        {
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
            _label.Text = isOn ? "On" : "Off";
            _label.Color = isOn ? Palette.Green : Palette.Red;

            /*Point optionPos = Value.Option.Position.ToPoint();

            int x = optionPos.X + (Size.X / 2) - (int)(_label.Size.X / 2);
            int y = optionPos.Y;

            _position = new Point(x, y);*/
        }
    }
}
