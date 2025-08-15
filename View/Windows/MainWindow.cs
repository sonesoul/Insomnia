using Insomnia.DirectMedia;
using System;
using static SDL3.SDL;

namespace Insomnia.View.Windows
{
    public class MainWindow
    {
        public Window Window { get; set; }

        public const string Name = "Insomnia";
        
        public MainWindow()
        {
            Window = new(Name, new Point(128, 128), new Point(384, 384), WindowFlags.OpenGL);
            Window.Event += OnEvent;
        }

        private void OnEvent(Event e)
        {
            if (e.Key.Key == Keycode.F2)
            {
                Program.TrayMenu.Show();
            }

            if (e.Type == (uint)EventType.WindowCloseRequested)
            {
                Window.IsVisible = false;
            }
        }
    }
}