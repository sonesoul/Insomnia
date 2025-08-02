using System;
using System.Numerics;
using Raylib_cs;

namespace Insomnia.AppWindow
{
    public class Window
    {
        public static Point Resolution { get; } = new Point(128, 128);
        public static Point Size { get; } = new Point(Resolution.X * 3, Resolution.Y * 3);

        public bool IsOpened { get; private set; } = false;
        public bool IsVisible { get; private set; } = true;

        public event Action<float> Draw;
        public event Action DrawEnd;

        private Rectangle _source;
        private Rectangle _destination;
        private RenderTexture2D _renderTarget;

        private Point size;
        private Point resolution;

        public Window()
        {
            size = Size;
            resolution = Resolution;
        }

        public void Open()
        {
            int rX = resolution.X;
            int rY = resolution.Y;
            int sX = size.X;
            int sY = size.Y;

            Raylib.InitWindow(sX, sY, Program.Name);
            Raylib.SetExitKey(KeyboardKey.F1);

            _renderTarget = Raylib.LoadRenderTexture(rX, rY);
            _source = new(0, 0, rX, -rY);
            _destination = new(0, 0, sX, sY);

            IsOpened = true;
        }
        public void Close()
        {
            Raylib.UnloadRenderTexture(_renderTarget);
            Raylib.CloseWindow();
            IsOpened = false;
        }

        public void RenderFrame()
        {
            BeginDrawing();
            Draw?.Invoke(Raylib.GetFrameTime());
            EndDrawing();
            DrawEnd?.Invoke();
        }

        private void BeginDrawing()
        {
            Raylib.BeginTextureMode(_renderTarget);
            Raylib.ClearBackground(Color.Black);
        }
        private void EndDrawing()
        {
            Raylib.EndTextureMode();
            Raylib.BeginDrawing();
            Raylib.DrawTextureV(_renderTarget.Texture, Vector2.Zero, Color.White);
            Raylib.DrawTexturePro(_renderTarget.Texture, _source, _destination, new Vector2(0, 0), 0f, Color.White);
            Raylib.EndDrawing();
        }        
        
        public void Show()
        {
            if (!IsOpened)
                return;

            Raylib.ClearWindowState(ConfigFlags.HiddenWindow);
            IsVisible = true;
        }
        public void Hide()
        {
            if (!IsOpened)
                return;

            Raylib.SetWindowState(ConfigFlags.HiddenWindow);
            IsVisible = false;
        }
    }
}