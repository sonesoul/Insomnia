using Insomnia.Assets;
using Insomnia.Coroutines;
using System.Collections;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System.Collections.Generic;

namespace Insomnia.View.MainMenu
{
    public class MenuRenderer
    {
        public OptionsMenu Menu { get; }

        public Vector2 Position { get; set; } = new(0, 0);
        public int ItemHeight { get; } = 7;

        public Point CharSize { get; }
        private List<Option> Options => Menu.Options;

        private Point _arrowOffset = new(0, 10);
        private Vector2 _itemsOffset = new(5, 10);
        private Point _descriptionOffset = new(5, 0);

        private readonly Label _arrowLabel;
        private readonly Label _descriptionLabel;

        private bool _changesUnapplied = true;

        public MenuRenderer(OptionsMenu menu)
        {
            Font font = Fonts.Pico8;
            CharSize = font.CharSize;

            Menu = menu;
            _arrowLabel = new(">", font, menu.Window);
            _descriptionLabel = new("", font, Palette.Indigo, menu.Window);

            menu.Window.Draw += Draw;

            menu.ChangesApplied += () => _changesUnapplied = true;
            menu.ValueChanged += () => _changesUnapplied = false;
            menu.ChangesDiscarded += () => _changesUnapplied = true;

            menu.Selected += o => _descriptionLabel.Text = o.Description;
            menu.Entered += o => _descriptionLabel.Text = o.Value.Description;
            menu.Exited += o => _descriptionLabel.Text = o.Description;
        }

        private void Draw(Renderer renderer)
        {
            Point arrowPos = new Point(
                    (int)Position.X, 
                    (int)Position.Y + (ItemHeight * Menu.Index)) + _arrowOffset;

            _arrowLabel.Draw(arrowPos, renderer);
            
            if (_changesUnapplied)
            {
                for (int i = 0; i < 2; i++)
                {
                    arrowPos.X--;
                    _arrowLabel.Draw(arrowPos, renderer);
                }
            }

            for (int i = 0; i < Options.Count; i++)
            {
                Vector2 position = new Vector2(
                        Position.X, 
                        Position.Y + (ItemHeight * i)) + _itemsOffset;

                var item = Options[i].Renderer;
                item.Draw(renderer, position);
            }

            _descriptionLabel.Draw(Position.ToPoint() + _descriptionOffset, renderer);
        }
    }
}