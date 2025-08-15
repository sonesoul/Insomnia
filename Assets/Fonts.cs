using Insomnia.DirectMedia.Types;
using System;

namespace Insomnia.Assets
{
    public static class Fonts
    {
        public static Font Pico8Mono => FontCreators[0]();

        private static Func<Font>[] FontCreators { get; } = 
        [
            () => Load("pico-8-mono.ttf", 8),
        ];

        private static Font Load(string relativePath, float size) => new(Asset.GetAbsolutePath(relativePath), size);
    }
}
