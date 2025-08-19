using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;
using System;

namespace Insomnia.View.Elements
{
    public class Label : Element
    {
        public float Scale { get; } = 1f;

        public string Text { get => _text; set => CreateTexture(value, Font); } 
        public Font Font { get => _font; set => CreateTexture(Text, value); } 
        public Color Color { get => _color; set => Texture?.SetColor(value); }
        public Point Size { get; private set; }

        public Texture Texture { get; private set; } 

        private Color _color = Color.White;
        private string _text;
        private Font _font;

        public Label(string text, Font font, Window window) : base(window)
        {
            CreateTexture(text, font);
        }
        public Label(string text, Font font, Color color, Window window) : base(window)
        {
            CreateTexture(text, font);
            SetColor(color);
        }

        public override void Draw(Renderer renderer)
        {
            SDL.FRect dst = new()
            {
                X = (int)Position.X, 
                Y = (int)Position.Y, 
                W = Size.X * Scale, 
                H = Size.Y * Scale, 
            };
            SDL.RenderTexture(renderer, Texture, IntPtr.Zero, dst);
        }

        public void CreateTexture(string text, Font font)
        {
            _text = text;
            _font = font;

            IntPtr surface = TTF.RenderTextSolid(Font, Text, 0, Color.White);

            Texture = new Texture(Window.Renderer, surface);
            Size = Font.MeasureString(Text);
            SetColor(_color);
        }

        private void SetColor(Color c)
        {
            _color = c;
            Texture.SetColor(c);
        }
    }
}