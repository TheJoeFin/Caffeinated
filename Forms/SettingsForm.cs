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

        if (appSettings.ShowMessageOnLaunch)
            SettingsAtLaunchChkBox.Checked = true;

        if (appSettings.ActivateOnLaunch)
            ActivateChkBox.Checked = true;

        SetupDurationsListView();

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
        hoursLabel.ForeColor = ForeColor;
        minutesLabel.ForeColor = ForeColor;
        
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
        
        // Update scrollable panel background
        scrollablePanel.BackColor = BackColor;
        
        // Update TableLayoutPanels background
        tableLayoutPanel1.BackColor = BackColor;
        tableLayoutPanel2.BackColor = BackColor;
        tableLayoutPanel3.BackColor = BackColor;
        tableLayoutPanel4.BackColor = BackColor;
        
        // Update picture boxes background
        pictureBox1.BackColor = BackColor;
        
        // Update ListView colors
        DurationsListView.BackColor = BackColor;
        DurationsListView.ForeColor = ForeColor;
        DurationsListView.Invalidate();
        
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

    private void SetupDurationsListView() {
        DurationsListView.Columns.Add("Default", 60);
        DurationsListView.Columns.Add("Duration", 200);
        DurationsListView.Columns.Add("", 40);
        
        RefreshDurationsListView();
    }

    private void RefreshDurationsListView() {
        DurationsListView.BeginUpdate();
        DurationsListView.Items.Clear();
        
        var sortedDurations = Durations.OrderByDescending(d => d.Minutes).ToList();
        
        foreach (Duration duration in sortedDurations) {
            ListViewItem item = new();
            item.SubItems.Add(duration.Description);
            item.SubItems.Add("🗑");
            item.Tag = duration;
            item.UseItemStyleForSubItems = false;
            DurationsListView.Items.Add(item);
        }
        
        int itemHeight = 30;
        int headerHeight = 26;
        int borderHeight = 4;
        int totalHeight = headerHeight + (sortedDurations.Count * itemHeight) + borderHeight;
        DurationsListView.Height = totalHeight;
        DurationsListView.Width = 310;
        
        DurationsListView.EndUpdate();
    }

    private void DurationsListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e) {
        e.Graphics.FillRectangle(new SolidBrush(BackColor), e.Bounds);
        
        TextRenderer.DrawText(
            e.Graphics,
            e.Header.Text,
            DurationsListView.Font,
            e.Bounds,
            ForeColor,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter
        );
    }

    private void DurationsListView_DrawItem(object? sender, DrawListViewItemEventArgs e) {
        e.DrawDefault = false;
    }

    private void DurationsListView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e) {
        if (e.Item.Tag is not Duration duration)
            return;

        e.Graphics.FillRectangle(
            new SolidBrush(e.Item.Selected ? SystemColors.Highlight : BackColor),
            e.Bounds
        );

        if (e.ColumnIndex == 0) {
            bool isDefault = duration.Minutes == appSettings.DefaultDuration;
            int centerX = e.Bounds.X + e.Bounds.Width / 2;
            int centerY = e.Bounds.Y + e.Bounds.Height / 2;
            int radioSize = 16;
            
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(
                new Pen(ForeColor, 2),
                centerX - radioSize / 2,
                centerY - radioSize / 2,
                radioSize,
                radioSize
            );
            
            if (isDefault) {
                e.Graphics.FillEllipse(
                    new SolidBrush(ForeColor),
                    centerX - radioSize / 4,
                    centerY - radioSize / 4,
                    radioSize / 2,
                    radioSize / 2
                );
            }
        }
        else if (e.ColumnIndex == 1) {
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                DurationsListView.Font,
                e.Bounds,
                e.Item.Selected ? SystemColors.HighlightText : ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );
        }
        else if (e.ColumnIndex == 2) {
            using Font emojiFont = new("Segoe UI Emoji", 11);
            TextRenderer.DrawText(
                e.Graphics,
                "🗑️",
                emojiFont,
                e.Bounds,
                e.Item.Selected ? SystemColors.HighlightText : ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
    }

    private void DurationsListView_MouseClick(object? sender, MouseEventArgs e) {
        ListViewHitTestInfo hit = DurationsListView.HitTest(e.Location);
        
        if (hit.Item?.Tag is not Duration duration)
            return;

        if (hit.SubItem == hit.Item.SubItems[0]) {
            appSettings.DefaultDuration = duration.Minutes;
            DurationsListView.Invalidate();
        }
        else if (hit.SubItem == hit.Item.SubItems[2]) {
            DialogResult result = MessageBox.Show(
                $"Delete {duration.Description}?",
                "Caffeinated",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.Yes) {
                Durations.Remove(duration);
                appSettings.Durations.Remove(duration.Minutes);
                RefreshDurationsListView();
            }
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
        bool hoursParsed = int.TryParse(hoursTextBox.Text, out int hours);
        bool minutesParsed = int.TryParse(minutesTextBox.Text, out int minutes);

        if (!hoursParsed || !minutesParsed)
            return;

        if (hours < 0 || minutes < 0) {
            MessageBox.Show(
                "Enter positive numbers.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
            return;
        }

        if (minutes >= 60) {
            MessageBox.Show(
                "Minutes must be less than 60.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
            return;
        }

        int newDuration = hours * 60 + minutes;

        if (newDuration == 0) {
            MessageBox.Show(
                "Duration must be greater than 0.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
            return;
        }

        if (appSettings.Durations.Contains(newDuration)) {
            MessageBox.Show(
                $"{newDuration} minutes is already a duration.",
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
        
        RefreshDurationsListView();

        hoursTextBox.Text = "0";
        minutesTextBox.Text = "0";
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

    private void exceptionLogBtn_Click(object? sender, EventArgs e) {
        if (Application.OpenForms.OfType<ExceptionLogForm>().Any() == false) {
            var exceptionLogForm = new ExceptionLogForm();
            exceptionLogForm.Show();
        }
    }
}
