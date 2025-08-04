global using Point = System.Drawing.Point;

using System.Windows.Forms;
using Insomnia.AppTray;

namespace Insomnia
{
    internal class Program
    {
        public static Window Window { get; private set; }
        public static AwakeKeeper AwakeKeeper { get; private set; } = new();
        public static Tray Tray { get; } = new();

        
        public const string Name = "Insomnia";

        private static void Main()
        {
            Window = new(Name, new Point(384, 384), new Point(128, 128), SDL3.SDL.WindowFlags.OpenGL);

            while (Window.IsOpened)
            {
                AwakeKeeper.Update();
                Window.RenderFrame();
            }

            Tray.Icon.Visible = false;
            Application.Exit();
        }
    }
}