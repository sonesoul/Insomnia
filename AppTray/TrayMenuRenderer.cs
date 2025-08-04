using Insomnia.Assets;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Insomnia.AppTray
{
    public class TrayMenuRenderer : ToolStripProfessionalRenderer
    {
        public const int LeftPadding = 9;

        private readonly SolidBrush _brushStripBack = new(Palette.LightGray);
        private readonly SolidBrush _brushItemBack = new(Palette.Black);
        private readonly SolidBrush _brushText = new(Palette.Black);
        private readonly SolidBrush _brushTextSelected = new(Palette.White);

        private readonly StringFormat _stringFormat = new() 
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
        };

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ToolStripItem item = e.Item;

            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            Rectangle textRect = item.ContentRectangle;
            textRect.X = LeftPadding * TrayMenu.PixelScale;

            TextRenderer.DrawText(e.Graphics, item.Text, item.Font, textRect, item.Selected ? Palette.White : Palette.Black, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            
            return;
            
            //sometimes occures AccessViolationException, but looks better :(
            graphics.DrawString(
                    e.Text,
                    item.Font,
                    item.Selected ? _brushTextSelected : _brushText,
                    textRect,
                    _stringFormat);
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;

            if (!item.Selected)
            {
                return;
            }

            e.Graphics.FillRectangle(
                _brushItemBack, 
                new Rectangle(Point.Empty, item.Size));
        }
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e) 
        {
            e.Graphics.FillRectangle(
                _brushStripBack, 
                new Rectangle(Point.Empty, e.ToolStrip.Size));
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }
    }
}