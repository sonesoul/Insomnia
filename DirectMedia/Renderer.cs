using System;
using SDL3;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public unsafe class Renderer(IntPtr handle) : IDisposable
    {
        public IntPtr Pointer { get; } = CreateRenderer(handle, null);
        public Color Color => _color;
        public bool IsDisposed { get; private set; } = false;

        private Color _color;

        public void RenderTexture(Texture texture, Rectangle? src, Rectangle? dst)
        {
            IntPtr srcPtr = IntPtr.Zero;
            IntPtr dstPtr = IntPtr.Zero;

            if (src.HasValue)
            {
                Rectangle value = src.Value;
                srcPtr = new IntPtr(&value);
            }

            if (dst.HasValue)
            {
                Rectangle value = src.Value;
                dstPtr = new IntPtr(&value);
            }

            SDL.RenderTexture(this, texture, srcPtr, dstPtr);
        }
        public void EndRender() => RenderPresent(this);

        public void SetColor(Color c)
        {
            SetRenderDrawColor(
                this, 
                c.Red, 
                c.Green, 
                c.Blue, 
                c.Alpha);

            _color = c;
        }
        public void Clear(Color color)
        {
            Color temp = Color;
            SetColor(color);
            RenderClear(this);
            SetColor(temp);
        }

        public void SetTarget(IntPtr texture)
        {
            SetRenderTarget(this, texture);
        }
        public void UnsetTarget()
        {
            SetTarget(IntPtr.Zero);
        }

        public void Dispose()
        {
            if (IsDisposed) 
                return;

            IsDisposed = true;
             
            DestroyRenderer(this);
            GC.SuppressFinalize(this);
        }

        public static implicit operator IntPtr(Renderer r) => r.Pointer;
    }
}