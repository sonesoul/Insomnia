using System.Drawing;

namespace Insomnia.Assets
{
    public class IconAsset : Asset<Icon>
    {
        public IconAsset(string relativePath) : base(relativePath)
        {
            Data = new Icon(GetAbsolutePath(relativePath));
        }
    }
}