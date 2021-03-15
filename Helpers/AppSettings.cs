using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Caffeinated.Helpers { 
    public class AppSettings {
        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        KeyValueConfigurationCollection appSettings;

        private bool _activateOnLaunch;

        public bool ActivateOnLaunch {
            get { return _activateOnLaunch; }
            set { 
                _activateOnLaunch = value;
                appSettings[nameof(ActivateOnLaunch)].Value = _activateOnLaunch.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        private bool _automaticallyLaunchWithWindows;

        public bool AutomaticallyLaunchWithWindows {
            get { return _automaticallyLaunchWithWindows; }
            set { 
                _automaticallyLaunchWithWindows = value;
                appSettings[nameof(AutomaticallyLaunchWithWindows)].Value = _automaticallyLaunchWithWindows.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        private bool _showMessageOnLaunch;

        public bool ShowMessageOnLaunch {
            get { return _showMessageOnLaunch; }
            set { 
                _showMessageOnLaunch = value;
                appSettings[nameof(ShowMessageOnLaunch)].Value = _showMessageOnLaunch.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        private int _defaultDuration;

        public int DefaultDuration {
            get { return _defaultDuration; }
            set { 
                _defaultDuration = value; 
                appSettings[nameof(DefaultDuration)].Value = _defaultDuration.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        private TrayIcon _icon;

        public TrayIcon Icon {
            get { return _icon; }
            set { 
                _icon = value; 
                appSettings[nameof(Icon)].Value = _icon.ToString();
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        private ObservableCollection<int> _durations;

        public ObservableCollection<int> Durations {
            get { return _durations; }
            set { 
                _durations = value;
                appSettings[nameof(Durations)].Value = string.Join(',',Durations.ToArray());
                configFile.Save(ConfigurationSaveMode.Modified);
            }
        }

        public AppSettings() {
            appSettings = configFile.AppSettings.Settings;

            try {
                string ActivateOnLaunchresult = appSettings[nameof(ActivateOnLaunch)].Value;
                _activateOnLaunch = bool.Parse(ActivateOnLaunchresult);
            }
            catch (Exception) {
                _activateOnLaunch = false;
                AddUpdateAppSettings(nameof(ActivateOnLaunch), _activateOnLaunch.ToString());
                Console.WriteLine($"Error reading _activateOnLaunch app settings");
            }

            try {
                string AutomaticallyLaunchWithWindowsresult = appSettings[nameof(AutomaticallyLaunchWithWindows)].Value;
                _automaticallyLaunchWithWindows = bool.Parse(AutomaticallyLaunchWithWindowsresult);
            }
            catch (Exception) {
                _automaticallyLaunchWithWindows = false;
                AddUpdateAppSettings(nameof(AutomaticallyLaunchWithWindows), _automaticallyLaunchWithWindows.ToString());
                Console.WriteLine("Error reading _automaticallyLaunchWithWindows app settings");
            }

            try {
                string ShowMessageOnLaunchresult = appSettings[nameof(ShowMessageOnLaunch)].Value;
                _showMessageOnLaunch = bool.Parse(ShowMessageOnLaunchresult);
            }
            catch (Exception) {
                _showMessageOnLaunch = true;
                AddUpdateAppSettings(nameof(ShowMessageOnLaunch), _showMessageOnLaunch.ToString());
                Console.WriteLine($"Error reading _showMessageOnLaunch app settings");
            }

            try {
                string DefaultDurationresult = appSettings[nameof(DefaultDuration)].Value;
                _defaultDuration = int.Parse(DefaultDurationresult);
            }
            catch (Exception) {
                _defaultDuration = 0;
                AddUpdateAppSettings(nameof(DefaultDuration), _defaultDuration.ToString());
                Console.WriteLine("Error reading _defaultDuration app settings");
            }

            try {
                string Iconresult = appSettings[nameof(Icon)].Value;
                switch (Iconresult) {
                    case "Mug":
                        _icon = TrayIcon.Mug;
                        break;
                    case "EyeWithZzz":
                        _icon = TrayIcon.EyeWithZzz;
                        break;
                    default:
                        _icon = TrayIcon.Default;
                        break;
                }
            }
            catch (Exception) {
                _icon = TrayIcon.Default;
                AddUpdateAppSettings(nameof(Icon), _icon.ToString());
                Console.WriteLine($"Error reading Icon app settings");
            }

            try {
                string Durationsresult = appSettings[nameof(Durations)].Value;
                List<string> splitResult = Durationsresult.Split(',').ToList();

                _durations = new ObservableCollection<int>();
                foreach (string item in splitResult) {
                    _durations.Add(int.Parse(item));
                }
            }
            catch (Exception) {
                _durations = new ObservableCollection<int> { 0, 15, 60, 120, 480 };
                AddUpdateAppSettings(nameof(Durations), string.Join(',', _durations.ToArray()));
                Console.WriteLine($"Error reading Durations app settings");
            }
            Durations.CollectionChanged += Durations_CollectionChanged;

            configFile.Save(ConfigurationSaveMode.Modified);
        }

        private void Durations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            appSettings[nameof(Durations)].Value = string.Join(',', Durations.ToArray());
            configFile.Save(ConfigurationSaveMode.Modified);
        }

        void AddUpdateAppSettings(string key, string value) {
            try {
                if (appSettings[key] == null) {
                    appSettings.Add(key, value);
                }
                else {
                    appSettings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException) {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
