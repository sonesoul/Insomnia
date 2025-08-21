using Insomnia.Assets;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System.Collections.Generic;

namespace Insomnia.View.MainMenu
{
    public class MenuRenderer
    {
        public OptionsMenu Menu { get; }

        public Vector2 Position { get; set; } = new(0, 10);
        public int ItemHeight { get; } = 7;

        private List<Option> Options => Menu.Options;
        private Label _arrowLabel;

        public MenuRenderer(OptionsMenu menu)
        {
            Menu = menu;
            _arrowLabel = new(">", Fonts.Pico8Mono, menu.Window);
            menu.Window.Draw += Draw;
        }

        private void Draw(Renderer renderer)
        {
            _arrowLabel.Draw(new Point((int)Position.X, (int)Position.Y + ItemHeight * Menu.Index), renderer);
            
            for (int i = 0; i < Options.Count; i++)
            {
                var item = Options[i].Renderer;
                item.Draw(renderer);
            }
        }
    }
}