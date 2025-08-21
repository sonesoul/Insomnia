using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System;
using System.Collections.Generic;
using Event = SDL3.SDL.Event;
using Keycode = SDL3.SDL.Keycode;

namespace Insomnia.View.MainMenu
{
    public class OptionsMenu
    {
        public Window Window { get; }
        public List<Option> Options { get; } = [];

        public int Index { get; private set; } = -1;
        public Option Item => Options[Index];

        public Point Position { get; } = new(5, 10);

        public Keycode UpKey { get; } = Keycode.Up;
        public Keycode DownKey { get; } = Keycode.Down;

        public MenuRenderer Renderer { get; }

        private List<Keycode> EnterKeys { get; } = [Keycode.Right, Keycode.Return];
        private List<Keycode> ExitKeys { get; } = [Keycode.Left, Keycode.Escape];

        private Action _upAction;
        private Action _downAction;

        private readonly List<Option> _deactivatedOptions = []; 

        public OptionsMenu(Window window)
        {
            Window = window;
            Window.Event += OnEvent;

            RestoreUpdown();

            AddOption("State");
            AddOption("Delay");
            AddOption("Start");
            AddOption("Quit");
            
            Option option = Options[0];
            OnOffValue value = new(Options[0], true);

            value.Renderer = new OnOffRenderer(Window, value);
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

            if (EnterKeys.Contains(keycode))
            {
                Enter();
                return;
            }

            if (ExitKeys.Contains(keycode))
            {
                Exit();
                return;
            }

            if (keycode == DownKey)
            {
                _downAction();
            }

            if (keycode == UpKey)
            {
                _upAction();
            }
        }

        public void Select(int index)
        {
            if (index < 0)
                index = Options.Count - 1;
            else if (index >= Options.Count)
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

            if (Item.State == OptionState.Entered)
            {
                Exit();
                return;
            }

            Item.Enter();
            Item.Value?.RetakeUpDown(out _upAction, out _downAction);
            DeactivateItems();
        }
        public void Exit()
        {
            if (Item.State != OptionState.Entered)
                return;

            Item.Select();
            RestoreUpdown();
            ActivateItems();
        }

        private void RestoreUpdown()
        {
            _upAction = Up;
            _downAction = Down;
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
            Vector2 position = new(Position.X, Position.Y + (7 * Options.Count));

            Option item = new(position);
            item.Renderer = new(text, item, Window);

            Options.Add(item);
        }
    }    
}