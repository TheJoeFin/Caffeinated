using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.Storage;

namespace Caffeinated.Helpers { 
    public class AppSettings {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private bool _activateOnLaunch;

        public bool ActivateOnLaunch {
            get { return _activateOnLaunch; }
            set { 
                _activateOnLaunch = value;
                localSettings.Values[nameof(ActivateOnLaunch)] = _activateOnLaunch.ToString();
            }
        }

        private bool _automaticallyLaunchWithWindows;

        public bool AutomaticallyLaunchWithWindows {
            get { return _automaticallyLaunchWithWindows; }
            set { 
                _automaticallyLaunchWithWindows = value;
                localSettings.Values[nameof(AutomaticallyLaunchWithWindows)] = _automaticallyLaunchWithWindows.ToString();
            }
        }

        private bool _showMessageOnLaunch;

        public bool ShowMessageOnLaunch {
            get { return _showMessageOnLaunch; }
            set { 
                _showMessageOnLaunch = value;
                localSettings.Values[nameof(ShowMessageOnLaunch)] = _showMessageOnLaunch.ToString();
            }
        }

        private int _defaultDuration;

        public int DefaultDuration {
            get { return _defaultDuration; }
            set { 
                _defaultDuration = value;
                localSettings.Values[nameof(DefaultDuration)] = _defaultDuration.ToString();
            }
        }

        private TrayIcon _icon;

        public TrayIcon Icon {
            get { return _icon; }
            set { 
                _icon = value;
                localSettings.Values[nameof(Icon)] = _icon.ToString();
            }
        }

        private ObservableCollection<int> _durations;

        public ObservableCollection<int> Durations {
            get { return _durations; }
            set { 
                _durations = value;
                localSettings.Values[nameof(Durations)] = string.Join(',',Durations.ToArray());
            }
        }

        public AppSettings() {

            string ActivateOnLaunchSetting = (string)localSettings.Values[nameof(ActivateOnLaunch)];
            if (ActivateOnLaunchSetting == null)
                ActivateOnLaunchSetting = "false";
            _activateOnLaunch = bool.Parse(ActivateOnLaunchSetting);
            
            string AutomaticallyLaunchWithWindowsSetting = (string)localSettings.Values[nameof(AutomaticallyLaunchWithWindows)];
            if (AutomaticallyLaunchWithWindowsSetting == null)
                AutomaticallyLaunchWithWindowsSetting = "false";
            _automaticallyLaunchWithWindows = bool.Parse(AutomaticallyLaunchWithWindowsSetting);
            
            string ShowMessageOnLaunchSetting = (string)localSettings.Values[nameof(ShowMessageOnLaunch)];
            if (ShowMessageOnLaunchSetting == null)
                ShowMessageOnLaunchSetting = "true";
            _showMessageOnLaunch = bool.Parse(ShowMessageOnLaunchSetting);
            
            string DefaultDurationSetting = (string)localSettings.Values[nameof(DefaultDuration)];
            if (DefaultDurationSetting == null)
                DefaultDurationSetting = "0";
            _defaultDuration = int.Parse(DefaultDurationSetting);
            
            string IconSetting = (string)localSettings.Values[nameof(Icon)];
            if (IconSetting == null)
                IconSetting = "default";
            
            switch (IconSetting) {
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

            string DurationsSetting = (string)localSettings.Values[nameof(Durations)];
            if (DurationsSetting == null)
                DurationsSetting = "0,15,60,120,480";
            List<string> splitResult = DurationsSetting.Split(',').ToList();

            _durations = new ObservableCollection<int>();
            foreach (string item in splitResult) {
                _durations.Add(int.Parse(item));
            }
            Durations.CollectionChanged += Durations_CollectionChanged;
        }

        private void Durations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            localSettings.Values[nameof(Durations)] = string.Join(',', Durations.ToArray());
        }
    }
}
