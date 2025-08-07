using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using SDLColor = SDL3.SDL.Color;

namespace Insomnia.Structures
{
    [DebuggerDisplay("{ToString()}")]
    public struct Color(byte r, byte g, byte b, byte a)
    {
        public byte Red = r;
        public byte Green = g;
        public byte Blue = b;
        public byte Alpha = a;

        public static Color White => new(255, 255, 255, 255);
        public static Color Black => new(0, 0, 0, 255);

        public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

        public readonly override string ToString() => $"{Red} {Green} {Blue} [{Alpha}]";
        public readonly override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not Color other)
            {
                return false;
            }

            return Equals(other);
        }
        public readonly override int GetHashCode() => HashCode.Combine(Red, Green, Blue, Alpha);

        private readonly bool Equals(Color other)
        {
            return
                Red == other.Red &&
                Green == other.Green &&
                Blue == other.Blue &&
                Alpha == other.Alpha;
        }

        public static bool operator ==(Color left, Color right) => left.Equals(right);
        public static bool operator !=(Color left, Color right) => !(left == right);

        public static implicit operator SDLColor(Color color) 
        {
            return new()
            {
                R = color.Red,
                G = color.Green,
                B = color.Blue,
                A = color.Alpha
            };
        }
    }
}