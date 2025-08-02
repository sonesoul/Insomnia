using Raylib_cs;

namespace Insomnia.Assets.Window
{
    public static class Fonts
    {
        public static Font Pico8 { get; } 

        public const string Pico8FontPath = "pico-8-mono.ttf";

        static Fonts()
        {
            Pico8 = Raylib.LoadFontEx(Asset.GetAbsolutePath(Pico8FontPath), 10, null, 256);
            Raylib.SetTextureFilter(Pico8.Texture, TextureFilter.Point);
        }
    }
}