using System.Drawing;
using System.Windows.Forms;

namespace Caffeinated;

public class ModernColorTable : ProfessionalColorTable {
    private readonly bool isLightTheme;

    public ModernColorTable(bool isLightTheme) {
        this.isLightTheme = isLightTheme;
    }

    public override Color MenuItemSelected => isLightTheme
        ? Color.FromArgb(240, 240, 240)
        : Color.FromArgb(50, 50, 50);

    public override Color MenuItemSelectedGradientBegin => MenuItemSelected;

    public override Color MenuItemSelectedGradientEnd => MenuItemSelected;

    public override Color MenuBorder => isLightTheme
        ? Color.FromArgb(204, 206, 219)
        : Color.FromArgb(60, 60, 60);

    public override Color MenuItemBorder => isLightTheme
        ? Color.FromArgb(0, 120, 215)
        : Color.FromArgb(0, 120, 215);

    public override Color ImageMarginGradientBegin => isLightTheme
        ? Color.FromArgb(250, 250, 250)
        : Color.FromArgb(30, 30, 30);

    public override Color ImageMarginGradientMiddle => ImageMarginGradientBegin;

    public override Color ImageMarginGradientEnd => ImageMarginGradientBegin;

    public override Color ToolStripDropDownBackground => isLightTheme
        ? Color.FromArgb(255, 255, 255)
        : Color.FromArgb(40, 40, 40);
}