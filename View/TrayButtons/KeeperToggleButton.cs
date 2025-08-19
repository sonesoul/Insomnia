using Insomnia.View.Elements;
using Insomnia.DirectMedia;
using Insomnia.DirectMedia.Types;

namespace Insomnia.View.TrayButtons
{
    public class KeeperToggleButton : ITrayButton
    {
        public const string ActiveString = "Disable";
        public const string InactiveString = "Enable";
        
        public Button Button { get; private set; }
        private AwakeKeeper _awakeKeeper;

        private Font Font => Button.Label.Font;
        private Color Color => Button.Label.ForeColor;

        public KeeperToggleButton(Button button, AwakeKeeper awakeKeeper)
        {
            _awakeKeeper = awakeKeeper;
            Button = button;

            RecreateLabel();
            button.MouseClick += OnButtonClick; 
        }

        private void OnButtonClick()
        {
            AwakeKeeper keeper = _awakeKeeper;
            keeper.IsActive = !keeper.IsActive;
            RecreateLabel();
        }
        private void RecreateLabel()
        {
            string newText = _awakeKeeper.IsActive ? ActiveString : InactiveString;

            Button.Label.CreateTexture(newText, Font, Color);
        }
    }
}