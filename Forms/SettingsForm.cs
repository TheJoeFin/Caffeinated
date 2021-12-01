using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Caffeinated.Helpers;
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
}
