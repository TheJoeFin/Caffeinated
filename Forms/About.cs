using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Windows.System;
using System;
using Caffeinated.Properties;

namespace Caffeinated;

partial class AboutForm : BaseForm {
    public AboutForm() : base() {
        InitializeComponent();
    }

    protected override void UpdateTheme() {
        base.UpdateTheme();

        // Update all labels to use proper foreground color
        VersionTxtBlk.ForeColor = ForeColor;
        label3.ForeColor = ForeColor;
        label1.ForeColor = ForeColor;

        // Update LinkLabel colors for better visibility in dark mode
        Color linkColor;
        Color visitedLinkColor;
        Color activeLinkColor;

        if (IsDarkMode) {
            // Light colors for dark background
            linkColor = Color.FromArgb(100, 181, 246);        // Light blue
            visitedLinkColor = Color.FromArgb(186, 104, 200); // Light purple
            activeLinkColor = Color.FromArgb(144, 202, 249);  // Lighter blue
        } else {
            // Standard colors for light background
            linkColor = Color.FromArgb(0, 0, 255);            // Blue
            visitedLinkColor = Color.FromArgb(128, 0, 128);   // Purple
            activeLinkColor = Color.FromArgb(255, 0, 0);      // Red
        }

        // Apply to all LinkLabels
        dmndLbl.LinkColor = linkColor;
        dmndLbl.VisitedLinkColor = visitedLinkColor;
        dmndLbl.ActiveLinkColor = activeLinkColor;

        caffeineLbl.LinkColor = linkColor;
        caffeineLbl.VisitedLinkColor = visitedLinkColor;
        caffeineLbl.ActiveLinkColor = activeLinkColor;

        OriginallyByLink.LinkColor = linkColor;
        OriginallyByLink.VisitedLinkColor = visitedLinkColor;
        OriginallyByLink.ActiveLinkColor = activeLinkColor;

        ForkedByTheJoeFinLink.LinkColor = linkColor;
        ForkedByTheJoeFinLink.VisitedLinkColor = visitedLinkColor;
        ForkedByTheJoeFinLink.ActiveLinkColor = activeLinkColor;

        RateLabel.LinkColor = linkColor;
        RateLabel.VisitedLinkColor = visitedLinkColor;
        RateLabel.ActiveLinkColor = activeLinkColor;

        // Update icon based on theme
        pictureBox1.Image = IsDarkMode ? Resources.cup_coffee_icon_96 : Resources.Caffeine_Black_96;
        pictureBox1.BackColor = BackColor;
    }

    #region Assembly Attribute Accessors

    public static string AssemblyTitle {
        get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0) {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "") {
                    return titleAttribute.Title;
                }
            }
            string? systemString = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);

            if (systemString == null)
                return "";
            else
                return systemString;
        }
    }

    public string AssemblyVersion {
        get {
            return Application.ProductVersion.ToString();
        }
    }

    public string AssemblyDescription {
        get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0) {
                return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    public string AssemblyProduct {
        get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0) {
                return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    public string AssemblyCopyright {
        get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0) {
                return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    public string AssemblyCompany {
        get {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0) {
                return "";
            }
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }
    #endregion

    private async void linkLbl_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e) {
        _ = await Launcher.LaunchUriAsync(new Uri(string.Format("http://lightheadsw.com/caffeine/")));
    }

    private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
        _ = await Launcher.LaunchUriAsync(new Uri(string.Format("http://desmondbrand.com/caffeinated")));
    }

    private async void ForkedByTheJoeFinLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
        _ = await Launcher.LaunchUriAsync(new Uri(string.Format("https://github.com/TheJoeFin/Caffeinated")));
    }

    private async void RateLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
        _ = await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", "40087JoeFinApps.WindowsCaffeinated_kdbpvth5scec4")));
    }
}
