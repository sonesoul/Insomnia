using Insomnia.DirectMedia;
using Insomnia.View.MainMenu;
using static SDL3.SDL;

namespace Insomnia.View.Windows
{
    public class MainWindow
    {
        public Window Window { get; set; }
        public OptionsMenu Menu { get; set; }

        public Point Source { get; } = new Point(128);
        public Point Destination { get; } = new(384);

        public const string Name = "Insomnia";
        
        public void ToggleVisibility()
        {
            Window.IsVisible = !Window.IsVisible;
        }

        public MainWindow()
        {
            Window = new(Name, Source, Destination, WindowFlags.OpenGL);
            Window.Event += OnEvent;

            Menu = new(Window);
        }

        private void OnEvent(in Event e)
        {
            if (e.Type == (uint)EventType.WindowCloseRequested)
            {
                Window.IsVisible = false;
            }
        }
    }
}