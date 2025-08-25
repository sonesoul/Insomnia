using Insomnia.DirectMedia;

namespace Insomnia.Tray.Buttons
{
    public class InsomniaButton(Window window) : TrayButton(Text, window)
    {
        public const string Text = "Insomnia";

        protected override void OnClick()
        {
            base.OnClick();
            Program.MainWindow.ToggleVisibility();
        }
    }
}