using System;
using System.Diagnostics.CodeAnalysis;

namespace Insomnia.Structures
{
    public struct Rectangle(Point position, Point size)
    {
        public Point Position { readonly get => _pos; set => _pos = value; } 
        public Point Size { readonly get => _size; set => _size = value; } 
        
        public int X { readonly get => _pos.X; set => _pos.X = value; }
        public int Y { readonly get => _pos.Y; set => _pos.Y = value; }
        public int Width { readonly get => _size.X; set => _size.X = value; }
        public int Height { readonly get => _size.Y; set => _size.Y = value; }

        private Point _pos = position, _size = size;

        public Rectangle(int x, int y, int width, int height) : this(new Point(x, y), new Point(width, height) )
        {
            
        }

        public readonly override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not Rectangle frect)
            {
                return false;
            }

            return Equals(frect);
        }
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Position.GetHashCode(), Size.GetHashCode());
        }

        private readonly bool Equals(Rectangle other)
        {
            return Position.Equals(other.Position) && Size.Equals(other.Size);
        }
        
        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);
        public static bool operator !=(Rectangle left, Rectangle right) => !(left == right);
    }
}