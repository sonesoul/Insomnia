using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using System.Collections.Generic;
using static SDL3.SDL;

namespace Insomnia.View
{
    public class ElementManager
    {
        public Window Window { get; }
        private List<Element> Items { get; } = [];

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

        private void OnEvent(in Event e)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                Items[i].OnEvent(e);
            }
        }

        public void Add(Element item)
        {
            Items.Insert(0, item);
        }

        public void AddRange(params Element[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                Add(elements[i]);
            }
        }

        public void Remove(Element item)
        {
            Items.Remove(item);
        }
    }
}