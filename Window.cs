using System;
using SDL3;
using static SDL3.SDL;

namespace Insomnia
{
    public unsafe class Window
    {
        public bool IsOpened { get; private set; } = false;
        public bool IsVisible { get; private set; } = true;
        
        public event Action<float> Draw;
        public event Action DrawEnd;

        private IntPtr _handle;
        private IntPtr _renderer;

        private Rect _src, _dst;

        public Window(string title, Point dst, Point src, WindowFlags flags)
        {
            _src = new()
            {
                X = 0,
                Y = 0,
                W = src.X,
                H = src.Y,
            };
            _dst = _src with { W = dst.X, H = dst.Y };
        
            _handle = CreateWindow(title, _dst.W, _dst.H, flags);
            _renderer = CreateRenderer(_handle, "default renderer");
            SetRenderDrawColor(_renderer, 0, 0, 0, 255);

            IsOpened = true;
        }

        public void Close()
        {
            DestroyWindow(_handle);
            IsOpened = false;
        }

        public void RenderFrame()
        {
            PollEvent(out Event e);

            if ((EventType)e.Type == EventType.Quit || e.Key.Key == Keycode.F1)
            {
                Close();
            }

            RenderClear(_renderer);
            RenderPresent(_renderer);

            Delay(16);
        }

        public void Show()
        {
            HideWindow(_handle);
        }
        public void Hide()
        {
            ShowWindow(_handle);
        }
    }
}