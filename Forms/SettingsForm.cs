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
        
        // Update durations table colors
        if (durationsTable != null) {
            durationsTable.BackColor = BackColor;
            foreach (Control control in durationsTable.Controls) {
                if (control is Label label) {
                    label.ForeColor = ForeColor;
                    label.BackColor = BackColor;
                }
                else if (control is Button button) {
                    button.ForeColor = ForeColor;
                    button.BackColor = BackColor;
                }
                else if (control is RadioButton radio) {
                    radio.ForeColor = ForeColor;
                    radio.BackColor = BackColor;
                }
            }
        }
        
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

    private TableLayoutPanel? durationsTable;

    private void SetupDurationsListView() {
        // Hide the old ListView
        DurationsListView.Visible = false;
        
        // Create new TableLayoutPanel for durations
        durationsTable = new TableLayoutPanel {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 3,
            Location = DurationsListView.Location,
            Name = "durationsTable",
            Padding = new Padding(0),
            Margin = new Padding(0),
            RowCount = 0,
            AutoScroll = false,  // CRITICAL: No scrolling on inner table
            Dock = DockStyle.None,  // Don't dock, let it auto-size
            Anchor = AnchorStyles.Top | AnchorStyles.Left  // Anchor to top-left
        };
        
        durationsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        durationsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        durationsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        
        // Add header row
        Label headerDefault = new() {
            Text = "Default",
            AutoSize = true,
            Font = new Font(Font, FontStyle.Bold),
            Padding = new Padding(5),
            Margin = new Padding(0)
        };
        
        Label headerDuration = new() {
            Text = "Duration",
            AutoSize = true,
            Font = new Font(Font, FontStyle.Bold),
            Padding = new Padding(5),
            Margin = new Padding(0)
        };
        
        Label headerDelete = new() {
            Text = "",
            AutoSize = true,
            Font = new Font(Font, FontStyle.Bold),
            Padding = new Padding(5),
            Margin = new Padding(0)
        };
        
        durationsTable.Controls.Add(headerDefault, 0, 0);
        durationsTable.Controls.Add(headerDuration, 1, 0);
        durationsTable.Controls.Add(headerDelete, 2, 0);
        durationsTable.RowCount = 1;
        durationsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        
        // Add to parent container
        Control? parent = DurationsListView.Parent;
        if (parent != null) {
            parent.Controls.Add(durationsTable);
        }
        
        RefreshDurationsListView();
    }

    private void RefreshDurationsListView() {
        if (durationsTable == null)
            return;
            
        durationsTable.SuspendLayout();
        
        // Remove all rows except header
        while (durationsTable.RowCount > 1) {
            for (int col = 0; col < durationsTable.ColumnCount; col++) {
                Control? control = durationsTable.GetControlFromPosition(col, durationsTable.RowCount - 1);
                if (control != null) {
                    durationsTable.Controls.Remove(control);
                    control.Dispose();
                }
            }
            durationsTable.RowCount--;
            durationsTable.RowStyles.RemoveAt(durationsTable.RowStyles.Count - 1);
        }
        
        var sortedDurations = Durations.OrderByDescending(d => d.Minutes).ToList();
        
        int row = 1;
        foreach (Duration duration in sortedDurations) {
            // Radio button for default
            RadioButton radioBtn = new() {
                AutoSize = true,
                Checked = duration.Minutes == appSettings.DefaultDuration,
                Tag = duration,
                Margin = new Padding(5),
                Text = ""
            };
            radioBtn.CheckedChanged += DurationRadio_CheckedChanged;
            
            // Duration label
            Label durationLabel = new() {
                Text = duration.Description,
                AutoSize = true,
                Padding = new Padding(5),
                Margin = new Padding(0),
                Anchor = AnchorStyles.Left
            };
            
            // Delete button
            Button deleteBtn = new() {
                Text = "🗑",
                AutoSize = true,
                Tag = duration,
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            deleteBtn.FlatAppearance.BorderSize = 0;
            deleteBtn.Click += DeleteDuration_Click;
            
            durationsTable.Controls.Add(radioBtn, 0, row);
            durationsTable.Controls.Add(durationLabel, 1, row);
            durationsTable.Controls.Add(deleteBtn, 2, row);
            
            durationsTable.RowCount++;
            durationsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            row++;
        }
        
        durationsTable.ResumeLayout();
    }

    private void DurationRadio_CheckedChanged(object? sender, EventArgs e) {
        if (sender is RadioButton radio && radio.Checked && radio.Tag is Duration duration) {
            appSettings.DefaultDuration = duration.Minutes;
            
            // Uncheck all other radio buttons
            if (durationsTable != null) {
                foreach (Control control in durationsTable.Controls) {
                    if (control is RadioButton otherRadio && otherRadio != radio) {
                        otherRadio.Checked = false;
                    }
                }
            }
        }
    }

    private void DeleteDuration_Click(object? sender, EventArgs e) {
        if (sender is Button button && button.Tag is Duration duration) {
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

    private void DurationsListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e) {
        // No longer needed
    }

    private void DurationsListView_DrawItem(object? sender, DrawListViewItemEventArgs e) {
        // No longer needed
    }

    private void DurationsListView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e) {
        // No longer needed
    }


    private void DurationsListView_MouseClick(object? sender, MouseEventArgs e) {
        // No longer needed
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
