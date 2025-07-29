using System.Drawing.Text;
using Font = System.Drawing.Font;

namespace Insomnia.Assets
{
    public class FontAsset : Asset<Font>
    {
        public FontAsset(string relativePath, float size) : base(relativePath)
        {
            Data = Load(relativePath, size);
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