using Caffeinated.Helpers;
using Caffeinated.Properties;
using Humanizer;
using Microsoft.Win32;
using RegistryUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Caffeinated;

public partial class AppContext : ApplicationContext {
    private readonly NotifyIcon? notifyIcon;
    private readonly Container? components;
    private Icon? onIcon;
    private Icon? offIcon;
    private bool isActivated = false;
    private DateTime? endTime;
    private readonly Timer? timer;
    private readonly Timer updateTooltipTimer = new();
    private SettingsForm? settingsForm = null;
    private AboutForm? aboutForm = null;
    private bool isLightTheme = false;
    private readonly AppSettings? appSettings;
    private const string themeKeyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";

    [STAThread]
    private static void Main() {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        AppContext? context = new();
        if (context.notifyIcon == null) {
            Application.Exit();
        }
        else {
            Application.Run(context);
        }
    }

    public AppContext() {
        // Caffeinated.exe
        Process[]? processes = Process.GetProcessesByName("Caffeinated");
        if (processes.Length > 1) {
            // Is already running
            return;
        }

        components = new Container();
        timer = new Timer(components);
        timer.Tick += new EventHandler(timer_Tick);

        updateTooltipTimer = new Timer(components);
        updateTooltipTimer.Tick += new EventHandler(UpdateTooltipTimer_Tick);
        updateTooltipTimer.Interval = 10000; // 5 seconds
        updateTooltipTimer.Start();

        appSettings = new AppSettings();

        SetIsLightTheme();

        if (Registry.CurrentUser.OpenSubKey(themeKeyPath) is RegistryKey key) {
            RegistryMonitor monitor = new(key);
            monitor.RegChanged += new EventHandler(SetIsLightTheme);
            monitor.Start();
        }

        notifyIcon = new(components) {
            // tooltip
            Text = "Caffeinated",
            Visible = true
        };

        // Handle the DoubleClick event to activate the form.
        notifyIcon.MouseClick += new MouseEventHandler(notifyIcon1_Click);

        setIcons();
        setContextMenu();

        if (appSettings.ActivateOnLaunch) {
            activate(appSettings.DefaultDuration);
        }
        else {
            deactivate();
        }

        if (appSettings.ShowMessageOnLaunch || appSettings.IsFirstLaunch) {
            if (appSettings.IsFirstLaunch)
                appSettings.IsFirstLaunch = false;

            showSettings();
        }
    }

    private void UpdateTooltipTimer_Tick(object? sender, EventArgs e)
    {
        if (notifyIcon is null)
            return;

        updateNotifyIconText();
    }

    private void SetIsLightTheme(object? sender = null, EventArgs? e = null) {
        try {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(themeKeyPath);
            if (key is null) {
                return;
            }
            Object? o = key.GetValue("SystemUsesLightTheme");
            if (o is null) {
                return;
            }

            if (o.ToString() == "1") {
                isLightTheme = true;
            }
            else {
                isLightTheme = false;
            }
        }
        catch (Exception) {
            isLightTheme = false;
        }

        setIcons();
        setContextMenu();

        if (notifyIcon != null) {
            if (isActivated) {
                notifyIcon.Icon = onIcon;
            }
            else {
                notifyIcon.Icon = offIcon;
            }
        }
    }

    private void setIcons() {
        if (appSettings == null) {
            return;
        }

        switch (appSettings.Icon)
        {
            case TrayIcon.Mug:
                if (isLightTheme) {
                    offIcon = new Icon(
                        Resources.Mug_Sleep_Black_icon,
                        SystemInformation.SmallIconSize
                    );
                    onIcon = new Icon(
                        Resources.Mug_Active_Black_icon,
                        SystemInformation.SmallIconSize
                        );
                }
                else {
                    offIcon = new Icon(
                        Resources.mug_sleep_icon,
                        SystemInformation.SmallIconSize
                    );
                    onIcon = new Icon(
                        Resources.mug_active_icon,
                        SystemInformation.SmallIconSize
                    );
                }

                break;
            case TrayIcon.EyeWithZzz:
                if (isLightTheme) {
                    offIcon = new Icon(
                        Resources.Eye_zzz_Sleep_Black_icon,
                        SystemInformation.SmallIconSize
                    );
                    onIcon = new Icon(
                        Resources.Eye_zzz_Active_Black_icon,
                        SystemInformation.SmallIconSize
                    );
                }
                else {
                    offIcon = new Icon(
                        Resources.Eye_zzz_Sleep_icon,
                        SystemInformation.SmallIconSize
                    );
                    onIcon = new Icon(
                        Resources.Eye_zzz_Active_icon,
                        SystemInformation.SmallIconSize
                    );
                }
                break;
            default:
                if (isLightTheme) {
                    offIcon = new Icon(
                    Resources.Caffeine_Black_icon,
                    SystemInformation.SmallIconSize
                );
                    onIcon = new Icon(
                        Resources.SleepEye_Black_icon,
                        SystemInformation.SmallIconSize
                    );
                }
                else {
                    offIcon = new Icon(
                        Resources.cup_coffee_icon_bw,
                        SystemInformation.SmallIconSize
                    );
                    onIcon = new Icon(
                        Resources.cup_coffee_icon,
                        SystemInformation.SmallIconSize
                    );
                }
                break;
        }
    }

