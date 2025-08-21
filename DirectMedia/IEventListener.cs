using static SDL3.SDL;

namespace Insomnia.DirectMedia
{
    public interface IEventListener
    {
        public abstract void OnEvent(in Event e);
    }
}