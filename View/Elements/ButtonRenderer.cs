using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;

namespace Insomnia.View.Elements
{
    public class ButtonRenderer : IDrawable
    {
        public Button Button { get; }
        public Label Label { get; }

        public Vector2 LabelOffset { get; set; } = Vector2.Zero;

        public Color UnselectedLabelColor { get; set; } = Palette.Black;
        public Color SelectedLabelColor { get; set; } = Palette.White;

        public Color BackgroundColor { get; set; } = Palette.Black;

        private bool _backgroundVisible = false;

        public ButtonRenderer(string text, Font font, Button button)
        {
            Button = button;
            Label = new(text, font, UnselectedLabelColor, button.Window);

            Button.MouseEnter += OnMouseEnter;
            Button.MouseLeave += OnMouseLeave;
        }

        public void Draw(Renderer renderer)
        {
            if (_backgroundVisible)
            {
                renderer.SetColor(BackgroundColor);
                SDL3.SDL.RenderFillRect(renderer, (SDL3.SDL.FRect)Button.Bounds);
            }

            Label.Draw((Label.Position + Button.Position).ToPoint(), renderer);
        }

        private void OnMouseLeave()
        {
            _backgroundVisible = false;
            Label.Color = UnselectedLabelColor;
        }

        private void OnMouseEnter()
        {
            _backgroundVisible = true;
            Label.Color = SelectedLabelColor;
        }
    }
}