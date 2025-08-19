using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;
using Insomnia.View.Elements;

using static SDL3.SDL;
using Palette = Insomnia.Assets.Palette;
using Fonts = Insomnia.Assets.Fonts;
using System.Collections.Generic;
using Insomnia.View.TrayButtons;

namespace Insomnia.View.Windows
{
    public class TrayMenuWindow
    {
        public Window Window { get; private set; }

        public ElementManager ElementManager { get; }

        public Point Source { get; } = new Point(56, 35);
        public Point Destination { get; } = new(112, 70);

        public List<ITrayButton> Buttons { get; } = [];

        private static WindowFlags Flags { get; } =
            WindowFlags.OpenGL |
            WindowFlags.Borderless |
            WindowFlags.Utility |
            WindowFlags.AlwaysOnTop |
            WindowFlags.InputFocus |
            WindowFlags.MouseFocus | 
            WindowFlags.Hidden;

        public const string Name = "";
        
        public TrayMenuWindow() 
        {
            Window = new(Name, Source, Destination, Flags);
            Window.BackgroundColor = Assets.Palette.LightGray;
            Window.Event += OnEvent;

            ElementManager = new(Window);

            InitButtons();
        }

        public void Show()
        {
            Window.Position = ApplyOrigin(Mouse.Position).ToPoint();
            Window.IsVisible = true;
            RaiseWindow(Window.Handle);
        }
        public void Hide() => Window.IsVisible = false;

        private void OnEvent(in Event e)
        {
            if (e.Type == (uint)EventType.WindowFocusLost)
            {
                Hide();
            }
        }
        private Vector2 ApplyOrigin(Vector2 position)
        {
            uint displayId = GetPrimaryDisplay();
            GetDisplayBounds(displayId, out Rect rect);

            Vector2 resolution = new(rect.W, rect.H);

            Vector2 center = resolution / 2;

            position -= new Vector2(
                position.X > center.X ? Destination.X : 0,
                position.Y > center.Y ? Destination.Y : 0);
            
            return position;
        }

        private void InitButtons()
        {
            int yPos = 1;

            Button insomniaButton = CreateButton("Insomnia", ref yPos);
            Button toggleButton = CreateButton(Program.AwakeKeeper.IsActive ? "Disable" : "Enable", ref yPos);
            Button quitButton = CreateButton("Quit", ref yPos);

            Buttons.AddRange(
                new WindowVisibilityButton(insomniaButton),
                new KeeperToggleButton(toggleButton, Program.AwakeKeeper), 
                new QuitButton(quitButton));

            ElementManager.AddRange(
                insomniaButton, 
                toggleButton, 
                quitButton);
        }

        private Button CreateButton(string text, ref int y)
        {
            int height = 11;

            Font font = Fonts.Pico8Mono;
            Label label = new(Window);
            label.CreateTexture(text, font, Palette.Black);
            label.Position = new(9, y + 3);

            Button button = new(label)
            {
                Position = new(1, y),
                Size = new(54, height),
                BackgroundColor = Palette.Black
            };

            y += height;

            void SetSelected()
            {
                label.CreateTexture(label.Text, font, Palette.White);
                button.IsBackVisible = true;
            }
            void SetUnselected()
            {
                label.CreateTexture(label.Text, font, Palette.Black);
                button.IsBackVisible = false;
            }

            button.MouseEnter += SetSelected;
            button.MouseExit += SetUnselected;

            button.MouseClick += () =>
            {
                Hide();
                SetUnselected();
            };
            
            return button;
        }
    }
}