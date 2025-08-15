using static SDL3.SDL;

namespace Insomnia.View
{
    public interface IEventListener
    {
        public abstract void OnEvent(Event e);
    }
}