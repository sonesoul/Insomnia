using System;
using Insomnia.DirectMedia;

namespace Insomnia.View.MainMenu
{
    public class MenuItem(Vector2 position)
    {
        public Vector2 Position { get; set; } = position;
        public MenuItemRenderer ItemRenderer { get; set; }

        public event Action Selected;
        public event Action Deselected;

        public virtual void Select()
        {
            Selected?.Invoke();
        }
        public virtual void Deselect()
        {
            Deselected?.Invoke();
        }
    }
}
