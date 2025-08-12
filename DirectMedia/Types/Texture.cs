using System;
using static SDL3.SDL;

namespace Insomnia.DirectMedia.Types
{
    public class Texture : Resource
    {
        public Renderer Renderer { get; }

        public Texture(Renderer renderer, Point size, PixelFormat format, TextureAccess access)
        {
            Renderer = renderer;
            Pointer = CreateTexture(Renderer, format, access, size.X, size.Y);
        }

        public Texture(Renderer renderer, IntPtr surface)
        {
            Renderer = renderer;
            Pointer = CreateTextureFromSurface(renderer, surface);
        }

        public void SetScaleMode(ScaleMode mode)
        {
            SetTextureScaleMode(this, mode);
        }

        public void SetColor(Color c)
        {
            SetTextureColorMod(this, c.Red, c.Green, c.Blue);
        }

        protected override void Destroy()
        {
            DestroyTexture(this);
        }
    }
}