using System.Windows.Forms;

namespace Insomnia.AppTray.Buttons
{
    public abstract class TrayButton : ToolStripMenuItem
    {
        public TrayButton(string text) : base(text)
        {
            CheckOnClick = false;
            AutoSize = false;
            
            Size = new(
                TrayMenu.MenuWidth - TrayMenu.PixelScale * 2,
                TrayMenu.MenuHeight / 3 - 1);

            MouseEnter += (obj, args) => SetCursorHand();
            MouseLeave += (obj, args) => SetCursorDefault();

            Margin = new(TrayMenu.PixelScale, 0, 0, 0);
        }

        private void SetCursorHand() => Owner.Cursor = Cursors.Hand;
        private void SetCursorDefault() => Owner.Cursor = Cursors.Default;
    }
}