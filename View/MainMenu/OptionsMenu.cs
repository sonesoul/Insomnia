using Insomnia.DirectMedia;
using System;
using System.Collections.Generic;
using Event = SDL3.SDL.Event;
using Keycode = SDL3.SDL.Keycode;

namespace Insomnia.View.MainMenu
{
    enum MenuState
    {
        Selecting,
        Editing, 
    }

    public class OptionsMenu
    {
        public Window Window { get; }
        public List<Option> Options { get; } = [];

        public int Index { get; private set; } = -1;
        public Option Item => Options[Index];

        public MenuRenderer Renderer { get; }

        public event Action Selected;
        public event Action Entered;
        public event Action Exited;

        public event Action ValueChanged;
        public event Action ChangesApplied;

        private Dictionary<MenuState, Dictionary<Keycode, Action>> _bindings;

        private MenuState _state = MenuState.Selecting;
        private readonly List<Option> _deactivatedOptions = []; 

        public OptionsMenu(Window window)
        {
            Window = window;
            Window.Event += OnEvent;
            
            InitBindings();

            AddOption("State");
            AddOption("Delay");
            AddOption("Start");
            AddOption("Quit");

            Option option = Options[0];
            SwitchValue switchValue = new(option, true);

            switchValue.Renderer = new SwitchRenderer("Yes", "No", switchValue, Window);
            option.Value = switchValue;

            option = Options[1];
            TimeRollValue timeValue = new(option, TimeMetric.Seconds, 1);

            timeValue.Renderer = new TimeRollRenderer(timeValue.Value, timeValue.Metric, timeValue, Window);
            option.Value = timeValue;

            Select(0);

            Renderer = new(this);
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
                    Item.Reset();

                Index = index;
                Options[Index].Select();
            }
        }

        public void Up() => Select(Index - 1);
        public void Down() => Select(Index + 1);

        public void Enter()
        {
            if (!Item.IsActive)
                return;

            Item.Enter();
            DeactivateItems();

            _state = MenuState.Editing;
            Entered?.Invoke();
        }
        public void Exit()
        {
            Item.Select();
            ActivateItems();

            _state = MenuState.Selecting;
            Item.Value.Discard();
            Exited?.Invoke();
        }

        private void Apply()
        {
            var value = Item.Value;

            if (value != null)
            {
                value.Apply();
                ChangesApplied?.Invoke();
            }
        }

        private void ChangeValue(Action<OptionValue> valueAction)
        {
            var value = Item.Value;

            if (value != null)
            {
                valueAction(value);
                ValueChanged?.Invoke();
            }
        }

        private void ActivateItems()
        {
            for (int i = _deactivatedOptions.Count - 1; i >= 0; i--)
            {
                _deactivatedOptions[i].IsActive = true;
                _deactivatedOptions.RemoveAt(i);
            }
        }
        private void DeactivateItems()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                if (i == Index)
                    continue;

                Option option = Options[i];

                if (option.IsActive)
                {
                    option.IsActive = false;
                    _deactivatedOptions.Add(option);
                }
            }
        }

        private void AddOption(string text)
        {
            Option item = new(text);
            item.Renderer = new(item, Window);

            Options.Add(item);
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