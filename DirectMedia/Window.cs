using SDL3;
using System;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public unsafe class Window
    {
        public bool IsRunning { get; private set; } = false;
        public bool IsVisible { get; private set; } = true;
        public string Title { get; private set; }
        
        public event Action<float> Draw;
        public event Action DrawEnd;

        private IntPtr _handle;
        private IntPtr _renderTarget;
        private IntPtr _renderer;

        private FRect _src;
        private FRect _dst;

        private string _title;

        public Window(string title, Point src, Point dst, WindowFlags flags)
        {
            _src = new FRect { W = src.X, H = src.Y };
            _dst = new FRect { W = dst.X, H = dst.Y };

            _title = title;

            CreateWindowAndRenderer(_title, (int)_dst.W, (int)_dst.H, flags,
                out _handle, 
                out _renderer);

            _renderTarget = CreateTexture(_renderer, PixelFormat.RGB24, TextureAccess.Target, (int)_src.W, (int)_src.H);
            
            SetRenderDrawColor(_renderer, 0, 0, 0, 255);
            SetTextureScaleMode(_renderTarget, ScaleMode.Nearest);

            IsRunning = true;
        }

        public void Close()
        {
            DestroyWindow(_handle);
            IsRunning = false;
        }

        public void Update()
        {
            PollEvent(out Event e);

            if ((EventType)e.Type == EventType.Quit || e.Key.Key == Keycode.F1)
            {
                Close();
            }

            SetRenderTarget(_renderer, _renderTarget);
            SetRenderDrawColor(_renderer, 255, 255, 255, 255);

            //draw everything

            SetRenderTarget(_renderer, IntPtr.Zero);
            SetRenderDrawColor(_renderer, 0, 0, 0, 255);
            RenderClear(_renderer);

            SetRenderDrawColor(_renderer, 255, 255, 255, 255);
            RenderTexture(_renderer, _renderTarget, IntPtr.Zero, IntPtr.Zero);            

            RenderPresent(_renderer);

            Delay(32);
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