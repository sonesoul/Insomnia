using Insomnia.DirectMedia;
using Insomnia.Assets;

using static SDL3.SDL;
using Insomnia.View;

namespace Insomnia.Windows
{
    public class TrayWindow
    {
        public Window Window { get; private set; }

        public Point Source { get; } = new Structures.Point(56, 35);
        public Point Destination { get; } = new(112, 70);

        private static WindowFlags Flags { get; } =
            WindowFlags.OpenGL |
            WindowFlags.Borderless |
            WindowFlags.Utility |
            WindowFlags.AlwaysOnTop |
            WindowFlags.InputFocus |
            WindowFlags.MouseFocus | 
            WindowFlags.Hidden;

        public TrayMenu Menu { get; }

        public const string Name = "";
        
        public TrayWindow() 
        {
            Window = new(Name, Source, Destination, Flags);
            Window.BackgroundColor = Assets.Palette.LightGray;
            Window.Event += OnEvent;

            Menu = new(Window);
        }

        public void Show()
        {
            Window.Position = ApplyOrigin(Mouse.Position).ToPoint();
            Window.IsVisible = true;
            RaiseWindow(Window.Handle);
        }
        public void Hide() => Window.IsVisible = false;

        private void OnEvent(in Event e)
        {
            if (e.Type == (uint)EventType.WindowFocusLost)
            {
                Hide();
            }
        }
        private Vector2 ApplyOrigin(Vector2 position)
        {
            uint displayId = GetPrimaryDisplay();
            GetDisplayBounds(displayId, out Rect rect);

            Vector2 resolution = new(rect.W, rect.H);

            Vector2 center = resolution / 2;

            position -= new Vector2(
                position.X > center.X ? Destination.X : 0,
                position.Y > center.Y ? Destination.Y : 0);

            return position;
        }
    }
}