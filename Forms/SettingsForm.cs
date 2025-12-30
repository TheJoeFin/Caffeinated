using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Caffeinated.Helpers;
using Caffeinated.Properties;
using Windows.ApplicationModel;

namespace Caffeinated;

public partial class SettingsForm : BaseForm {
    readonly BindingList<Duration> Durations;
    readonly AppSettings appSettings;

    public SettingsForm(AppSettings passedAppSettings) : base() {
        InitializeComponent();
        appSettings = passedAppSettings;
        var durations = from i in appSettings.Durations
                        select new Duration { Minutes = i };
        this.Durations = new BindingList<Duration>(durations.ToList());
        var defaultItem = this.Durations.Where(
            d => d.Minutes == appSettings.DefaultDuration
        ).FirstOrDefault();

        if (appSettings.ShowMessageOnLaunch)
            SettingsAtLaunchChkBox.Checked = true;

        if (appSettings.ActivateOnLaunch)
            ActivateChkBox.Checked = true;

        DefaultDurationBox.DataSource = this.Durations;
        DefaultDurationBox.DisplayMember = "Description";
        DefaultDurationBox.ValueMember = "Minutes";
        DefaultDurationBox.SelectedItem = defaultItem;

        ToolStripMenuItem deleteMI = new("Delete Duration");
        deleteMI.Click += DeleteMI_Click;
        ContextMenuStrip durationCM = new();
        durationCM.Items.Add(deleteMI);
        DefaultDurationBox.ContextMenuStrip = durationCM;

        setStartupCheckBox();
        setRadioButtons();
    }

    private static Bitmap IconToBitmap(Icon icon, int size) {
        Bitmap bitmap = new(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(bitmap)) {
            g.Clear(Color.Transparent);
            g.DrawIcon(icon, new Rectangle(0, 0, size, size));
        }
        return bitmap;
    }

    protected override void UpdateTheme() {
        base.UpdateTheme();
        
        // Update all labels to use proper foreground color
        label1.ForeColor = ForeColor;
        label2.ForeColor = ForeColor;
        label3.ForeColor = ForeColor;
        OffIconLbl.ForeColor = ForeColor;
        OnIconLbl.ForeColor = ForeColor;
        CustomDurationLBL.ForeColor = ForeColor;
        tooltipFormatLBL.ForeColor = ForeColor;
        
        // Update main icon based on theme - use light colored icons for dark mode
        pictureBox1.Image = IsDarkMode ? Resources.cup_coffee_icon_96 : Resources.Caffeine_Black_96;
        
        // Update default icon set (row 1)
        // In dark mode, use light-colored icons; in light mode, use black icons
        if (IsDarkMode) {
            pictureBox2.Image = IconToBitmap(Resources.cup_coffee_icon_bw, 80);
            pictureBox3.Image = Resources.cup_coffee_icon_64;
        } else {
            pictureBox2.Image = Resources.SleepEye_Black;
            pictureBox3.Image = Resources.Caffeine_Black_512;
        }
        
        // Update eye with zzz icon set (row 2)
        if (IsDarkMode) {
            pictureBox4.Image = IconToBitmap(Resources.Eye_zzz_Sleep_icon, 80);
            pictureBox5.Image = IconToBitmap(Resources.Eye_zzz_Active_icon, 80);
        } else {
            pictureBox4.Image = Resources.Eye_zzz_Sleep_Black;
            pictureBox5.Image = Resources.Eye_zzz_Active_Black;
        }
        
        // Update mug icon set (row 3)
        if (IsDarkMode) {
            pictureBox6.Image = IconToBitmap(Resources.mug_sleep_icon, 80);
            pictureBox7.Image = IconToBitmap(Resources.mug_active_icon, 80);
        } else {
            pictureBox6.Image = Resources.Mug_Sleep_Black;
            pictureBox7.Image = Resources.Mug_Active_Black;
        }
        
        // Update TableLayoutPanels background
        tableLayoutPanel1.BackColor = BackColor;
        tableLayoutPanel2.BackColor = BackColor;
        tableLayoutPanel3.BackColor = BackColor;
        tableLayoutPanel4.BackColor = BackColor;
        
        // Update picture boxes background
        pictureBox1.BackColor = BackColor;
        
        // Force refresh
        Refresh();
    }

    private void setRadioButtons() {
        switch (appSettings.Icon) {
            case TrayIcon.Mug:
                mugRDBTN.Checked = true;
                break;
            case TrayIcon.EyeWithZzz:
                eyeZZZRDBTN.Checked = true;
                break;
            default:
                defaultRDBTN.Checked = true;
                break;
        }

        switch (appSettings.TooltipFormat) {
            case TooltipFormat.Specific:
                specificTooltipRDBTN.Checked = true;
                break;
            default:
                generalTooltipRDBTN.Checked = true;
                break;
        }
    }

