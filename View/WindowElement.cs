using Insomnia.DirectMedia.Types;
using static SDL3.SDL;

namespace Insomnia.View
{
    public abstract class WindowElement : IDrawable, IEventListener
    {
        public abstract void OnEvent(Event e);
        public abstract void Draw(Renderer renderer);
    }
}