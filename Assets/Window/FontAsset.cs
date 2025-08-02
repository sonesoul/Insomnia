using Raylib_cs;

namespace Insomnia.Assets.Window
{
    public class FontAsset : Asset
    {
        public Font Font { get; private set; }

        public FontAsset(string relativePath) : base(relativePath)
        {
            Font = Load(relativePath);
        }

        public static Font Load(string relativePath)
        {
            return Raylib.LoadFont(GetAbsolutePath(relativePath));
        }
    }
}