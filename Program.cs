global using Point = Insomnia.DirectMedia.Structures.Point;
global using Vector2 = Insomnia.DirectMedia.Structures.Vector2;
global using Rectangle = Insomnia.DirectMedia.Structures.Rectangle;
global using FRectangle = Insomnia.DirectMedia.Structures.FRectangle;
global using Color = Insomnia.DirectMedia.Structures.Color;

using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Structures;
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
                AwakeKeeper.Update();
                Window.Update();
            }
        }
    }
}