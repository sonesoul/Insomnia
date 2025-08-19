using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;
using System.Collections.Generic;

namespace Insomnia.View.MainMenu
{
    public class OptionsMenu
    {
        public Window Window { get; }
        public List<MenuItem> Items { get; } = [];

        public int Index { get; private set; } = -1;

        public Point StartPosition { get; } = new(5, 0);
        public int YOffset { get; } = 7;

        private Label ArrowLabel { get; }

        public OptionsMenu(Window window)
        {
            Window = window;
            Window.Event += OnEvent;
            Window.Draw += Draw;

            ArrowLabel = new(">", Fonts.Pico8Mono, Palette.White, Window);
            
            AddItem("State");
            AddItem("Delay");
            AddItem("Start");
            AddItem("Quit");

            Select(0);
        }

        private void Draw(Renderer renderer)
        {
            ArrowLabel.Position = new(StartPosition.X - 4, StartPosition.Y + YOffset * Index);
            ArrowLabel.Draw(renderer);

            for (int i = 0; i < Items.Count; i++) 
            {
                Items[i].ItemRenderer.Draw(renderer);
            } 
        }
        private void OnEvent(in SDL3.SDL.Event e)
        {
            if (e.Key.Key == SDL3.SDL.Keycode.Up && e.Key.Down)
            {
                Select(Index - 1);
            }

            if (e.Key.Key == SDL3.SDL.Keycode.Down && e.Key.Down)
            {
                Select(Index + 1);
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
                if (Index >= 0 && index < Items.Count)
                    Items[Index].Deselect();
                
                Index = index;
                Items[Index].Select();
            }
        }

        public void AddItem(string text)
        {
            Vector2 position = new(StartPosition.X, StartPosition.Y + (YOffset * Items.Count));

            MenuItem item = new(position);
            item.ItemRenderer = new(text, item, Window);

            Items.Add(item);
        }
    }    
}