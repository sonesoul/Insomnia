using Insomnia.View.Elements;

namespace Insomnia.View.TrayButtons
{
    public class WindowVisibilityButton : ITrayButton
    {
        public Button Button { get; private set; }

        public WindowVisibilityButton(Button button)
        {
            Button = button;
            Button.MouseClick += Program.MainWindow.ToggleVisibility;
        }
    }
}