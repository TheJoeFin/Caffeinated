using System.Drawing;
using System.Windows.Forms;

namespace Caffeinated;

public class ModernMenuRenderer : ToolStripProfessionalRenderer {
    private readonly bool isLightTheme;

    public ModernMenuRenderer(bool isLightTheme) : base(new ModernColorTable(isLightTheme)) {
        this.isLightTheme = isLightTheme;
        RoundedEdges = false;
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
        if (e.Item.Selected) {
            Rectangle rect = new(Point.Empty, e.Item.Size);
            Color hoverColor = isLightTheme
                ? Color.FromArgb(240, 240, 240)
                : Color.FromArgb(50, 50, 50);

            using SolidBrush brush = new(hoverColor);
            e.Graphics.FillRectangle(brush, rect);

            Color borderColor = isLightTheme
                ? Color.FromArgb(0, 120, 215)
                : Color.FromArgb(0, 120, 215);

            using Pen pen = new(borderColor, 1);
            Rectangle borderRect = rect;
            borderRect.Width -= 1;
            borderRect.Height -= 1;
            e.Graphics.DrawRectangle(pen, borderRect);
        }
        else {
            base.OnRenderMenuItemBackground(e);
        }
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
        if (e.Item is ToolStripSeparator) {
            base.OnRenderItemText(e);
            return;
        }

        e.TextColor = isLightTheme
            ? Color.FromArgb(32, 32, 32)
            : Color.FromArgb(240, 240, 240);

        e.TextFont = new Font(e.TextFont.FontFamily, e.TextFont.Size, FontStyle.Regular);

        Rectangle adjustedRect = e.TextRectangle;
        adjustedRect.Y += 3;
        e.TextRectangle = adjustedRect;

        base.OnRenderItemText(e);
    }

    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
        Rectangle rect = new(0, e.Item.Height / 2, e.Item.Width, 1);

        Color separatorColor = isLightTheme
            ? Color.FromArgb(220, 220, 220)
            : Color.FromArgb(60, 60, 60);

        using Pen pen = new(separatorColor);
        e.Graphics.DrawLine(pen, rect.Left + 30, rect.Top, rect.Right - 5, rect.Top);
    }
}
