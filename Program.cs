global using Point = System.Drawing.Point;

using System;
using System.Threading;
using System.Windows.Forms;
using Raylib_cs;
using Insomnia.AppTray;

namespace Insomnia
{
    internal class Program
    {
        public static Window Window { get; private set; } = new();
        public static AwakeKeeper AwakeKeeper { get; private set; } = new();
        public static Tray Tray { get; } = new();

        public const string Name = "Insomnia";

        private static void Main()
        {
            Window.Open();

            while (Window.IsOpened)
            {
                if (Raylib.IsKeyDown(KeyboardKey.F1))
                {
                    Window.Close();
                    break;
                }
                else if (Raylib.WindowShouldClose())
                {
                    Window.Hide();
                }

                AwakeKeeper.Update();
                Window.RenderFrame();
            }

            Tray.Icon.Visible = false;
            Application.Exit();
        }
    }
}