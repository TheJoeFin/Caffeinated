using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Caffeinated;

// Base class for all forms. See Stackoverflow for reasons:
// http://stackoverflow.com/questions/297701/default-font-for-windows-forms-application/4076183#4076183
public class BaseForm : Form {
    protected bool IsDarkMode => Application.IsDarkModeEnabled;

    public BaseForm() {
        // Provides design-time support for the default font on Visa/Win7.
        // Falls back to something decent if system lacks the font.
        Font = SystemFonts.MessageBoxFont;
    }

    protected override void OnLoad(System.EventArgs e) {
        base.OnLoad(e);
        UpdateTheme();
    }

    protected override void OnSystemColorsChanged(System.EventArgs e) {
        base.OnSystemColorsChanged(e);
        UpdateTheme();
    }

    protected virtual void UpdateTheme() {
        // Override in derived classes to respond to theme changes
        BackColor = IsDarkMode ? Color.FromArgb(32, 32, 32) : SystemColors.Control;
        ForeColor = IsDarkMode ? Color.FromArgb(255, 255, 255) : SystemColors.ControlText;
    }

    private void InitializeComponent()
    {
        ComponentResourceManager resources = new(typeof(BaseForm));
        SuspendLayout();
        // 
        // BaseForm
        // 
        ClientSize = new Size(278, 244);
        Icon = resources.GetObject("$this.Icon") as Icon;
        Name = "BaseForm";
        ResumeLayout(false);
    }
}

