using Insomnia.Assets;
using Insomnia.Coroutines;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System;
using System.Collections;

namespace Insomnia.View.MainMenu
{
    public class OptionRenderer
    {
        public Option Option { get; }
        
        public Vector2 SelectedOffset { get; set; } = new(5, 0);
        public Vector2 CurrentOffset { get; private set; } = Vector2.Zero;

        private StepTask _animation = null;

        private readonly Label _label;
        private OptionValue Value => Option.Value;

        public OptionRenderer(Option option, Window window)
        {
            Option = option;

            _label = new(option.Name, Fonts.Pico8Mono, Palette.LightGray, window);

            Option.StateChanged += StateChanged;

            Option.Activated += OnActivated;
            Option.Deactivated += OnDeactivated;

            if (option.IsActive)
                OnActivated();
            else
                OnDeactivated();
        }

        public void Draw(Renderer renderer, Vector2 position)
        {
            bool entered = Option.State == OptionState.Entered;

            if (entered)
                Value?.Renderer?.Draw(renderer, position);

            _label.Draw((position + CurrentOffset).ToPoint(), renderer); 
        }

        private void OnActivated()
        {
            _label.Color = Palette.LightGray;
        }
        private void OnDeactivated()
        {
            _label.Color = Palette.DarkGray;
        }

        private void StateChanged(OptionState state)
        {
            switch (state) 
            {
                case OptionState.Selected:
                    _label.Color = Palette.White;
                    MoveLabel(SelectedOffset);
                    break;
                case OptionState.Avaliable:
                    _label.Color = Palette.LightGray;
                    MoveLabel(Vector2.Zero);
                    break;
                    
                case OptionState.Entered:
                    MoveLabel(new(Value.Renderer.Size.X, 0));
                    break;
            } 

        }

        private void MoveLabel(Vector2 offset)
        {
            _animation?.Break();
            _animation = StepTask.Run(() => LerpOffset(offset));
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

        private IEnumerator ChangeColor(Color color, float seconds)
        {
            Color temp = _label.Color;
            _label.Color = color;
            yield return StepTask.Yields.WaitForSeconds(seconds);
            _label.Color = temp;
        }
    }
}