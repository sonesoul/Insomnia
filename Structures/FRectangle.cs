namespace Insomnia.DirectMedia.Structures
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
    }
}