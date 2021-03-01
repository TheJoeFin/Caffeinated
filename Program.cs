using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Caffeinated.Properties;
using Microsoft.Win32;

namespace Caffeinated {
    public class Duration: IComparable {
        public int Minutes { get; set; }
        public string Description {
            get {
                return Duration.ToDescription(this.Minutes);
            }
        }

        public static string ToDescription(int time) {
            if (time == 0) {
                return "Indefinitely";
            }
            
            string returnDescription = "";

            if(time >= 60) {
                int hours = time / 60;
                if (hours == 1) {
                    returnDescription = "1 hr ";
                }
                else {
                    returnDescription = String.Format("{0} hrs ", hours);
                }
            }
            int mins = time % 60;
            if(mins == 1) {
                returnDescription += String.Format("{0} min", mins);
            }
            if (mins > 1) {
                returnDescription += String.Format("{0} mins", mins);
            }

            return returnDescription;
        }

        public int CompareTo(object obj) {
            if (obj == null) return 1;

            Duration otherDuration = obj as Duration;

            if (otherDuration != null)
            {
                if (otherDuration.Minutes > this.Minutes)
                    return 1;
                
                if (otherDuration.Minutes < this.Minutes)
                    return -1;

                return 0;
            }
            else
                return 1;
        }
    }

    internal static class NativeMethods {
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;
    }

    public class AppContext : ApplicationContext {
        private NotifyIcon notifyIcon;
        private IContainer components;
        private Icon onIcon;
        private Icon offIcon;
        private uint? oldState = null;
        private bool isActivated = false;
        private Timer timer;
        private SettingsForm settingsForm = null;
        private AboutForm aboutForm = null;
        private bool isLightTheme = false;

        [STAThread]
        static void Main() {
            var context = new AppContext();
            if(context.notifyIcon == null)
                Application.Exit();
            else
                Application.Run(context);
        }

        public AppContext() {
            // Caffeinated.exe
            var processes = Process.GetProcessesByName("Caffeinated");
            if (processes.Length > 1)
            {
                // Is already running
                return;
            }

            this.components = new Container();
            this.timer = new Timer(components);
            timer.Tick += new EventHandler(timer_Tick);
            SetIsLightTheme();

            // TODO: Looks like this is clearing custom durations. Need to look into this more.
            // Settings.Default.Upgrade();
            // Settings.Default.Save();
            
            setIcons();

            this.notifyIcon = new NotifyIcon(this.components);

            setContextMenu();

            // tooltip
            notifyIcon.Text = "Caffeinated";
            notifyIcon.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon.MouseClick += new MouseEventHandler(notifyIcon1_Click);

            if (Settings.Default.ActivateAtLaunch)
            {
                activate(Settings.Default.DefaultDuration);
            }
            else
            {
                deactivate();
            }
            if (Settings.Default.ShowSettingsAtLaunch)
            {
                showSettings();
            }
        }

        void SetIsLightTheme() {
            try {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize")) {
                    if (key != null) {
                        Object o = key.GetValue("SystemUsesLightTheme");
                        if (o != null) {
                            if (o.ToString() == "1")
                                isLightTheme = true;
                            else
                                isLightTheme = false;
                        }
                    }
                }
            }
            catch (Exception ex) {
                isLightTheme = false;
            }
        }

