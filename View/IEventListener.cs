using static SDL3.SDL;

namespace Insomnia.View.Elements
{
    public interface IEventListener
    {
        public abstract void OnEvent(in Event e);
    }
}