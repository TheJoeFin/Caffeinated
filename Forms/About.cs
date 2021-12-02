using System.Reflection;
using System.Windows.Forms;
using System.IO;
using Windows.System;
using System;

namespace Caffeinated;

partial class AboutForm : BaseForm {
    public AboutForm() : base() {
        InitializeComponent();
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
