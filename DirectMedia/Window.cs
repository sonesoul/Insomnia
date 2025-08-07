using System;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public unsafe class Window : IDisposable
    {
        public bool IsVisible { get; private set; } = true;

        public event Action Draw;
        public event Action<Event> Event;
        public event Action Disposed;

        private IntPtr _handle;
        private IntPtr _renderer;
        private IntPtr _texture;

        private Rectangle _src;
        private Rectangle _dst;

        private bool _disposed = false;

        public Window(string title, Point src, Point dst, WindowFlags flags)
        {
            _src = new Rectangle(Point.Zero, src);
            _dst = new Rectangle(Point.Zero, dst);

            InitWindow(title, flags);
            InitTexture(PixelFormat.ARGB64, TextureAccess.Target);

            SetRenderDrawColor(_renderer, 0, 0, 0, 255);
            SetTextureScaleMode(_texture, ScaleMode.Nearest);
        }

        public void Update()
        {
            PollEvent(out Event e);

            if ((EventType)e.Type == EventType.Quit || e.Key.Key == Keycode.F1)
            {
                Dispose();
                return;
            }

            Event?.Invoke(e);

            if (_disposed)
                return;

            SetTarget(_texture);
            Clear(Color.White);

            Draw?.Invoke();

            SetTarget(null);
            Clear(Color.Black);
            
            RenderTexture(_renderer, _texture, IntPtr.Zero, IntPtr.Zero);
            RenderPresent(_renderer);

            Delay(16);
        }

        public void Show() => ShowWindow(_handle);
        public void Hide() => HideWindow(_handle);

        public void SetColor(Color c)
        {
            SetRenderDrawColor(_renderer, c.Red, c.Green, c.Blue, c.Alpha);
        }
        public void SetTarget(IntPtr? renderTarget)
        {
            SetRenderTarget(_renderer, renderTarget ?? IntPtr.Zero);
        }
        public void Clear(Color color)
        {
            SetColor(color);
            RenderClear(_renderer);
        }
       
        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            DestroyWindow(_handle);
            GC.SuppressFinalize(this);
            Disposed?.Invoke();
        }

        private void InitWindow(string title, WindowFlags flags)
        {
            _dst.Size.Deconstruct(out int w, out int h);

            _handle = CreateWindow(title, w, h, flags);
            _renderer = CreateRenderer(_handle, null);
        }
        private void InitTexture(PixelFormat format, TextureAccess access)
        {
            _src.Size.Deconstruct(out int w, out int h);
            
            _texture = CreateTexture(_renderer, format, access, w, h);
        }
    }
}