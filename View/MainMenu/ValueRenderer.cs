using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;

namespace Insomnia.View.MainMenu
{
    public abstract class ValueRenderer(Window window)
    {
        public virtual Point Size { get; protected set; }
        public Window Window { get; protected set; } = window;

        public abstract void Draw(Renderer renderer, Vector2 position);
    }

}