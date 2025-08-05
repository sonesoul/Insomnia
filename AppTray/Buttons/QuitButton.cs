using Insomnia.DirectMedia;
using System;

namespace Insomnia.AppTray.Buttons
{
    public class QuitButton : TrayButton
    {
        public Window Window { get; }

        public const string ButtonText = "Quit";

        public QuitButton(Window window) : base(ButtonText)
        {
            Window = window;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            throw new NotImplementedException();
        }
    }
}