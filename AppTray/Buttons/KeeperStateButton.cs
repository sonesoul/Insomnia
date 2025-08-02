using Insomnia.Assets.Tray;
using System;
using System.Windows.Forms;

namespace Insomnia.AppTray.Buttons
{
    public class KeeperStateButton : TrayButton
    {
        public AwakeKeeper AwakeKeeper { get; }
        public NotifyIcon TrayIcon { get; }

        public const string EnableText = "Enable";
        public const string DisableText = "Disable";

        public KeeperStateButton(AwakeKeeper keeper, NotifyIcon trayIcon) : base(GetStatusString(keeper.Enabled))
        {
            AwakeKeeper = keeper;
            TrayIcon = trayIcon;

            SetTrayIcon(keeper.Enabled);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            bool enabled = AwakeKeeper.Enabled = !AwakeKeeper.Enabled;
            base.Text = GetStatusString(AwakeKeeper.Enabled);

            SetTrayIcon(enabled);
        }

        private void SetTrayIcon(bool enabled) => TrayIcon.Icon = enabled ? Icons.Active : Icons.Inactive;
        private static string GetStatusString(bool enabled) => enabled ? DisableText : EnableText;
    }
}