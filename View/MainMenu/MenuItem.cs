using System;

namespace Insomnia.View.MainMenu
{
    public class MenuItem(Vector2 position)
    {
        public Vector2 Position { get; set; } = position;
        public Option Option { get; set; } = new();
        public MenuItemRenderer ItemRenderer { get; set; }

        public string Description { get; set; } = "No description";

        public event Action Selected;
        public event Action Deselected;

        public event Action Entered;

        public virtual void Select()
        {
            Selected?.Invoke();
        }
        public virtual void Deselect()
        {
            Deselected?.Invoke();
        }

        public virtual void Enter()
        {
            Entered?.Invoke();
        }
    }
}