        private static String HexConverter(Color c) {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private void setIcons() {
            switch (Settings.Default.Icon) {
                case "Mug":
                    if (isLightTheme){
                        this.offIcon = new Icon(
                            Properties.Resources.Mug_Sleep_Black_icon,
                            SystemInformation.SmallIconSize
                        );
                        this.onIcon = new Icon(
                            Properties.Resources.Mug_Active_Black_icon,
                            SystemInformation.SmallIconSize
                            );
                    } else{
                        this.offIcon = new Icon(
                            Properties.Resources.mug_sleep_icon,
                            SystemInformation.SmallIconSize
                        );
                        this.onIcon = new Icon(
                            Properties.Resources.mug_active_icon,
                            SystemInformation.SmallIconSize
                        );
                    }

                    break;
                case "Eye-ZZZ":
                    if (isLightTheme){
                        this.offIcon = new Icon(
                            Properties.Resources.Eye_zzz_Sleep_Black_icon,
                            SystemInformation.SmallIconSize
                        );
                        this.onIcon = new Icon(
                            Properties.Resources.Eye_zzz_Active_Black_icon,
                            SystemInformation.SmallIconSize
                        );
                    } else {
                        this.offIcon = new Icon(
                            Properties.Resources.Eye_zzz_Sleep_icon,
                            SystemInformation.SmallIconSize
                        );
                        this.onIcon = new Icon(
                            Properties.Resources.Eye_zzz_Active_icon,
                            SystemInformation.SmallIconSize
                        );
                    }
                    break;
                default:
                    if (isLightTheme) {
                        this.offIcon = new Icon(
                        Properties.Resources.Caffeine_Black_icon,
                        SystemInformation.SmallIconSize
                    );
                        this.onIcon = new Icon(
                            Properties.Resources.SleepEye_Black_icon,
                            SystemInformation.SmallIconSize
                        );
                    } else {
                        this.offIcon = new Icon(
                            Properties.Resources.cup_coffee_icon_bw,
                            SystemInformation.SmallIconSize
                        );
                        this.onIcon = new Icon(
                            Properties.Resources.cup_coffee_icon,
                            SystemInformation.SmallIconSize
                        );
                    }
                    break;
            }
        }

        public void setContextMenu() {
            var contextMenu = new ContextMenuStrip();

            var exitItem = new ToolStripMenuItem("E&xit");
            exitItem.Click += new EventHandler(this.exitItem_Click);

            // If the user deleted all time settings, add 0 back in.
            if (Settings.Default.Durations.Length == 0)
                Settings.Default.Durations = "0";

            // we want the lower durations to be closer to the mouse. So, 
            var times = Settings.Default.RealDurations;
            IEnumerable<int> sortedTimes = Enumerable.Empty<int>();
            if ((new Taskbar()).Position == TaskbarPosition.Top) {
                sortedTimes = times.OrderBy(i => i);
            }
            else {
                sortedTimes = times.OrderByDescending(i => i);
            }

            var settingsItem = new ToolStripMenuItem("&Settings...");
            settingsItem.Click += new EventHandler(settingsItem_Click);

            var aboutItem = new ToolStripMenuItem("&About...");
            aboutItem.Click += new EventHandler(aboutItem_Click);

            contextMenu.Items.AddRange(
                new ToolStripMenuItem[] {
                    settingsItem,
                    aboutItem,
                    exitItem,
                    // new ToolStripMenuItem("-"),
                    //activateForItem, 
                }
            );
            contextMenu.Items.Add(new ToolStripSeparator());

            foreach (var time in sortedTimes) {
                var item = new ToolStripMenuItem(Duration.ToDescription(time));
                item.Tag = time;
                item.Click += new EventHandler(item_Click);
                contextMenu.Items.Add(item);
            }

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        void aboutItem_Click(object sender, EventArgs e) {
            if (Application.OpenForms.OfType<AboutForm>().Any() == false) {
                aboutForm = new AboutForm();
                aboutForm.Show();
            }
        }

        void settingsItem_Click(object sender, EventArgs e) {
            if(Application.OpenForms.OfType<SettingsForm>().Any() == false)
                showSettings();
        }

        void showSettings() {
            settingsForm = new SettingsForm();
            settingsForm.FormClosing += SettingsForm_FormClosing;
            settingsForm.Show();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e) {
            setContextMenu();
            SetIsLightTheme();
            setIcons();

            if (isActivated)
                notifyIcon.Icon = onIcon;
            else
                notifyIcon.Icon = offIcon;
        }

        void timer_Tick(object sender, EventArgs e) {
            deactivate();
        }

        void item_Click(object sender, EventArgs e) {
            int time = (int)((ToolStripMenuItem)sender).Tag;
            this.activate(time);
        }

        void notifyIcon1_Click(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;
            if (this.isActive())
                this.deactivate();
            else
                this.activate(Settings.Default.DefaultDuration);
        }

        void ShowError() {
            MessageBox.Show(
                "Call to SetThreadExecutionState failed.",
                "Caffeinated",
                MessageBoxButtons.OK
            );
        }

        bool isActive() {
            return notifyIcon.Icon == onIcon;
        }

        void activate(int duration) {
            var sleepDisabled = NativeMethods.ES_CONTINUOUS |
                                NativeMethods.ES_DISPLAY_REQUIRED;
            oldState = NativeMethods.SetThreadExecutionState(sleepDisabled);
            if (oldState == 0) {
                ShowError();
                ExitThread();
            }
            if (duration > 0) {
                this.timer.Interval = duration * 60 * 1000;
                this.timer.Start();
            }
            this.isActivated = true;
            this.notifyIcon.Icon = onIcon;
            this.notifyIcon.Text = "Caffeinated: sleep not allowed!";
        }

        void deactivate() {
            timer.Stop();
            if (oldState.HasValue) {
                uint result = 
                    NativeMethods.SetThreadExecutionState(oldState.Value);
                if (result == 0) {
                    ShowError();
                }
            }
            this.isActivated = false;
            this.notifyIcon.Icon = offIcon;
            this.notifyIcon.Text = "Caffeinated: sleep allowed";
        }

        private void exitItem_Click(object Sender, EventArgs e) {
            deactivate();
            notifyIcon.Dispose();
            ExitThread();
        }

        protected override void Dispose(bool disposing) {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}