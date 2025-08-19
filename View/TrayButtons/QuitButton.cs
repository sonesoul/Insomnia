using Insomnia.View.Elements;

namespace Insomnia.View.TrayButtons
{
    public class QuitButton : ITrayButton
    {
        public const string ActiveString = "Disable";
        public const string InactiveString = "Enable";

        public Button Button { get; private set; }
        
        public QuitButton(Button button)
        {
            Button = button;
            button.MouseClick += Program.Quit;
        }
    }
}