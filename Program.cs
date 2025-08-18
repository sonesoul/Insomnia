global using Insomnia.Structures;
global using Color = Insomnia.Structures.Color;
global using FRectangle = Insomnia.Structures.FRectangle;
global using Point = Insomnia.Structures.Point;
global using Rectangle = Insomnia.Structures.Rectangle;
global using Vector2 = Insomnia.Structures.Vector2;

using Insomnia.DirectMedia;
using Insomnia.View;
using Insomnia.View.Windows;
using System;
using System.Collections.Generic;

namespace Insomnia
{
    internal class Program
    {
        public static MainWindow MainWindow { get; private set; }
        public static TrayMenuWindow TrayWindow { get; private set; }
        public static TrayIcon TrayIcon { get; private set; }
        public static AwakeKeeper AwakeKeeper { get; private set; } 
        public static bool IsWorking { get; set; } = true;
        
        public const string Name = "Insomnia";

        public const int FPS = 60;
        public const int FrameTime = 1000 / FPS;

        private static IReadOnlyList<Window> Instances { get; } = Window.GetInstances();

        private static void Main()
        {
            SDL3.TTF.Init();

            InitializeComponents();

            while (Instances.Count > 0)
            {
                AwakeKeeper.Update();

                Window.Delay(FrameTime);
                Window.PollEvents();

                IterateInstances(w => w.HandleEvents());
                IterateInstances(w => w.Render());
            }
        }

        private static void InitializeComponents()
        {
            AwakeKeeper = new(); 
            MainWindow = new();
            TrayWindow = new();
            
            TrayIcon = new(AwakeKeeper);
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