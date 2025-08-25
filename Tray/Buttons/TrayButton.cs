using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.View.Elements;

namespace Insomnia.Tray.Buttons
{
    public abstract class TrayButton
    {
        public Button Button { get; }
        public Window Window { get; }

        protected TrayButton(string text, Window window)
        {
            Button = new Button(text, Fonts.Pico8, window);
            Window = window;

            Button.Renderer.Label.Position = new(6, 3);
            Button.Bounds = new(1, 1, 54, 11);

            Button.MouseEnter += OnMouseEnter;
            Button.MouseLeave += OnMouseLeave;
            Button.MouseClick += OnClick;
        }

        protected virtual void OnClick()
        {
            Window.IsVisible = false;
        }
        protected virtual void OnMouseEnter()
        {

        }
        protected virtual void OnMouseLeave()
        {

        }
    }
}