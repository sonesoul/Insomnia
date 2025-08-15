global using Insomnia.Structures;
global using Color = Insomnia.Structures.Color;
global using FRectangle = Insomnia.Structures.FRectangle;
global using Point = Insomnia.Structures.Point;
global using Rectangle = Insomnia.Structures.Rectangle;
global using Vector2 = Insomnia.Structures.Vector2;

using Insomnia.DirectMedia;
using Insomnia.View.Windows;
using System;
using System.Collections.Generic;
using System.Threading;
using WindowFlags = SDL3.SDL.WindowFlags;

namespace Insomnia
{
    internal class Program
    {
        public static Window Window { get; private set; }
        public static TrayMenuWindow TrayMenu { get; private set; }
        public static AwakeKeeper AwakeKeeper { get; private set; } = new();
        public static bool IsWorking { get; set; } = true;
        
        public const string Name = "Insomnia";

        public const int FPS = 60;
        public const int FrameTime = 1000 / FPS;

        private static IReadOnlyList<Window> Instances { get; } = Window.GetInstances();

        private static void Main()
        {
            SDL3.TTF.Init();

            Window = new(Name, new Point(128, 128), new Point(384, 384), WindowFlags.OpenGL);
            TrayMenu = new();
            
            Window.Event += e =>
            {
                if (e.Key.Key == SDL3.SDL.Keycode.F2)
                {
                    TrayMenu.Show();
                }

                if (e.Type == (int)SDL3.SDL.EventType.WindowCloseRequested)
                {
                    
                }
            };

            while (Instances.Count > 0)
            {
                AwakeKeeper.Update();

                Window.Delay(FrameTime);
                Window.PollEvents();

                IterateInstances(w => w.HandleEvents());
                IterateInstances(w => w.Render());
            }

            TrayMenu.Window.Dispose();
        }

        private static void IterateInstances(Action<Window> action)
        {
            var instances = Instances;

            for (int i = instances.Count - 1; i >= 0; i--)
            {
                action(instances[i]);
            }
        }
    }
}