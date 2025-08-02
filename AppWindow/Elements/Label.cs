using Insomnia.Assets;
using Insomnia.Assets.Window;
using Raylib_cs;
using System.Numerics;

namespace Insomnia.AppWindow.Elements
{
    public class Label : IUIElement
    {
        public string Text { get; set; } = "Label";

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        
        public Color Color { get; set; } = Palette.White;

        public float RotationRad { get; set; } = 0;
        public float Scale { get; set; } = 1f;
        
        public void Draw(float dt)
        {
            Raylib.DrawTextPro(
                Fonts.Pico8,
                Text, 
                Position, 
                Origin, 
                RotationRad, 
                5 * Scale, 
                0, 
                Color);
        }
    }
}