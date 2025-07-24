global using Raylib_cs;
global using Point = System.Drawing.Point;

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Insomnia
{
    internal class Program
    {
        public static Point Size { get; } = new Point(400, 400);
        public static Point Resolution { get; } = new Point(128, 128);

        private static NotifyIcon trayIcon;
        private static Window window;

        private static bool ShouldQuit { get; set; } = false;

        [STAThread]
        private static void Main()
        {
            window = new(Size, Resolution);

            window.Open();
            
            var winFormsThread = new Thread(Application.Run);
            winFormsThread.SetApartmentState(ApartmentState.STA);
            winFormsThread.Start();
            
            trayIcon = new()
            {
                Icon = new Icon("D:\\IconActive.ico"),
                Visible = true,
                Text = "Insomnia",
            };

            trayIcon.MouseDoubleClick += (obj, args) =>
            {
                if (args.Button != MouseButtons.Left)
                    return;

                Raylib.ClearWindowState(ConfigFlags.HiddenWindow);
            };
            trayIcon.MouseClick += (obj, args) =>
            {
                if (args.Button != MouseButtons.Right)
                    return;
            };

            ContextMenuStrip contextMenu = new();
            
            ToolStripMenuItem quitItem = new("Quit");
            ToolStripLabel item = new("Insomnia");
            ToolStripSeparator sep = new();

            quitItem.BackColor = System.Drawing.Color.White;
            quitItem.ForeColor = new System.Drawing.Color();
            quitItem.Click += (obj, args) => ShouldQuit = true;

            contextMenu.Font = new System.Drawing.Font("PICO-8 mono", 6f);
            
            contextMenu.Items.Add(item);
            contextMenu.Items.Add(sep);
            contextMenu.Items.Add(quitItem);
            

            trayIcon.ContextMenuStrip = contextMenu;
            
            AwakeKeeper keeper = new();
            
            while (!ShouldQuit)
            {
                keeper.Update();

                if (window.IsOpened)
                {
                    window.RenderFrame();

                    if (Raylib.IsKeyDown(KeyboardKey.F1))
                    {
                        break;
                    }
                    else if (Raylib.WindowShouldClose())
                    {
                        Raylib.SetWindowState(ConfigFlags.HiddenWindow);
                    }
                }
            }

            trayIcon.Visible = false;
            Application.Exit();
            window.Close();
        }
    }
}