using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using static SDL3.SDL;

namespace Insomnia.View
{
    public abstract class Element(Window window) : IDrawable, IEventListener
    {
        public Window Window { get; } = window;

        public virtual void OnEvent(in Event e)
        {

        }
        public virtual void Draw(Renderer renderer)
        {
            
        }
    }
}