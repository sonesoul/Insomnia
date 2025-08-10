using Insomnia.DirectMedia.Types;
using System;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public unsafe class Window : IDisposable
    {
        public Keycode ExitKey { get; set; } = Keycode.F1;
        public Color BackgroundColor { get; set; } = Color.Black;

        public bool IsDisposed { get; private set; } = false;

        public event Action<Renderer> Draw;
        public event Action<Event> Event;
        public event Action Disposed;

        private IntPtr _handle;

        private Renderer _renderer;
        private Texture _texture;

        private Rectangle _src;
        private Rectangle _dst;

        public Window(string title, Point src, Point dst, WindowFlags flags)
        {
            _src = new Rectangle(Point.Zero, src);
            _dst = new Rectangle(Point.Zero, dst);

            _handle = CreateWindow(title, dst.X, dst.Y, flags);
            _renderer = new(_handle);
            _texture = new(_renderer, src, PixelFormat.RGB24, TextureAccess.Target);

            _renderer.SetColor(Color.Black);
            _texture.SetScaleMode(ScaleMode.Nearest);
        }

        public void PollEvents()
        {
            while (PollEvent(out Event e))
            {
                if (e.Key.Key == ExitKey)
                {
                    Dispose();
                    return;
                }
                
                Event?.Invoke(e);
            }
        }
        public void Render()
        {
            _renderer.SetTarget(_texture);
            _renderer.Clear(BackgroundColor);

            Draw?.Invoke(_renderer);
            
            _renderer.UnsetTarget();

            Present();
        }
        
        public void Delay(uint ms) => SDL3.SDL.Delay(ms);

        public void Show() => ShowWindow(_handle);
        public void Hide() => HideWindow(_handle);

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            DestroyWindow(_handle);
            DestroyTexture(_texture);
            DestroyRenderer(_renderer);

            GC.SuppressFinalize(this);
            Disposed?.Invoke();
        }

        private void Present()
        {
            RenderTexture(_renderer, _texture, _src, _dst);
            RenderPresent(_renderer);
        }
    }
}