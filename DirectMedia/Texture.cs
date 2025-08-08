using System;
using SDL3;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public class Texture : IDisposable
    {
        public IntPtr Pointer { get; } 
        public Renderer Renderer { get; }
        public Point Size { get; private set; }
        public bool IsDisposed { get; private set; } = false;

        public Texture(Renderer renderer, Point size, PixelFormat format, TextureAccess access)
        {
            Renderer = renderer;
            Pointer = CreateTexture(Renderer, format, access, size.X, size.Y);
            Size = size;
        }

        public void SetScaleMode(ScaleMode mode)
        {
            SetTextureScaleMode(this, mode);
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            DestroyTexture(this);
            GC.SuppressFinalize(this);
        }

        public static implicit operator IntPtr(Texture t) => t.Pointer;
    }
}