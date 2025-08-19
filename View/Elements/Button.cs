using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;
using System;

namespace Insomnia.View.Elements
{
    public class Button(Label label) : Element(label.Window)
    {
        public new Point Position { get => _bounds.Position; set => _bounds.Position = value; }
        public Point Size { get => _bounds.Size; set => _bounds.Size = value; }
        
        public Label Label { get; set; } = label;

        public Color BackgroundColor { get; set; } = Assets.Palette.Transparent;

        public bool IsBackVisible { get; set; } = false;

        public event Action MouseEnter;
        public event Action MouseExit;

        public event Action MouseClick;

        private bool _containsMouse = false;

        private Rectangle _bounds;

        public override void OnEvent(in SDL.Event e)
        {
            if (e.Type == (uint)SDL.EventType.MouseMotion)
            {
                CheckMouseInBounds();
            }

            if (e.Button.Button == (uint)SDL.MouseButtonFlags.Left && e.Button.Down)
            {
                if (_containsMouse)
                    MouseClick?.Invoke();
            }
        }

        public override void Draw(Renderer renderer)
        {
            if (IsBackVisible)
                SDL.RenderFillRect(renderer, _bounds);
            
            Label.Draw(renderer);
        }

        private void CheckMouseInBounds()
        {
            bool mouseInBounds = _bounds.Contains(Mouse.GetRelativePosition(Window).ToPoint());

            if (mouseInBounds && !_containsMouse)
            {
                MouseEnter?.Invoke();
                _containsMouse = true;
            }
            else if (!mouseInBounds && _containsMouse)
            {
                MouseExit?.Invoke();
                _containsMouse = false;
            }
        }
    }
}