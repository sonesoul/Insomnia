using Insomnia.DirectMedia;
using static SDL3.SDL;

namespace Insomnia.View.Windows
{
    public class MainWindow
    {
        public Window Window { get; set; }

        public ElementManager ElementManager { get; } 

        public Point Source { get; } = new Point(128);
        public Point Destination { get; } = new(384);

        public const string Name = "Insomnia";
        
        public MainWindow()
        {
            Window = new(Name, Source, Destination, WindowFlags.OpenGL);
            Window.Event += OnEvent;

            ElementManager = new(Window);
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