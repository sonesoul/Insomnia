using SDL3;

namespace Insomnia.DirectMedia.Types
{
    public class Font : Resource
    {
        public Point CharSize { get; }

        public const string CharMeasurer = "0";

        public Font(string path, float size)
        {
            Pointer = TTF.OpenFont(path, size);
            CharSize = MeasureString(CharMeasurer);
        }

        protected override void Destroy()
        {
            TTF.CloseFont(this);
        }

        public Point MeasureString(string text)
        {
            TTF.GetStringSize(this, text, 0, out int x, out int y);
            return new Structures.Point(x, y) / 2; //idk why results are twice bigger, and dividing by two makes it pixel-perfect
        }
    }
}