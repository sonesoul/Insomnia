using System;

namespace Insomnia.DirectMedia.Types
{
    public class Font : Resource
    {
        public int Height => SDL3.TTF.GetFontHeight(this);

        public Font(string path, float size)
        {
            Pointer = SDL3.TTF.OpenFont(path, size);
        }

        protected override void Destroy()
        {
            SDL3.TTF.CloseFont(this);
        }
    }
}