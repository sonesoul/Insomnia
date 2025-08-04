using DrawingColor = System.Drawing.Color;

namespace Insomnia.Assets
{
    public struct RGB
    {
        public byte Red;
        public byte Green;
        public byte Blue;

        public RGB(byte r, byte g, byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public static implicit operator DrawingColor(RGB rgb) => DrawingColor.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
    }
}
