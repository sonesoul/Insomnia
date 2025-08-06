using System;
using System.Diagnostics.CodeAnalysis;

namespace Insomnia.DirectMedia.Structures
{
    public struct Vector2(float x, float y)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;

        public static Vector2 Zero => new(0, 0);
        public static Vector2 UnitX => new(1, 0);
        public static Vector2 UnitY => new(0, 1);

        public Vector2(float both) : this(both, both) { }

        public readonly void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
        public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

        public readonly override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not Vector2 v)
                return false;

            return Equals(v);
        }
        public readonly override int GetHashCode() => HashCode.Combine(X, Y);

        private readonly bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}