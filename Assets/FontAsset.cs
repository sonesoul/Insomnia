using System.Drawing.Text;
using Font = System.Drawing.Font;

namespace Insomnia.Assets
{
    public class FontAsset : Asset<Font>
    {
        public FontAsset(string relativePath, float size) : base(relativePath)
        {
            PrivateFontCollection pfc = new();
            string absolute = GetAbsolutePath(relativePath);
            pfc.AddFontFile(absolute);
            Data = new(pfc.Families[0], size);
        }
    }
}