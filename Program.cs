global using Point = Insomnia.Structures.Point;
global using Vector2 = Insomnia.Structures.Vector2;
global using Rectangle = Insomnia.Structures.Rectangle;
global using FRectangle = Insomnia.Structures.FRectangle;
global using Color = Insomnia.Structures.Color;

using Insomnia.DirectMedia;
using WindowFlags = SDL3.SDL.WindowFlags;

namespace Insomnia
{
    internal class Program
    {
        public static Window Window { get; private set; }
        public static AwakeKeeper AwakeKeeper { get; private set; } = new();
        
        public const string Name = "Insomnia";

        private static void Main()
        {
            bool disposed = false;

            Window = new(Name, new Point(128, 128), new Point(384, 384), WindowFlags.OpenGL);
            Window.Disposed += () => disposed = true;

            while (!disposed)
            {
                Window.PollEvents();
                Window.Render();

                AwakeKeeper.Update();
            }
        }
    }
}