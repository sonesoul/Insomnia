using Raylib_cs;

namespace Insomnia.Assets.Window
{
    public static class Fonts
    {
        public static Font Pico8 { get; } = FontAsset.Load(Pico8FontPath);

        public const string Pico8FontPath = "pico-8-mono.ttf";
    }
}