using System.Drawing;

namespace Insomnia.Assets.Tray
{
    public static class Icons
    {
        public static Icon Active { get; } = IconAsset.Load(ActivePath);
        public static Icon Inactive { get; } = IconAsset.Load(InactivePath);
        
        public const string ActivePath = "Icon_Active.ico";
        public const string InactivePath = "Icon_Inactive.ico";
    }
}