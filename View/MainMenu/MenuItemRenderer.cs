using Insomnia.Assets;
using Insomnia.Coroutines;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System;
using System.Collections;

namespace Insomnia.View.MainMenu
{
    public class MenuItemRenderer
    {
        public MenuItem Item { get; }
        public Vector2 SelectedOffset { get; } = new(5, 0);
        public StepTask CurrentAnimation { get; private set; } = null;

        public Vector2 CurrentOffset { get; set; } = Vector2.Zero;

        public Label Label { get; }

        public MenuItemRenderer(string text, MenuItem item, Window window)
        {
            Item = item;
            Item.Selected += OnSelected;
            Item.Deselected += OnDeselected;

            Label = new(text, Fonts.Pico8Mono, Palette.LightGray, window);
        }

        public void Draw(Renderer renderer)
        {
            Label.Position = Item.Position + CurrentOffset;
            Label.Draw(renderer);
        }

        private void OnSelected()
        {
            CurrentAnimation?.Break();
            CurrentAnimation = StepTask.Run(AnimateSelect);
            Label.Color = Palette.White;
        }
        private void OnDeselected()
        {
            CurrentAnimation?.Break();
            CurrentAnimation = StepTask.Run(AnimateDeselect);
            Label.Color = Palette.LightGray;
        }

        private IEnumerator AnimateSelect()
        {
            Vector2 start = CurrentOffset;
            Vector2 end = SelectedOffset;

            yield return StepTask.Yields.Interpolate(e =>
            {
                CurrentOffset = Vector2.Lerp(start, end, (1 - MathF.Pow(1 - e, 2)));
                return e + (Time.Delta / 0.2f);
            });
        }
        private IEnumerator AnimateDeselect()
        {
            Vector2 start = CurrentOffset;
            Vector2 end = Vector2.Zero;

            yield return StepTask.Yields.Interpolate(e =>
            {
                CurrentOffset = Vector2.Lerp(start, end, (1 - MathF.Pow(1 - e, 2)));
                return e + (Time.Delta / 0.2f);
            });
        }
    }
}