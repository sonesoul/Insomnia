using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insomnia.AppTray
{
    public class TrayIcon
    {
        private NotifyIcon _notifyIcon;
        private Window _window;

        public TrayIcon(Window window)
        {
            _window = window;
            _notifyIcon = new NotifyIcon() 
            {

            };
        }

        private void ShowWindow()
        {
            
        }
    }
}