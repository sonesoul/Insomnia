using System.Drawing;

namespace Insomnia.Assets
{
    public class IconAsset : Asset
    {
        public Icon Icon { get; private set; }

        public IconAsset(string relativePath) : base(relativePath)
        {
            Icon = Load(relativePath);
        }

        public static Icon Load(string relativePath) => new(GetAbsolutePath(relativePath));
    }
}