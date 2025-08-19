using Insomnia.View.Elements;

namespace Insomnia.View.TrayButtons
{
    public class KeeperToggleButton : ITrayButton
    {
        public const string ActiveString = "Disable";
        public const string InactiveString = "Enable";
        
        public Button Button { get; private set; }
        private AwakeKeeper _awakeKeeper;

        public KeeperToggleButton(Button button, AwakeKeeper awakeKeeper)
        {
            _awakeKeeper = awakeKeeper;
            Button = button;

            SetLabelText();
            button.MouseClick += OnButtonClick; 
        }

        private void OnButtonClick()
        {
            AwakeKeeper keeper = _awakeKeeper;
            keeper.IsActive = !keeper.IsActive;
            SetLabelText();
        }
        private void SetLabelText()
        {
            Button.Label.Text = _awakeKeeper.IsActive ? ActiveString : InactiveString;
        }
    }
}