using System;
using System.Numerics;

namespace Insomnia
{
    public class Window(Point size, Point resolution)
    {
        public bool IsOpened { get; private set; } = false;

        public event Action<float> Draw;

        private Rectangle _source;
        private Rectangle _destination;
        private RenderTexture2D _renderTarget;

        private Point size = size;
        private Point resolution = resolution;

        public void Open()
        {
            int rX = resolution.X;
            int rY = resolution.Y;
            int sX = size.X;
            int sY = size.Y;

            Raylib.InitWindow(sX, sY, nameof(Insomnia));
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

            Raylib.DrawTexturePro(_renderTarget.Texture, _source, _destination, new Vector2(0, 0), 0f, Color.White);
            Raylib.EndDrawing();
        }
    }
}
