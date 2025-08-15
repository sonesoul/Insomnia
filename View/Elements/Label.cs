using System;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;

namespace Insomnia.View.Elements
{
    public class Label : Element
    {
        public Rectangle Destination { get; set; } 
        public string Text { get; private set; } 
        public Font Font { get; private set; } 
        public Color ForeColor { get; private set; }

        private Texture _texture;

        public Label(Window window, string text, Font font, Rectangle dst) : base(window)
        {
            RenderTexture(text, font, dst);
        }

        public override void Draw(Renderer renderer)
        {
            SDL.RenderTexture(renderer, _texture, IntPtr.Zero, Destination);
        }

        public void RenderTexture(string text, Font font, Rectangle dst)
        {
            Text = text;
            Destination = dst;
            Font = font;

            IntPtr surface = TTF.RenderTextSolid(Font, Text, 0, ForeColor);

            _texture = new Texture(Window.Renderer, surface);
        }
    }
}