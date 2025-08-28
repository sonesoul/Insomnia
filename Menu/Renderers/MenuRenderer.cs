using Insomnia.Assets;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Insomnia.Menu.Renderers
{
    public class MenuRenderer
    {
        public OptionsMenu Menu { get; }

        public Vector2 Position { get; set; } = new(0, 5);
        public int ItemHeight { get; } = 7;

        public Point CharSize { get; }
        private List<Option> Options => Menu.Options;

        private Point _arrowOffset = new(0, 10);
        private Vector2 _itemsOffset = new(5, 10);
        private Point _descriptionOffset = new(5, 0);

        private readonly Label _arrow;
        private readonly Label _filledArrow;

        private readonly Label _descriptionLabel;

        private readonly Label _versionLabel;

        private bool _changesApplied = true;


        private readonly static string FilledArrowSymbol = $"{(char)0x00DB}";
        private const string ArrowSymbol = ">";

        public MenuRenderer(OptionsMenu menu)
        {
            Font font = Fonts.Pico8;
            CharSize = font.CharSize;

            var window = menu.Window;

            Menu = menu;
            _arrow = new(ArrowSymbol, font, window);
            _filledArrow = new(FilledArrowSymbol, font, window);
            _descriptionLabel = new(string.Empty, font, Palette.Indigo, window);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            
            _versionLabel = new($"v{version.Major}.{version.Minor}", font, Palette.DarkGray, window);

            menu.Window.Draw += Draw;

            menu.ChangesApplied += () => _changesApplied = true;
            menu.ValueChanged += () => _changesApplied = false;
            menu.ChangesDiscarded += () => _changesApplied = true;

            menu.Selected += o => _descriptionLabel.Text = o.Description;
            menu.Entered += o => _descriptionLabel.Text = o.Value.Description;
            menu.Exited += o => _descriptionLabel.Text = o.Description;
        }

        private void Draw(Renderer renderer)
        {
            Point arrowPos = new Point(
                    (int)Position.X, 
                    (int)Position.Y + ItemHeight * Menu.Index) + _arrowOffset;

            Action<Point, Renderer> drawArrow = _changesApplied ? _filledArrow.Draw : _arrow.Draw;

            drawArrow(arrowPos, renderer);

            for (int i = 0; i < Options.Count; i++)
            {
                Vector2 optionPos = new Vector2(
                        Position.X, 
                        Position.Y + ItemHeight * i) + _itemsOffset;

                var item = Options[i].Renderer;
                item.Draw(renderer, optionPos);
            }

            _descriptionLabel.Draw(Position.ToPoint() + _descriptionOffset, renderer);

            Point versionPos = Menu.Window.Source.Size;
            versionPos.X = 5;
            versionPos.Y -= CharSize.Y + 5;
            _versionLabel.Draw(versionPos, renderer);
        }
    }
}