namespace Insomnia.Assets
{
    public struct RGB
    {
        public byte R;
        public byte G;
        public byte B;

        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public readonly Raylib_cs.Color ToRaylib() => new Raylib_cs.Color(R, G, B);
        public readonly System.Drawing.Color ToDrawing() => System.Drawing.Color.FromArgb(R, G, B);

        public static implicit operator Raylib_cs.Color(RGB rgb) => rgb.ToRaylib();
        public static implicit operator System.Drawing.Color(RGB rgb) => rgb.ToDrawing();
    }

    public class Palette
    {
        public static RGB Black => Colors[0];
        public static RGB DarkBlue => Colors[1];
        public static RGB DarkPurple => Colors[2];
        public static RGB DarkGreen => Colors[3];
        public static RGB Brown => Colors[4];
        public static RGB DarkGray => Colors[5];
        public static RGB LightGray => Colors[6];
        public static RGB White => Colors[7];
        public static RGB Red => Colors[8];
        public static RGB Orange => Colors[9];
        public static RGB Yellow => Colors[10];
        public static RGB Green => Colors[11];
        public static RGB Blue => Colors[12];
        public static RGB Indigo => Colors[13];
        public static RGB Pink => Colors[14];
        public static RGB Peach => Colors[15];

        private static readonly RGB[] Colors = new RGB[]
        {
            new(0, 0, 0),         // Black
            new(29, 43, 83),      // Dark Blue
            new(126, 37, 83),     // Dark Purple
            new(0, 135, 81),      // Dark Green
            new(171, 82, 54),     // Brown
            new(95, 87, 79),      // Dark Gray
            new(194, 195, 199),   // Light Gray
            new(255, 241, 232),   // Soft White (Pico-8)
            new(255, 0, 77),      // Red
            new(255, 163, 0),     // Orange
            new(255, 236, 39),    // Yellow
            new(0, 228, 54),      // Green
            new(41, 173, 255),    // Blue
            new(131, 118, 156),   // Indigo
            new(255, 119, 168),   // Pink
            new(255, 204, 170),   // Peach
        };
    }
}