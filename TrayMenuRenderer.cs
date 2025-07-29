using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Insomnia
{
    public class TrayMenuRenderer : ToolStripProfessionalRenderer
    {
        public const int PixelWidth = 2;
        public const int PixelHeight = 2;
        
        public static Point PixelSize => new(PixelWidth, PixelHeight);

        public readonly static Color BackgroundColor = Color.FromArgb(194, 195, 199);

        public readonly static SolidBrush BackgroundBrush = new(BackgroundColor);
        public readonly static Pen BackgroundPen = new(BackgroundColor);

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Black, 2, 0, 108, 22);
            }
        }
        
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            using var sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            var textRect = e.Item.ContentRectangle;
            textRect.X = (9 * PixelSize.X);

            //bounds of the toolstrip
            textRect.X--;
            Brush brush = Brushes.Black;

            if (e.Item.Selected)
            {
                brush = new SolidBrush(Color.FromArgb(255, 241, 232));
            }

            g.DrawString(e.Text, e.TextFont, brush, textRect, sf);
        }
        
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) { }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }
    }
}