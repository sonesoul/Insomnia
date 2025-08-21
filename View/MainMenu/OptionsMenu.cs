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
        public List<Option> Items { get; } = [];

        public int Index { get; private set; } = -1;
        public Option Item => Items[Index];

        public Point Position { get; } = new(5, 10);

        public Keycode UpKey { get; } = Keycode.Up;
        public Keycode DownKey { get; } = Keycode.Down;

        private List<Keycode> EnterKeys { get; } = [Keycode.Right, Keycode.Return];
        private List<Keycode> ExitKeys { get; } = [Keycode.Left, Keycode.Escape];

        private Action _upAction;
        private Action _downAction;

        private readonly int _yOffset = 7;
        private readonly Label _arrowLabel;

        private readonly List<Option> _deactivatedOptions = []; 

        public OptionsMenu(Window window)
        {
            Window = window;
            Window.Event += OnEvent;
            Window.Draw += Draw;

            RestoreUpdown();

            _arrowLabel = new(">", Fonts.Pico8Mono, Palette.White, Window);

            AddOption("State");
            AddOption("Delay");
            AddOption("Start");
            AddOption("Quit");

            OnOffValue value = new(Items[0], true);
            OnOffRenderer renderer = new(Window, value);

            Option option = Items[0];

            option.Value = value;
            option.IsActive = true;
            
            Select(0);
        }

        private void Draw(Renderer renderer)
        {
            _arrowLabel.Position = new(Position.X - 4, Position.Y + _yOffset * Index);
            _arrowLabel.Draw(renderer);

            for (int i = 0; i < Items.Count; i++) 
            {
                Items[i].Renderer.Draw(renderer);
            } 
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
                index = Items.Count - 1;
            else if (index >= Items.Count)
                index = 0;

            if (index != Index)
            {
                if (Index >= 0)
                    Item.Reset();

                Index = index;
                Items[Index].Select();
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
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == Index)
                    continue;

                Option option = Items[i];

                if (option.IsActive)
                {
                    option.IsActive = false;
                    _deactivatedOptions.Add(option);
                }
            }
        }

        private void AddOption(string text)
        {
            Vector2 position = new(Position.X, Position.Y + (_yOffset * Items.Count));

            Option item = new(position);
            item.Renderer = new(text, item, Window);

            Items.Add(item);
        }
    }    
}