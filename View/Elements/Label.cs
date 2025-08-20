using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using SDL3;
using System;

namespace Insomnia.View.Elements
{
    public class Label : Element
    {
        public string Text { get => _text; set => CreateTexture(value, Font); } 
        public Font Font { get => _font; set => CreateTexture(Text, value); } 
        public Color Color { get => _color; set => Texture?.SetColor(value); }

        public ref FRectangle Bounds => ref _bounds;

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
            SDL.FRect dst = _bounds;
            dst.X = (int)_bounds.X;
            dst.Y = (int)_bounds.Y;
            
            SDL.RenderTexture(renderer, Texture, IntPtr.Zero, dst);
        }
        public void Draw(Point position, Renderer renderer)
        {
            SDL.FRect dst = new()
            {
                X = position.X,
                Y = position.Y,
                W = _bounds.Width, 
                H = _bounds.Height,
            };

            SDL.RenderTexture(renderer, Texture, IntPtr.Zero, dst);
        }


        public void CreateTexture(string text, Font font)
        {
            _text = text;
            _font = font;

            IntPtr surface = TTF.RenderTextSolid(Font, Text, 0, Color.White);

            Texture = new Texture(Window.Renderer, surface);
            _bounds.Size = Font.MeasureString(Text).ToVector2();
            SetColor(_color);
        }

        private void SetColor(Color c)
        {
            _color = c;
            Texture.SetColor(c);
        }
    }
}