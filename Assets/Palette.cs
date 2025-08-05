namespace Insomnia.Assets
{
    public class Palette
    {
        public static RGBA Black => Colors[0];
        public static RGBA DarkBlue => Colors[1];
        public static RGBA DarkPurple => Colors[2];
        public static RGBA DarkGreen => Colors[3];
        public static RGBA Brown => Colors[4];
        public static RGBA DarkGray => Colors[5];
        public static RGBA LightGray => Colors[6];
        public static RGBA White => Colors[7];
        public static RGBA Red => Colors[8];
        public static RGBA Orange => Colors[9];
        public static RGBA Yellow => Colors[10];
        public static RGBA Green => Colors[11];
        public static RGBA Blue => Colors[12];
        public static RGBA Indigo => Colors[13];
        public static RGBA Pink => Colors[14];
        public static RGBA Peach => Colors[15];

        private static readonly RGBA[] Colors = new RGBA[]
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