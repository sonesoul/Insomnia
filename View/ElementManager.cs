using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using System.Collections.Generic;
using static SDL3.SDL;

namespace Insomnia.View
{
    public class ElementManager
    {
        public Window Window { get; }
        private List<WindowElement> Items { get; } = [];

        public ElementManager(Window window)
        {
            Window = window;

            window.Draw += OnDraw;
            window.Event += OnEvent;
        }

        private void OnDraw(Renderer renderer)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                Items[i].Draw(renderer);
            }
        }

        private void OnEvent(Event e)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                Items[i].OnEvent(e);
            }
        }

        public void Add(WindowElement item) => Items.Insert(0, item);
        public void Remove(WindowElement item) => Items.Remove(item);
    }
}