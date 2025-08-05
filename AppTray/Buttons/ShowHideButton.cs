using Insomnia.DirectMedia;
using System;

namespace Insomnia.AppTray.Buttons
{
    public class ShowHideButton : TrayButton
    {
        public Window Window { get; }

        public const string ButtonText = "Insomnia";

        public ShowHideButton(Window window) : base(ButtonText) 
        { 
            Window = window;
        }

        protected override void OnClick(EventArgs e)
        {
            if (Window.IsVisible)
                Window.Hide();
            else 
                Window.Show();
        }
    }
}