global using Point = System.Drawing.Point;
using System;
using System.Windows.Forms;
using Insomnia.AppTray;
using Insomnia.DirectMedia;
using WindowFlags = SDL3.SDL.WindowFlags;

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
            Window = new(Name, new Point(128, 128), new Point(384, 384), WindowFlags.OpenGL);

            while (Window.IsRunning)
            {
                AwakeKeeper.Update();

                Window.Update();
            }

            Tray.Icon.Visible = false;
        }
    }
}