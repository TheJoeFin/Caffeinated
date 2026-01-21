using Caffeinated.Helpers;
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
using System.Threading;
using System.Windows.Forms;
using Resources = Caffeinated.Properties.Resources;

namespace Caffeinated;

public partial class AppContext : ApplicationContext {
    private readonly NotifyIcon? notifyIcon;
    private readonly Container? components;
    private Icon? onIcon;
    private Icon? offIcon;
    private bool isActivated = false;
    private DateTime? endTime;
    private readonly System.Windows.Forms.Timer? timer;
    private readonly System.Windows.Forms.Timer updateTooltipTimer = new();
    private SettingsForm? settingsForm = null;
    private AboutForm? aboutForm = null;
    private bool isLightTheme = false;
    private readonly AppSettings? appSettings;
    private const string themeKeyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";
    private readonly Lock iconLock = new();
    private static readonly Dictionary<string, Bitmap> symbolCache = [];
    private readonly MessageWindow? messageWindow;

    private const int WM_QUERYENDSESSION = 0x0011;
    private const int WM_ENDSESSION = 0x0016;
    private const int ENDSESSION_CLOSEAPP = 0x1;

    [STAThread]
    private static void Main() {
        // Add global exception handlers
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.SetColorMode(SystemColorMode.System);

        // Register for restart after updates/shutdowns
        // Don't restart after crashes or hangs - only for system updates
        _ = NativeMethods.RegisterApplicationRestart(
            null,
            NativeMethods.RESTART_NO_CRASH | NativeMethods.RESTART_NO_HANG
        );

        AppContext? context = new();
        if (context.notifyIcon == null) {
            Application.Exit();
        }
        else {
            Application.Run(context);
        }
    }

    private static void Application_ThreadException(object? sender, ThreadExceptionEventArgs e) {
        Debug.WriteLine($"UI Thread Exception: {e.Exception}");
        ExceptionLogService.LogException(e.Exception);
    }

