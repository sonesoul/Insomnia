using Insomnia.Assets.Window;
using Raylib_cs;
using System.Numerics;

namespace Insomnia.AppWindow.Elements
{
    public class Label : IUIElement
    {
        public string Text { get; set; } = "label";

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        
        public Color Color { get; set; } = Color.White;

        public float RotationRad { get; set; } = 0;
        public float Scale { get; set; } = 5f;
        public float Spacing { get; set; } = 0;

        public void Draw(float dt)
        {
            Raylib.DrawTextPro(
                Fonts.Pico8,
                Text, 
                Position, 
                Origin, 
                RotationRad, 
                Scale, 
                Spacing, 
                Color);
        }
    }
}