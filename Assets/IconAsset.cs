using System.Drawing;

namespace Insomnia.Assets
{
    public class IconAsset : Asset<Icon>
    {
        public IconAsset(string relativePath) : base(relativePath)
        {
            Data = Load(relativePath);
        }

        public static Icon Load(string relativePath) => new(GetAbsolutePath(relativePath));
    }
}