using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;
using System;

namespace Insomnia.View.Elements
{
    public class Button : Element
    {
        public ButtonRenderer Renderer { get; set; }
        public ref FRectangle Bounds => ref _bounds;

        public event Action MouseEnter;
        public event Action MouseLeave;
        public event Action MouseClick;

        private bool _containsMouse = false;

        public Button(Window window) : base(window)
        {
            Renderer = new("Button", Fonts.Pico8, this);
        }
        public Button(string text, Font font, Window window) : base(window)
        {
            Renderer = new(text, font, this);
        }

        public override void OnEvent(in SDL.Event e)
        {
            if (e.Type == (uint)SDL.EventType.MouseMotion)
            {
                CheckContainsMouse();
            }

            if (e.Button.Button == (uint)SDL.MouseButtonFlags.Left && e.Button.Down)
            {
                if (_containsMouse)
                    MouseClick?.Invoke();
            }
        }
        public override void Draw(Renderer renderer)
        {
            Renderer?.Draw(renderer);
        }

        private void CheckContainsMouse()
        {
            Vector2 pos = Mouse.GetRelativePosition(Window);
            bool mouseInBounds = _bounds.Contains((int)pos.X, (int)pos.Y);

            if (!_containsMouse && mouseInBounds)
            {
                _containsMouse = true;
                MouseEnter?.Invoke();
            }
            else if (_containsMouse && !mouseInBounds)
            {
                _containsMouse = false;
                MouseLeave?.Invoke();
            }
        }
    }
}