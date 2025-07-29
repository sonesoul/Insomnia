using Insomnia.Assets;
using Insomnia.AppTray.Buttons;
using System.Windows.Forms;

namespace Insomnia.AppTray
{
    public class Tray
    {
        public NotifyIcon Icon { get; set; } 
        public TrayMenu Menu { get; set; }

        private static Window AppWindow => Program.Window;
        private static AwakeKeeper AwakeKeeper => Program.AwakeKeeper;

        public Tray()
        {
            Menu = new TrayMenu(AppWindow);

            Icon = new()
            {
                Icon = Icons.Active,
                Visible = true,
                Text = Program.Name,
                ContextMenuStrip = Menu,
            };

            Icon.MouseDoubleClick += OnMouseDoubleClick;

            Menu.Items.AddRange(new TrayButton[] {
                new ShowHideButton(AppWindow),
                new KeeperStateButton(AwakeKeeper, Icon),
                new QuitButton(AppWindow)});
        }

        private static void OnMouseDoubleClick(object obj, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                AppWindow.Show();
            }
        }
    }
}
