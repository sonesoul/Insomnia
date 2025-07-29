using System;

namespace Insomnia.AppTray.Buttons
{
    public class QuitButton : TrayButton
    {
        public Window Window { get; }

        public const string ButtonText = "Quit";

        public QuitButton(Window window) : base(ButtonText)
        {
            Window = window;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (Window.IsOpened)
            {
                void CloseWindow()
                {
                    Window.Close();
                    Window.DrawEnd -= CloseWindow;
                }

                Window.DrawEnd -= CloseWindow; //remove previous handler on multiple invocations
                Window.DrawEnd += CloseWindow;
            }
        }
    }
}