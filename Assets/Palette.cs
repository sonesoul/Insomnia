using System.Drawing;

namespace Insomnia.Assets
{
    public class Palette
    {
        public static Color Black => Colors[0];
        public static Color DarkBlue => Colors[1];
        public static Color DarkPurple => Colors[2];
        public static Color DarkGreen => Colors[3];
        public static Color Brown => Colors[4];
        public static Color DarkGray => Colors[5];
        public static Color LightGray => Colors[6];
        public static Color White => Colors[7];
        public static Color Red => Colors[8];
        public static Color Orange => Colors[9];
        public static Color Yellow => Colors[10];
        public static Color Green => Colors[11];
        public static Color Blue => Colors[12];
        public static Color Indigo => Colors[13];
        public static Color Pink => Colors[14];
        public static Color Peach => Colors[15];

        private static readonly Color[] Colors = new[]
        {
            Color.FromArgb(0, 0, 0),         // Black
            Color.FromArgb(29, 43, 83),      // Dark Blue
            Color.FromArgb(126, 37, 83),     // Dark Purple
            Color.FromArgb(0, 135, 81),      // Dark Green
            Color.FromArgb(171, 82, 54),     // Brown
            Color.FromArgb(95, 87, 79),      // Dark Gray
            Color.FromArgb(194, 195, 199),   // Light Gray
            Color.FromArgb(255, 241, 232),   // Soft White (Pico-8)
            Color.FromArgb(255, 0, 77),      // Red
            Color.FromArgb(255, 163, 0),     // Orange
            Color.FromArgb(255, 236, 39),    // Yellow
            Color.FromArgb(0, 228, 54),      // Green
            Color.FromArgb(41, 173, 255),    // Blue
            Color.FromArgb(131, 118, 156),   // Indigo
            Color.FromArgb(255, 119, 168),   // Pink
            Color.FromArgb(255, 204, 170),   // Peach
        };
    }
}