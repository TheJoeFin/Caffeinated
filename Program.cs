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
        Application.SetColorMode(SystemColorMode.System);
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

        ContextMenuStrip? contextMenu = new()
        {
            Renderer = new ModernMenuRenderer(isLightTheme),
            ShowImageMargin = true,
            Padding = new Padding(2)
        };

        // If the user deleted all time settings, add 0 back in.
        if (appSettings.Durations.Count == 0) {
            appSettings.DefaultDuration = 0;
        }

        ToolStripMenuItem? settingsItem = new("&Settings...")
        {
            Image = CreateSymbolImage("⚙", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = new Padding(4, 6, 4, 6),
            ImageAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };
        settingsItem.Click += new(settingsItem_Click);
        contextMenu.Items.Add(settingsItem);

        ToolStripMenuItem? aboutItem = new("&About...")
        {
            Image = CreateSymbolImage("ℹ", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = new Padding(4, 6, 4, 6),
            ImageAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };
        aboutItem.Click += new(aboutItem_Click);
        contextMenu.Items.Add(aboutItem);

        ToolStripMenuItem? exitItem = new("E&xit")
        {
            Image = CreateSymbolImage("✖", isLightTheme),
            ImageScaling = ToolStripItemImageScaling.None,
            Padding = new Padding(4, 6, 4, 6),
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
            ToolStripMenuItem? item = new(Duration.ToDescription(time))
            {
                Tag = time,
                Image = CreateSymbolImage("⏰", isLightTheme),
                ImageScaling = ToolStripItemImageScaling.None,
                Padding = new Padding(4, 6, 4, 6),
                ImageAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            item.Click += new(item_Click);
            contextMenu.Items.Add(item);
        }

        notifyIcon.ContextMenuStrip = contextMenu;
    }

    private static Bitmap CreateSymbolImage(string symbol, bool isLightTheme) {
        int size = 16;
        int padding = 2;
        int totalSize = size + (padding * 2);
        Bitmap bitmap = new(totalSize, totalSize);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        using Font font = new("Segoe UI Symbol", 10f, FontStyle.Regular);
        Color textColor = isLightTheme ? Color.FromArgb(32, 32, 32) : Color.FromArgb(240, 240, 240);
        using SolidBrush brush = new(textColor);

        StringFormat format = new()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        graphics.DrawString(symbol, font, brush, new RectangleF(padding, padding, size, size), format);

        return bitmap;
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

        if (appSettings is null)
            return;

        if (appSettings.TooltipFormat == TooltipFormat.Specific) {
            TimeSpan remaining = endTime.Value - DateTime.Now;
            int hours = (int)remaining.TotalHours;
            int minutes = remaining.Minutes;

            string timeText;
            if (hours > 0 && minutes > 0) {
                timeText = $"{hours} hour{(hours != 1 ? "s" : "")} and {minutes} minute{(minutes != 1 ? "s" : "")}";
            }
            else if (hours > 0) {
                timeText = $"{hours} hour{(hours != 1 ? "s" : "")}";
            }
            else {
                timeText = $"{minutes} minute{(minutes != 1 ? "s" : "")}";
            }

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
            if (disposing && components != null) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    public class ModernMenuRenderer : ToolStripProfessionalRenderer {
        private readonly bool isLightTheme;

        public ModernMenuRenderer(bool isLightTheme) : base(new ModernColorTable(isLightTheme)) {
            this.isLightTheme = isLightTheme;
            RoundedEdges = false;
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
            if (e.Item.Selected) {
                Rectangle rect = new(Point.Empty, e.Item.Size);
                Color hoverColor = isLightTheme
                    ? Color.FromArgb(240, 240, 240)
                    : Color.FromArgb(50, 50, 50);

                using SolidBrush brush = new(hoverColor);
                e.Graphics.FillRectangle(brush, rect);

                Color borderColor = isLightTheme
                    ? Color.FromArgb(0, 120, 215)
                    : Color.FromArgb(0, 120, 215);

                using Pen pen = new(borderColor, 1);
                Rectangle borderRect = rect;
                borderRect.Width -= 1;
                borderRect.Height -= 1;
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            else {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
            if (e.Item is ToolStripSeparator) {
                base.OnRenderItemText(e);
                return;
            }

            e.TextColor = isLightTheme
                ? Color.FromArgb(32, 32, 32)
                : Color.FromArgb(240, 240, 240);

            e.TextFont = new Font(e.TextFont.FontFamily, e.TextFont.Size, FontStyle.Regular);

            Rectangle adjustedRect = e.TextRectangle;
            adjustedRect.Y += 3;
            e.TextRectangle = adjustedRect;

            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            Rectangle rect = new(0, e.Item.Height / 2, e.Item.Width, 1);

            Color separatorColor = isLightTheme
                ? Color.FromArgb(220, 220, 220)
                : Color.FromArgb(60, 60, 60);

            using Pen pen = new(separatorColor);
            e.Graphics.DrawLine(pen, rect.Left + 30, rect.Top, rect.Right - 5, rect.Top);
        }
    }

    public class ModernColorTable : ProfessionalColorTable {
        private readonly bool isLightTheme;

        public ModernColorTable(bool isLightTheme) {
            this.isLightTheme = isLightTheme;
        }

        public override Color MenuItemSelected => isLightTheme
            ? Color.FromArgb(240, 240, 240)
            : Color.FromArgb(50, 50, 50);

        public override Color MenuItemSelectedGradientBegin => MenuItemSelected;

        public override Color MenuItemSelectedGradientEnd => MenuItemSelected;

        public override Color MenuBorder => isLightTheme
            ? Color.FromArgb(204, 206, 219)
            : Color.FromArgb(60, 60, 60);

        public override Color MenuItemBorder => isLightTheme
            ? Color.FromArgb(0, 120, 215)
            : Color.FromArgb(0, 120, 215);

        public override Color ImageMarginGradientBegin => isLightTheme
            ? Color.FromArgb(250, 250, 250)
            : Color.FromArgb(30, 30, 30);

        public override Color ImageMarginGradientMiddle => ImageMarginGradientBegin;

        public override Color ImageMarginGradientEnd => ImageMarginGradientBegin;

        public override Color ToolStripDropDownBackground => isLightTheme
            ? Color.FromArgb(255, 255, 255)
            : Color.FromArgb(40, 40, 40);
    }