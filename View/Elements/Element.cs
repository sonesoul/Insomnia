using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using static SDL3.SDL;

namespace Insomnia.View.Elements
{
    public abstract class Element(Window window) : IDrawable, IEventListener
    {
        public Window Window { get; } = window;

        public Vector2 Position { get => _bounds.Position; set => _bounds.Position = value; }
        public Vector2 Size { get => _bounds.Size; set => _bounds.Size = value; }

        protected FRectangle _bounds;

        public virtual void OnEvent(in Event e)
        {

        }
        public virtual void Draw(Renderer renderer)
        {
            
        }
    }
}