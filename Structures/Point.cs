using System;
using System.Diagnostics.CodeAnalysis;

namespace Insomnia.DirectMedia.Structures
{
    public struct Point(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public static Point Zero => new(0, 0);
        public static Point UnitX => new(1, 0);
        public static Point UnitY => new(0, 1);

        public Point(int both) : this(both, both) { }

        public readonly void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
        public static Point operator *(Point a, Point b) => new(a.X* b.X, a.Y* b.Y);
        public static Point operator /(Point a, Point b) => new(a.X / b.X, a.Y / b.Y);
        public static bool operator ==(Point a, Point b) => a.Equals(b);
        public static bool operator !=(Point a, Point b) => !(a == b);

        public readonly override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not Point p)
                return false;

            return Equals(p);
        }
        public readonly override int GetHashCode() => HashCode.Combine(X, Y);

        private readonly bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}