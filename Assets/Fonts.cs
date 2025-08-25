using Insomnia.DirectMedia.Types;
using System;

namespace Insomnia.Assets
{
    public static class Fonts
    {
        public static Font Pico8 => FontCreators[0]();

        private static Func<Font>[] FontCreators { get; } = 
        [
            () => Load("Pico-8.ttf", 10f),
        ];

        private static Font Load(string relativePath, float size) => new(Asset.GetAbsolutePath(relativePath), size);
    }
}
