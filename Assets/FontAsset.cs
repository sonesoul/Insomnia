using System.Drawing.Text;
using Font = System.Drawing.Font;

namespace Insomnia.Assets
{
    public class FontAsset : Asset
    {
        public Font Font { get; private set; }

        public FontAsset(string relativePath, float size) : base(relativePath)
        {
            Font = Load(relativePath, size);
        }

        public static Font Load(string relativePath, float size)
        {
            PrivateFontCollection pfc = new();
            string absolute = GetAbsolutePath(relativePath);
            pfc.AddFontFile(absolute);
            return new(pfc.Families[0], size);
        }
    }
}