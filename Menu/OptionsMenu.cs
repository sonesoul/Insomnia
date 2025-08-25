using Insomnia.DirectMedia;
using Insomnia.Menu.Renderers;
using Insomnia.Menu.Values;
using System;
using System.Collections.Generic;
using Insomnia.Menu.Options;
using Event = SDL3.SDL.Event;
using Keycode = SDL3.SDL.Keycode;

namespace Insomnia.Menu
{
    public enum MenuState
    {
        Selecting,
        Editing, 
    }

    public class OptionsMenu
    {
        public Window Window { get; }
        public List<Option> Options { get; } = [];

        public int Index { get; private set; } = -1;
        public Option Option => Options[Index];

        public MenuRenderer Renderer { get; }

        public event Action<Option> Selected;
        public event Action<Option> Entered;
        public event Action<Option> Exited;

        public event Action ValueChanged;
        public event Action ChangesApplied;
        public event Action ChangesDiscarded;

        private Dictionary<MenuState, Dictionary<Keycode, Action>> _bindings;
        private MenuState _state = MenuState.Selecting;

        public OptionsMenu(Window window)
        {
            Window = window;
            Window.Event += OnEvent;
            
            InitBindings();

            Options.AddRange([
                new StateOption(window),
                new DelayOption(window), 
                new AutorunOption(window),
                new QuitOption(window),
            ]);

            Renderer = new(this);

            Select(0);
        }

        private void OnEvent(in Event e)
        {
            var key = e.Key;
            var keycode = key.Key;
            
            if (!key.Down)
                return;

            if (_bindings[_state].TryGetValue(keycode, out Action action))
            {
                action?.Invoke();
            }
        }

        public void Select(int index)
        {
            if (index < 0)
                index = Options.Count - 1;
            
            if (index >= Options.Count)
                index = 0;

            if (index != Index)
            {
                if (Index >= 0)
                    Option.Reset();

                Index = index;
                Options[Index].Select();
                Selected?.Invoke(Option);
            }
        }

        public void Up() => Select(Index - 1);
        public void Down() => Select(Index + 1);

        public void Enter()
        {
            if (!Option.IsActive)
                return;

            Option.Enter();
            _state = MenuState.Editing;
            SetItemsActive(false);

            Entered?.Invoke(Option);
        }
        public void Exit()
        {
            Option.Value.Discard();
            ChangesDiscarded?.Invoke();

            Option.Select();
            _state = MenuState.Selecting;
            SetItemsActive(true);

            Exited?.Invoke(Option);
        }

        private void Apply()
        {
            var value = Option.Value;

            if (value != null)
            {
                value.Apply();
                ChangesApplied?.Invoke();
            }
        }

        private void ChangeValue(Action<OptionValue> valueAction)
        {
            var value = Option.Value;

            if (value != null)
            {
                valueAction(value);
                ValueChanged?.Invoke();
            }
        }

        private void SetItemsActive(bool enabled)
        {
            for (int i = 0; i < Options.Count; i++)
            {
                if (i == Index)
                    continue;

                Options[i].IsActive = enabled;
            }
        }

        private void InitBindings()
        {
            Keycode enter = Keycode.Return;
            Keycode escape = Keycode.Escape;

            Keycode up = Keycode.Up;
            Keycode down = Keycode.Down;
            Keycode left = Keycode.Left;
            Keycode right = Keycode.Right;

            _bindings = new()
            {
                [MenuState.Selecting] = new()
                {
                    [up] = Up,
                    [down] = Down,

                    [enter] = Enter,
                    [right] = Enter,
                },
                [MenuState.Editing] = new()
                {
                    [up] = () => ChangeValue(v => v.Up()),
                    [down] = () => ChangeValue(v => v.Down()),
                    [enter] = Apply,

                    [escape] = Exit,
                    [left] = Exit,
                }
            };
        }
    }
}