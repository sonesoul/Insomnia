using Insomnia.Assets;
using Insomnia.DirectMedia;
using Insomnia.View.Elements;

namespace Insomnia.Tray.Buttons
{
    public class ToggleButton : TrayButton
    {
        public const string ActiveText = "Disable";
        public const string InactiveText = "Enable";
        
        private readonly AwakeKeeper _awakeKeeper;

        public ToggleButton(Window window, AwakeKeeper awakeKeeper) : base(string.Empty, window)
        {
            _awakeKeeper = awakeKeeper;
            SetLabelText();
        }

        protected override void OnClick()
        {
            base.OnClick();

            AwakeKeeper keeper = _awakeKeeper;
            keeper.IsActive = !keeper.IsActive;
            SetLabelText();
        }

        private void SetLabelText()
        {
            Button.Renderer.Label.Text = _awakeKeeper.IsActive ? ActiveText : InactiveText;
        }
    }
}