    private void DeleteMI_Click(object? sender, EventArgs e) {
        if (DefaultDurationBox.SelectedItem is not Duration durationToDelete)
            return;

        DialogResult result = MessageBox.Show(
                $"Delete {durationToDelete.Description}?",
                "Caffeinated",
                MessageBoxButtons.YesNo
            );

        switch (result) {
            case DialogResult.None:
                break;
            case DialogResult.OK:
                break;
            case DialogResult.Cancel:
                break;
            case DialogResult.Abort:
                break;
            case DialogResult.Retry:
                break;
            case DialogResult.Ignore:
                break;
            case DialogResult.Yes:
                Durations.Remove(durationToDelete);
                appSettings.Durations.Remove(durationToDelete.Minutes);
                // appSettings.Durations = appSettings.Durations;
                break;
            case DialogResult.No:
                break;
            default:
                break;
        }
    }

    private async void setStartupCheckBox() {
        try {
            StartupTask startupTask = await StartupTask.GetAsync("StartCaffeinated");
            Debug.WriteLine("Startup is " + startupTask.State.ToString());

            switch (startupTask.State) {
                case StartupTaskState.Disabled:
                    // Task is disabled but can be enabled.
                    StartupChkBox.Checked = false;
                    break;
                case StartupTaskState.DisabledByUser:
                    // Task is disabled and user must enable it manually.
                    StartupChkBox.Checked = false;
                    StartupChkBox.Enabled = false;

                    StartupChkBox.Text += "\nDisabled in Task Manager";
                    break;
                case StartupTaskState.Enabled:
                    StartupChkBox.Checked = true;
                    break;
            }
        }
        catch (Exception ex) {
            Debug.WriteLine($"Error checking startup state: {ex.Message}");
            // Disable checkbox if we can't determine state
            StartupChkBox.Enabled = false;
        }
    }

    private void okBtn_Click(object sender, EventArgs e) {
        this.Close();
    }

    private void cancelBtn_Click(object sender, EventArgs e) {
        this.Close();
    }

    private void DefaultDurationBox_SelectedIndexChanged(object sender,EventArgs e) {
        if (DefaultDurationBox.SelectedItem is Duration item) {
            appSettings.DefaultDuration = item.Minutes;
        }
    }

    private async void StartupChkBox_CheckedChanged(object sender, EventArgs e) {
        try {
            StartupTask startupTask = await StartupTask.GetAsync("StartCaffeinated");

            switch (StartupChkBox.Checked) {
                case true:
                    StartupTaskState newState = await startupTask.RequestEnableAsync();
                    Debug.WriteLine("Request to enable startup, result = {0}", newState);
                    break;
                case false:
                    startupTask.Disable();
                    Debug.WriteLine("Disabled startup task");
                    break;
            }
        }
        catch (Exception ex) {
            Debug.WriteLine($"Error changing startup state: {ex.Message}");
            MessageBox.Show(
                $"Could not change startup setting: {ex.Message}",
                "Caffeinated",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }

    private void addCustomDurationBTN_Click(object sender, EventArgs e) {
        bool didParse = int.TryParse(CustomDurationTXBX.Text, out int newDuration);

        if (didParse == false)
            return;

        if ( newDuration < 0) {
            CustomDurationTXBX.Text = "";
            MessageBox.Show(
                "Enter a positive number.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
            return;
        }

        if (appSettings.Durations.Contains(newDuration)) {
            CustomDurationTXBX.Text = "";
            MessageBox.Show(
                $"{newDuration} is already a duration.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
            return;
        }

        Duration newCustomDuration = new() {
            Minutes = newDuration
        };

        Durations.Add(newCustomDuration);
        var sortedDurations = Durations.OrderByDescending(i => i).ToList();
        Durations.Clear();
        foreach (var item in sortedDurations) {
            Durations.Add(item);
        }
        appSettings.Durations.Add(newDuration);

        CustomDurationTXBX.Text = "";
    }

    private void defaultRDBTN_Click(object sender, EventArgs e) {
        appSettings.Icon = TrayIcon.Default;
    }

    private void eyeZZZRDBTN_Click(object sender, EventArgs e) {
        appSettings.Icon = TrayIcon.EyeWithZzz;
    }

    private void mugRDBTN_Click(object sender, EventArgs e) {
        appSettings.Icon = TrayIcon.Mug;
    }

    private void ActivateChkBox_CheckedChanged(object sender, EventArgs e) {
        appSettings.ActivateOnLaunch = ActivateChkBox.Checked;
    }

    private void SettingsAtLaunchChkBox_CheckedChanged(object sender, EventArgs e) {
        appSettings.ShowMessageOnLaunch = SettingsAtLaunchChkBox.Checked;
    }

    private void generalTooltipRDBTN_Click(object sender, EventArgs e) {
        appSettings.TooltipFormat = TooltipFormat.General;
    }

    private void specificTooltipRDBTN_Click(object sender, EventArgs e) {
        appSettings.TooltipFormat = TooltipFormat.Specific;
    }
}
