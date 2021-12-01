using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Caffeinated;

// Base class for all forms. See stackvoerflow for reasons:
// http://stackoverflow.com/questions/297701/default-font-for-windows-forms-application/4076183#4076183
public class BaseForm : Form {
    public BaseForm() {
        // Provides design-time support for the default font on Visa/Win7.
        // Falls back to something decent if system lacks the font.
        Font = SystemFonts.MessageBoxFont;
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
