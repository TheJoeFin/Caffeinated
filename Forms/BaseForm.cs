using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;

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

    public void PositionNearTrayIcon(NotifyIcon? notifyIcon) {
        if (notifyIcon == null)
            return;

        Rectangle iconRect = GetNotifyIconRect(notifyIcon);
        
        if (iconRect.IsEmpty) {
            // Fallback to taskbar positioning
            PositionNearTaskbar();
            return;
        }

        PositionFormNearRect(iconRect);
    }

    private Rectangle GetNotifyIconRect(NotifyIcon notifyIcon) {
        try {
            FieldInfo? windowField = typeof(NotifyIcon).GetField(
                "window",
                BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (windowField?.GetValue(notifyIcon) is NativeWindow window) {
                NativeMethods.NOTIFYICONIDENTIFIER identifier = new() {
                    cbSize = System.Runtime.InteropServices.Marshal.SizeOf<NativeMethods.NOTIFYICONIDENTIFIER>(),
                    hWnd = window.Handle,
                    uID = (uint)notifyIcon.GetHashCode()
                };

                int result = NativeMethods.Shell_NotifyIconGetRect(
                    ref identifier,
                    out NativeMethods.RECT rect);

                if (result == 0) {
                    return rect.ToRectangle();
                }
            }
        }
        catch {
            // Fall through to return empty rectangle
        }

        return Rectangle.Empty;
    }

    private void PositionNearTaskbar() {
        Taskbar taskbar = new();
        Rectangle taskbarBounds = taskbar.Bounds;
        
        // Position based on taskbar location
        PositionFormNearRect(taskbarBounds);
    }

    private void PositionFormNearRect(Rectangle targetRect) {
        Taskbar taskbar = new();
        Screen screen = Screen.FromRectangle(targetRect);
        Rectangle workingArea = screen.WorkingArea;

        // Form dimensions are already DPI-scaled by WinForms
        int formWidth = Width;
        int formHeight = Height;

        int x, y;
        const int margin = 5;

        switch (taskbar.Position) {
            case TaskbarPosition.Bottom:
                x = targetRect.Right - formWidth;
                y = targetRect.Top - formHeight - margin;
                break;
            case TaskbarPosition.Top:
                x = targetRect.Right - formWidth;
                y = targetRect.Bottom + margin;
                break;
            case TaskbarPosition.Left:
                x = targetRect.Right + margin;
                y = targetRect.Bottom - formHeight;
                break;
            case TaskbarPosition.Right:
                x = targetRect.Left - formWidth - margin;
                y = targetRect.Bottom - formHeight;
                break;
            default:
                // Fallback to bottom-right
                x = workingArea.Right - formWidth - margin;
                y = workingArea.Bottom - formHeight - margin;
                break;
        }

        // Ensure form stays within screen bounds
        x = System.Math.Max(workingArea.Left, System.Math.Min(x, workingArea.Right - formWidth));
        y = System.Math.Max(workingArea.Top, System.Math.Min(y, workingArea.Bottom - formHeight));

        StartPosition = FormStartPosition.Manual;
        Location = new Point(x, y);
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

