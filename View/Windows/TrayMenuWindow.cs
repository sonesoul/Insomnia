using Insomnia.DirectMedia;
using static SDL3.SDL;

namespace Insomnia.View.Windows
{
    public class TrayMenuWindow
    {
        public Window Window { get; private set; }

        public Point Destination { get; } = new(130, 70);
        public Point Source { get; } = new Point(65, 35);

        private static WindowFlags Flags { get; } =
            WindowFlags.OpenGL |
            WindowFlags.Borderless |
            WindowFlags.Utility |
            WindowFlags.AlwaysOnTop |
            WindowFlags.InputFocus |
            WindowFlags.MouseFocus | 
            WindowFlags.Hidden;

        public const string Name = "";

        public TrayMenuWindow() 
        {
            Window = new(Name, Source, Destination, Flags);
            Window.BackgroundColor = Assets.Palette.LightGray;
            Window.Event += OnEvent;
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