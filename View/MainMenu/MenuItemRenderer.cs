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

        public Vector2 EnterOffset { get; set; } = new(15, 0);

        public Label Label { get; }

        public MenuItemRenderer(string text, MenuItem item, Window window)
        {
            Item = item;
            Item.Selected += OnSelected;
            Item.Deselected += OnDeselected;
            Item.Entered += OnEntered;

            Label = new(text, Fonts.Pico8Mono, Palette.LightGray, window);
        }

        public void Draw(Renderer renderer)
        {
            Label.Draw((Item.Position + CurrentOffset).ToPoint(), renderer);
        }

        private void OnSelected()
        {
            CurrentAnimation?.Break();
            CurrentAnimation = StepTask.Run(() => LerpOffset(SelectedOffset));
            Label.Color = Palette.White;
        }
        private void OnDeselected()
        {
            CurrentAnimation?.Break();
            CurrentAnimation = StepTask.Run(() => LerpOffset(Vector2.Zero));
            Label.Color = Palette.LightGray;
        }
        
        private void OnEntered()
        {
            CurrentAnimation?.Break();
            CurrentAnimation = StepTask.Run(() => LerpOffset(EnterOffset));
            Label.Color = Palette.DarkGray;
        }

        private IEnumerator LerpOffset(Vector2 end)
        {
            Vector2 start = CurrentOffset;

            yield return StepTask.Yields.Interpolate(e =>
            {
                CurrentOffset = Vector2.Lerp(start, end, (1 - MathF.Pow(1 - e, 2)));
                return e + (Time.Delta / 0.2f);
            });
        }
    }
}