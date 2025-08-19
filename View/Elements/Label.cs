using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;
using System;

namespace Insomnia.View.Elements
{
    public class Label(Window window) : Element(window)
    {
        public float Scale { get; } = 1f;

        public string Text { get; private set; } 
        public Font Font { get; private set; } 
        public Color ForeColor { get; private set; }
        public Point Size { get; private set; }

        public Texture Texture { get; private set; } = new Texture(window.Renderer, IntPtr.Zero);

        public override void Draw(Renderer renderer)
        {
            SDL.FRect dst = new()
            {
                X = Position.X, 
                Y = Position.Y,
                W = Size.X * Scale, 
                H = Size.Y * Scale, 
            };
            SDL.RenderTexture(renderer, Texture, IntPtr.Zero, dst);
        }

        public void CreateTexture(string text, Font font, Color c)
        {
            Text = text;
            Font = font;
            ForeColor = c;

            IntPtr surface = TTF.RenderTextSolid(Font, Text, 0, c);

            Texture = new Texture(Window.Renderer, surface);
            Size = Font.MeasureString(Text);
        }
    }
}