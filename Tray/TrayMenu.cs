using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.Tray.Buttons;
using Insomnia.View.Elements;
using System.Collections.Generic;

namespace Insomnia.View
{
    public class TrayMenu
    {
        public List<TrayButton> Items { get; } = [];
        public Window Window { get; }

        public TrayMenu(Window window)
        {
            Window = window;
            window.Draw += Draw;
            window.Event += OnEvent;

            Point windowSize = window.Source.Size;

            Point buttonSize = new(
                windowSize.X - 2, //-2 pixels for bounds
                (windowSize.Y - 2) / 3); //-2 pixels for bounds and / 3 for 3 buttons

            Items.AddRange(
                new InsomniaButton(Window),
                new ToggleButton(Window, Program.AwakeKeeper), 
                new QuitButton(Window));

            PlaceButtons();
        }

        private void Draw(Renderer renderer)
        {
            for (int i = 0; i < Items.Count; i++) 
            {
                Items[i].Button.Draw(renderer);
            } 
        }
        private void OnEvent(in SDL3.SDL.Event e)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Button.OnEvent(e);
            }
        }

        private void PlaceButtons()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                Button button = item.Button;

                button.Position = new(button.Position.X, button.Position.Y + (button.Size.Y * i));
            }
        }
    }
}