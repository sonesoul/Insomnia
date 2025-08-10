using Insomnia.Assets;
using Insomnia.DirectMedia.Types;
using System;

namespace Insomnia.UI.Tray
{
    public class Button : IDrawable
    {
        public Font Font { get; set; } 
        
        public Rectangle Bounds { get; set; } 
        public string Text { get; set; } 
        public Color Color { get; set; }

        public Button(string text)
        {
            Text = text;
            int width = 4 * text.Length;
            int height = 5;

            Font = new(Asset.GetAbsolutePath("pico-8-mono.ttf"), 8f);
            Bounds = new(0, 0, width * 2, height * 2);            
        }

        public unsafe void Draw(Renderer renderer)
        {
            IntPtr surface = SDL3.TTF.RenderTextSolid(Font, Text, 0, Palette.White);
            Texture texture = new(renderer, surface);
            SDL3.SDL.SetTextureScaleMode(texture, SDL3.SDL.ScaleMode.Nearest);

            SDL3.SDL.RenderTexture(renderer, texture, IntPtr.Zero, Bounds);
        }
    }
}