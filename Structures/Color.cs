using SDLColor = SDL3.SDL.Color;

namespace Insomnia.DirectMedia.Structures
{
    public struct Color(byte r, byte g, byte b, byte a)
    {
        public byte Red = r;
        public byte Green = g;
        public byte Blue = b;
        public byte Alpha = a;

        public static Color White => new(255, 255, 255, 255);
        public static Color Black => new(0, 0, 0, 255);

        public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

        public static implicit operator SDLColor(Color color) 
        {
            return new()
            {
                R = color.Red,
                G = color.Green,
                B = color.Blue,
                A = color.Alpha
            };
        };
    }
}