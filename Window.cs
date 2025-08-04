using SDL3;
using System;
using System.Drawing;
using static SDL3.SDL;

namespace Insomnia
{
    public unsafe class Window
    {
        public bool IsOpened { get; private set; } = true;
        public bool IsVisible { get; private set; } = true;

        public event Action<float> Draw;
        public event Action DrawEnd;

        private nint? _handle;
        private Rect _src, _dst;

        private bool initialized = false;

        public Window(string title, Point size, Point res, WindowFlags flags)
        {
            _handle = CreateWindow(title, size.X, size.Y, flags);

            _src = new()
            {
                X = 0,
                Y = 0,
                W = size.X,
                H = size.Y,
            };
            _dst = _src with { W = res.X, H = res.Y };
        }

        public void Open()
        {
            //not implemented
        }
        public void Close()
        {
            if (!_handle.HasValue)
                throw new InvalidOperationException("Attempt to close a window by null pointer handle.");
                
            DestroyWindow(_handle.Value);

            _handle = null;
        }

        public void RenderFrame()
        {

        }

        private void BeginDrawing()
        {
            
        }
        private void EndDrawing()
        {
           
        }

        public void Show()
        {
        }
        public void Hide()
        {
        }
    }
}