namespace Insomnia.DirectMedia.Types
{
    public class Font : Resource
    {
        public Font(string path, float size)
        {
            Pointer = SDL3.TTF.OpenFont(path, size);
        }

        protected override void Destroy()
        {
            SDL3.TTF.CloseFont(Pointer);
        }

    }
}