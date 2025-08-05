using DrawingColor = System.Drawing.Color;
using SDLColor = SDL3.SDL.Color;

namespace Insomnia.Assets
{
    public struct RGBA(byte r, byte g, byte b, byte a)
    {
        public byte Red = r;
        public byte Green = g;
        public byte Blue = b;
        public byte Alpha = a;

        public RGBA(byte r, byte g, byte b) : this(r, g, b, byte.MaxValue)
        {
        }

        public static implicit operator DrawingColor(RGBA rgba) => DrawingColor.FromArgb(rgba.Alpha, rgba.Red, rgba.Green, rgba.Blue);
        public static implicit operator SDLColor(RGBA rgba) => new() { R = rgba.Red, G = rgba.Green, B = rgba.Blue, A = rgba.Alpha };
    }
}