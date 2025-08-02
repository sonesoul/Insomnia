using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Insomnia.AppTray
{
    public class TrayMenuRenderer : ToolStripProfessionalRenderer
    {
        public static Color PicoGray { get; } = Color.FromArgb(194, 195, 199);
        public static Color PicoWhite { get; } = Color.FromArgb(255, 241, 232);
        public static Color PicoBlack { get; } = Color.FromArgb(0, 0, 0);
   
        public static Color StripBackground => PicoGray;
        public static Color ItemBackground => PicoBlack;
        public static Color Text => PicoBlack;
        public static Color TextSelected => PicoWhite;

        public const int LeftPadding = 9;

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ToolStripItem item = e.Item;

            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            
            using var sf = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            Rectangle textRect = item.ContentRectangle;
            textRect.X = LeftPadding * TrayMenu.PixelScale;

            using var brush = new SolidBrush(item.Selected ? TextSelected : Text);

            graphics.DrawString(
                e.Text, 
                e.TextFont, 
                brush, 
                textRect, 
                sf);
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;

            if (item.Selected)
            {
                using var brush = new SolidBrush(ItemBackground);

                e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, item.Size));
            }
        }
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) 
        {
            using var brush = new SolidBrush(StripBackground);

            e.Graphics.FillRectangle(
                brush, 
                new Rectangle(Point.Empty, e.ToolStrip.Size));
        }


        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }
    }
}