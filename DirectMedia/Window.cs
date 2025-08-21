using Insomnia.DirectMedia.Types;
using System;
using System.Collections.Generic;
using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public delegate void WindowEventHandler(in Event e);

    public class Window : IDisposable
    {    
        private static class WindowRegistry 
        {
            private static List<Window> Instances { get; } = [];
            private static Dictionary<uint, Queue<Event>> Infos { get; } = [];

            public static void Register(Window window) 
            {
                Instances.Add(window);
                Infos.Add(window.ID, []);
            }
            public static void Unregister(Window window)
            {
                Instances.Remove(window);
                Infos.Remove(window.ID);
            }

            public static void PollEvents()
            {
                while (PollEvent(out Event e))
                {
                    if (e.Key.Key == ExitKey)
                    {
                        for (int i = Instances.Count - 1; i >= 0; i--)
                        {
                            Instances[i].Dispose();
                        }

                        return;
                    }

                    if (Infos.TryGetValue(e.Window.WindowID, out Queue<Event> queue))
                    {
                        queue.Enqueue(e);
                    }
                }
            }

            public static Queue<Event> RetrieveEvents(uint id) => Infos[id];
            public static IReadOnlyList<Window> GetInstances() => Instances;

        }
        public static Keycode ExitKey { get; set; } = Keycode.F1;

        public Renderer Renderer { get; }
        public Texture Texture { get; }

        public Rectangle Source { get; set; }
        public Rectangle Destination { get; set; }

        public Point Position
        {
            get
            {
                GetWindowPosition(Handle, out int x, out int y);
                return new(x, y);
            }
            set => SetWindowPosition(Handle, value.X, value.Y);
        }

        public Color BackgroundColor { get; set; } = Color.Black;
        public bool IsDisposed { get; private set; } = false;
        public bool IsVisible 
        {
            get => !GetWindowFlags(Handle).HasFlag(WindowFlags.Hidden);
            set
            {
                if (value)
                {
                    ShowWindow(Handle);
                }
                else
                {
                    HideWindow(Handle);
                }
            }
        }

        public IntPtr Handle { get; }
        public uint ID { get; }

        public event WindowEventHandler Event;
        public event Action<Renderer> Draw;
        public event Action Disposed;

        public Window(string title, Point src, Point dst, WindowFlags flags)
        {
            Source = new Rectangle(Point.Zero, src);
            Destination = new Rectangle(Point.Zero, dst);

            Handle = CreateWindow(title, dst.X, dst.Y, flags);
            ID = GetWindowID(Handle);

            Renderer = new(Handle);
            Texture = new(Renderer, src, PixelFormat.RGB24, TextureAccess.Target);

            Renderer.SetColor(Color.Black);
            Texture.SetScaleMode(ScaleMode.Nearest);

            WindowRegistry.Register(this);
        }

        public void HandleEvents()
        {
            Queue<Event> events = WindowRegistry.RetrieveEvents(ID);

            while (events.TryDequeue(out Event e)) 
            { 
                Event?.Invoke(e);
            }
        }
        public void Render()
        {
            Renderer.SetTarget(Texture);
            Renderer.Clear(BackgroundColor);

            Draw?.Invoke(Renderer);

            Renderer.UnsetTarget();

            RenderTexture(Renderer, Texture, Source, Destination);
            RenderPresent(Renderer);
        }        
        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            DestroyWindow(Handle);
            DestroyTexture(Texture);
            DestroyRenderer(Renderer);

            WindowRegistry.Unregister(this);

            GC.SuppressFinalize(this);
            Disposed?.Invoke();
        }
        
        public static void Delay(uint ms) => SDL3.SDL.Delay(ms);
        public static void PollEvents() => WindowRegistry.PollEvents();
        public static IReadOnlyList<Window> GetInstances() => WindowRegistry.GetInstances();
    }
}