using Insomnia.DirectMedia;

namespace Insomnia.View.Tray.Buttons
{
    public class QuitButton(Window window) : TrayButton(Text, window)
    {
        public const string Text = "Quit";

        protected override void OnClick()
        {
            base.OnClick();
            Program.Quit();
        }
    }
}