    private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e) {
        Debug.WriteLine($"Unhandled Exception: {e.ExceptionObject}");
        if (e.ExceptionObject is Exception exception) {
            ExceptionLogService.LogException(exception);
        }
    }

    internal void PerformGracefulShutdown() {
        try {
            // 1. Stop all timers immediately
            timer?.Stop();
            updateTooltipTimer?.Stop();

            // 2. Deactivate caffeination
            _ = NativeMethods.SetThreadExecutionState(NativeMethods.ES_CONTINUOUS);

            // 3. Save settings (already auto-saved via AppSettings setters)

            // 4. Dispose resources
            lock (iconLock) {
                onIcon?.Dispose();
                offIcon?.Dispose();
            }

            notifyIcon?.Dispose();
            components?.Dispose();

            // 5. Exit cleanly
            ExitThread();
        }
        catch {
            // Swallow exceptions during shutdown - we're terminating anyway
        }
    }

    private static bool IsAnotherInstanceRunning() {
        Process current = Process.GetCurrentProcess();
        Process[] processes = Process.GetProcessesByName(current.ProcessName);

        return processes.Length > 1;
    }

    private void SystemEvents_SessionEnding(object? sender, SessionEndingEventArgs e) {
        // User is logging off or system is shutting down
        PerformGracefulShutdown();
    }

    public AppContext() {
        // Caffeinated.exe
        if (IsAnotherInstanceRunning()) {
            // Is already running
            return;
        }

        // Create hidden window to receive Windows messages
        messageWindow = new MessageWindow(this);

        // Subscribe to session ending events
        SystemEvents.SessionEnding += SystemEvents_SessionEnding;

        components = new Container();
        timer = new System.Windows.Forms.Timer(components);
        timer.Tick += new EventHandler(timer_Tick);

        updateTooltipTimer = new System.Windows.Forms.Timer(components);
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

    private void UpdateTooltipTimer_Tick(object? sender, EventArgs e) {
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

        // Clear symbol cache when theme changes
        ClearSymbolCache();

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

        lock (iconLock) {
            // Dispose old icons before creating new ones
            onIcon?.Dispose();
            offIcon?.Dispose();

            switch (appSettings.Icon) {
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
    }

    public void setContextMenu() {
        if (appSettings == null || notifyIcon == null) {
            return;
        }

        ContextMenuStrip? contextMenu = new() {
            Renderer = new ModernMenuRenderer(isLightTheme),
            ShowImageMargin = true,
            Padding = new Padding(2)
        };

        // If the user deleted all time settings, add 0 back in.
        if (appSettings.Durations.Count == 0) {
            appSettings.DefaultDuration = 0;
        }

        Padding itemPadding = new(6, 14, 6, 6);

        ToolStripMenuItem? settingsItem = new("&Settings...") {
            Image = CreateSymbolImage("⚙", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = itemPadding,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };
        settingsItem.Click += new(settingsItem_Click);
        contextMenu.Items.Add(settingsItem);

        ToolStripMenuItem? aboutItem = new("&About...") {
            Image = CreateSymbolImage("ℹ", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = itemPadding,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };
        aboutItem.Click += new(aboutItem_Click);
        contextMenu.Items.Add(aboutItem);

        ToolStripMenuItem? exitItem = new("E&xit") {
            Image = CreateSymbolImage("✖", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = itemPadding,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText,
        };
        exitItem.Click += new(exitItem_Click);
        contextMenu.Items.Add(exitItem);

        contextMenu.Items.Add(new ToolStripSeparator());

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

        foreach (int time in sortedTimes) {
            ToolStripMenuItem? item = new(Duration.ToDescription(time)) {
                Tag = time,
                Image = CreateSymbolImage("⏰", isLightTheme),
                ImageScaling = ToolStripItemImageScaling.None,
                Padding = itemPadding,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            item.Click += new(item_Click);
            contextMenu.Items.Add(item);
        }

        notifyIcon.ContextMenuStrip = contextMenu;
    }

    private static Bitmap CreateSymbolImage(string symbol, bool isLightTheme) {
        int size = 24;
        string cacheKey = $"{symbol}_{isLightTheme}";

        if (symbolCache.TryGetValue(cacheKey, out Bitmap? cached)) {
            return cached;
        }

        Bitmap bitmap = new(size, size);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        using Font font = new("Segoe UI Symbol", 11f, FontStyle.Regular);
        Color textColor = isLightTheme ? Color.FromArgb(32, 32, 32) : Color.FromArgb(240, 240, 240);
        using SolidBrush brush = new(textColor);

        StringFormat format = new() {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        graphics.DrawString(symbol, font, brush, new RectangleF(0, 0, size, size), format);

        symbolCache[cacheKey] = bitmap;

        return bitmap;
    }

    private static void ClearSymbolCache() {
        foreach (Bitmap? bitmap in symbolCache.Values) {
            bitmap?.Dispose();
        }
        symbolCache.Clear();
    }

    private void aboutItem_Click(object? sender, EventArgs e) {
        if (Application.OpenForms.OfType<AboutForm>().Any() == false) {
            aboutForm = new();
            aboutForm.PositionNearTrayIcon(notifyIcon);
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
        settingsForm.PositionNearTrayIcon(notifyIcon);
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
        if (sender is ToolStripMenuItem toolItem && toolItem.Tag is int time) {
            activate(time);
        }
    }

    private void notifyIcon1_Click(object? sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) {
            return;
        }

        bool? isActive = this.isActive();

        if (isActive is not null and true) {
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
            endTime = DateTime.Now.AddMilliseconds(timerIntervalInMilliseconds).AddSeconds(1);
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

        if (notifyIcon.Icon == offIcon) {
            notifyIcon.Text = "Caffeinated: sleep allowed";
            return;
        }

        if (endTime is null) {
            notifyIcon.Text = $"Caffeinated: No sleep indefinitely";
            return;
        }

        if (appSettings is null)
            return;

        if (appSettings.TooltipFormat == TooltipFormat.Specific) {
            TimeSpan remaining = endTime.Value - DateTime.Now;
            int hours = (int)remaining.TotalHours;
            int minutes = remaining.Minutes;
            int seconds = remaining.Seconds;

            List<string> parts = [];

            if (hours > 0) {
                parts.Add($"{hours} hour{(hours != 1 ? "s" : "")}");
            }

            if (minutes > 0) {
                parts.Add($"{minutes} minute{(minutes != 1 ? "s" : "")}");
            }

            if (seconds > 0 && hours == 0 && minutes < 5) {
                parts.Add($"{seconds} second{(seconds != 1 ? "s" : "")}");
            }

            string timeText = parts.Count switch {
                0 => "0 seconds",
                1 => parts[0],
                2 => $"{parts[0]} and {parts[1]}",
                _ => string.Join(", ", parts.Take(parts.Count - 1)) + $", and {parts[^1]}"
            };

            notifyIcon.Text = $"Caffeinated: No sleep for {timeText}";
        }
        else {
            string timeRemaining = endTime.Value.AddSeconds(2).Humanize();
            Debug.WriteLine($"timeRemaining {timeRemaining}");
            notifyIcon.Text = $"Caffeinated: No sleep for about {timeRemaining}";
        }
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
        if (disposing) {
            // Unsubscribe from system events
            SystemEvents.SessionEnding -= SystemEvents_SessionEnding;

            lock (iconLock) {
                onIcon?.Dispose();
                offIcon?.Dispose();
            }

            // Clear symbol cache
            ClearSymbolCache();

            timer?.Dispose();
            updateTooltipTimer?.Dispose();
            messageWindow?.Dispose();

            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}