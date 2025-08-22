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

        public Keycode UpKey { get; } = Keycode.Up;
        public Keycode DownKey { get; } = Keycode.Down;

        public MenuRenderer Renderer { get; }
        
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
            SwitchValue value = new(Options[0], true);

            value.Renderer = new SwitchRenderer("Yes", "No", Window, value);
            option.Value = value;
            option.IsActive = true;

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
        }
        public void Exit(bool saveChanges)
        {
            Item.Select();
            ActivateItems();

            if (saveChanges)
                Item.Value.Apply();
            else
                Item.Value.Discard();

            _state = MenuState.Selecting;
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

            _bindings = new()
            {
                [MenuState.Selecting] = new()
                {
                    [up] = Up,
                    [down] = Down,
                    [enter] = Enter
                },
                [MenuState.Editing] = new()
                {
                    [up] = () => Item.Value?.Up(),   // если Value имеет Up/Down методы
                    [down] = () => Item.Value?.Down(),
                    [enter] = () => Exit(true),
                    [escape] = () => Exit(false)
                }
            };
        }
    }    
}