    public void setContextMenu() {
        if (appSettings == null || notifyIcon == null) {
            return;
        }

        ContextMenuStrip? contextMenu = new();

        ToolStripMenuItem? exitItem = new("E&xit");
        exitItem.Click += new(exitItem_Click);

        // If the user deleted all time settings, add 0 back in.
        if (appSettings.Durations.Count == 0) {
            appSettings.DefaultDuration = 0;
        }

        // we want the lower durations to be closer to the mouse. So, 
        ObservableCollection<int>? times = appSettings.Durations;
        IEnumerable<int> sortedTimes = [];
        if ((new Taskbar()).Position == TaskbarPosition.Top) {
            if (times != null) {
                sortedTimes = times.OrderBy(i => i);
            }
        }
        else {
            if (times != null) {
                sortedTimes = times.OrderByDescending(i => i);
            }
        }

        ToolStripMenuItem? settingsItem = new("&Settings...");
        settingsItem.Click += new(settingsItem_Click);

        ToolStripMenuItem? aboutItem = new("&About...");
        aboutItem.Click += new(aboutItem_Click);

        ContextMenuStrip? settingsMenuItem = new() {
            Text = "Settings",
        };

        contextMenu.Items.AddRange(
            [
                settingsItem,
                aboutItem,
                exitItem
            ]
        );
        contextMenu.Items.Add(new ToolStripSeparator());

        foreach (int time in sortedTimes) {
            ToolStripMenuItem? item = new(Duration.ToDescription(time))
            {
                Tag = time
            };
            item.Click += new(item_Click);
            contextMenu.Items.Add(item);
        }

        notifyIcon.ContextMenuStrip = contextMenu;
    }

    private void aboutItem_Click(object? sender, EventArgs e) {
        if (Application.OpenForms.OfType<AboutForm>().Any() == false) {
            aboutForm = new();
            aboutForm.Show();
        }
    }

    private void settingsItem_Click(object? sender, EventArgs e) {
        if (Application.OpenForms.OfType<SettingsForm>().Any() == false) {
            showSettings();
        }
    }

    private void showSettings() {
        if (appSettings == null) {
            return;
        }

        settingsForm = new(appSettings);
        settingsForm.FormClosing += SettingsForm_FormClosing;
        settingsForm.Show();
    }

    private void SettingsForm_FormClosing(object? sender, FormClosingEventArgs e) {
        setContextMenu();
        SetIsLightTheme();
        setIcons();

        if (notifyIcon != null) {
            if (isActivated) {
                notifyIcon.Icon = onIcon;
            }
            else {
                notifyIcon.Icon = offIcon;
            }
        }
    }

    private void timer_Tick(object? sender, EventArgs e) {
        deactivate();
    }

    private void item_Click(object? sender, EventArgs e) {
        if (sender is ToolStripMenuItem tsmi && tsmi.Tag is int time) {
            activate(time);
        }
    }

    private void notifyIcon1_Click(object? sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }

        bool? isactive = isActive();

        if (isactive != null && isactive == true) {
            deactivate();
        }
        else {
            if (appSettings != null) {
                activate(appSettings.DefaultDuration);
            }
        }
    }

    private static void ShowError() {
        MessageBox.Show(
            "Call to SetThreadExecutionState failed.",
            "Caffeinated",
            MessageBoxButtons.OK
        );
    }

    private bool? isActive() {
        if (notifyIcon != null) {
            return notifyIcon.Icon == onIcon;
        }

        return false;
    }

    private void activate(int duration) {
        uint sleepDisabled = NativeMethods.ES_CONTINUOUS |
                            NativeMethods.ES_DISPLAY_REQUIRED;
        uint previousState = NativeMethods.SetThreadExecutionState(sleepDisabled);
        if (previousState == 0) {
            ShowError();
            ExitThread();
        }
        int timerIntervalInMilliseconds = duration * 60 * 1000;
        if (timerIntervalInMilliseconds > 0
            && timer != null) {
            timer.Interval = timerIntervalInMilliseconds;
            timer.Start();
            endTime = DateTime.Now.AddMilliseconds(timerIntervalInMilliseconds);
        }
        else {
            endTime = null;
        }
        isActivated = true;


        if (notifyIcon is null)
            return;

        notifyIcon.Icon = onIcon;
        updateNotifyIconText();
    }

    private void updateNotifyIconText() {
        if (notifyIcon is null)
            return;

        if (notifyIcon.Icon == offIcon)
        {
            notifyIcon.Text = "Caffeinated: sleep allowed";
            return;
        }

        if (endTime is null) {
            notifyIcon.Text = $"Caffeinated: No sleep indefinitely";
            return;
        }

        string timeRemaining = endTime.Value.AddSeconds(2).Humanize() ;
        Debug.WriteLine($"timeRemaining {timeRemaining}");
        notifyIcon.Text = $"Caffeinated: No sleep for about {timeRemaining}";
    }

    private void deactivate() {
        timer?.Stop();

        uint result = NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);
        if (result == 0) {
            ShowError();
        }
        isActivated = false;

        if (notifyIcon != null) {
            notifyIcon.Icon = offIcon;
            notifyIcon.Text = "Caffeinated: sleep allowed";
        }
    }

    private void exitItem_Click(object? Sender, EventArgs e) {
        deactivate();
        notifyIcon?.Dispose();

        ExitThread();
    }

    protected override void Dispose(bool disposing) {
        if (disposing && components != null) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
}