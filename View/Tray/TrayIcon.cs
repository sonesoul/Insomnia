using Insomnia.Assets;
using System;
using System.Windows.Forms;

namespace Insomnia.View.Tray
{
    public class TrayIcon
    {
        public NotifyIcon NotifyIcon { get; }

        public TrayIcon(AwakeKeeper awakeKeeper)
        {
            NotifyIcon = new()
            {
                Visible = true,
                Icon = Icons.Active,
            };
            NotifyIcon.MouseClick += OnIconClick;
            NotifyIcon.MouseDoubleClick += OnIconDoubleClick;
            
            awakeKeeper.ActiveStateChanged += OnActiveStateChanged;
        }

        public void Hide() => NotifyIcon.Visible = false;

        private void OnIconDoubleClick(object obj, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                Program.MainWindow.ToggleVisibility();
            }
        }
        private void OnIconClick(object obj, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right)
            {
                Program.TrayWindow.Show();
            }
        }
        private void OnActiveStateChanged(bool isActive)
        {
            NotifyIcon.Icon = isActive ? Icons.Active : Icons.Inactive;
        }
    }
}