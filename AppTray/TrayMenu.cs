using Insomnia.Assets.Tray;
using Insomnia.DirectMedia;
using System.Drawing;
using System.Windows.Forms;

namespace Insomnia.AppTray
{
    public class TrayMenu : ContextMenuStrip
    {
        public const string FontPath = "pico-8-mono.ttf";

        public const float OneToOneFontSize = 3f; //size when one pixel in the font equals one pixel on a screen
        public const int PixelScale = 2;

        public const float FontSize = OneToOneFontSize * PixelScale;
        
        public const int MenuWidth = 64 * PixelScale;
        public const int MenuHeight = 35 * PixelScale;

        private readonly Window _appWindow;

        public TrayMenu(Window appWindow)
        {
            _appWindow = appWindow;

            Font = FontAsset.Load(FontPath, FontSize);
            Renderer = new TrayMenuRenderer();
            AutoSize = false;
            Size = new Size(MenuWidth, MenuHeight);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                _appWindow.Show();
            }
        }
    }
}