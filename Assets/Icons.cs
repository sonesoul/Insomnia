using System;
using System.Drawing;

namespace Insomnia.Assets
{
    public static class Icons
    {
        public static Icon Active => IconCreators[0]();
        public static Icon Inactive => IconCreators[1]();

        private static Func<Icon>[] IconCreators { get; } = 
        [
            () => Load("Icon_Active.ico"),
            () => Load("Icon_Inactive.ico"),
        ];

        private static Icon Load(string relativePath) => new Icon(Asset.GetAbsolutePath(relativePath));
    }
}