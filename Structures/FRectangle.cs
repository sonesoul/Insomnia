using System;
using System.Diagnostics.CodeAnalysis;

namespace Insomnia.Structures
{
    public struct FRectangle(Vector2 position, Vector2 size)
    {
        public Vector2 Position { readonly get => _pos; set => _pos = value; }
        public Vector2 Size { readonly get => _size; set => _size = value; }

        public float X { readonly get => _pos.X; set => _pos.X = value; }
        public float Y { readonly get => _pos.Y; set => _pos.Y = value; }
        public float Width { readonly get => _size.X; set => _size.X = value; }
        public float Height { readonly get => _size.Y; set => _size.Y = value; }

        private Vector2 _pos = position, _size = size;

        public FRectangle(float x, float y, float width, float height) : this(new(x, y), new(width, height)) { }

        public static bool operator ==(FRectangle left, FRectangle right) => left.Equals(right); 
        public static bool operator !=(FRectangle left, FRectangle right) => !(left == right); 
        
        public readonly override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not FRectangle frect)
            {
                return false;
            }

            return Equals(frect);
        }
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Position.GetHashCode(), Size.GetHashCode());
        }

        private readonly bool Equals(FRectangle other)
        {
            return Position.Equals(other.Position) && Size.Equals(other.Size);
        }
    }
}