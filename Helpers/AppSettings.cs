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

        private bool _isFirstLaunch;

        public bool IsFirstLaunch
        {
            get { return _isFirstLaunch; }
            set {
                _isFirstLaunch = value;
                localSettings.Values[nameof(IsFirstLaunch)] = _isFirstLaunch.ToString();
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
            ActivateOnLaunchSetting ??= "false";
            _activateOnLaunch = bool.Parse(ActivateOnLaunchSetting);

            string IsFirstLaunchSetting = (string)localSettings.Values[nameof(IsFirstLaunch)];
            IsFirstLaunchSetting ??= "true";
            _isFirstLaunch = bool.Parse(IsFirstLaunchSetting);
            
            string AutomaticallyLaunchWithWindowsSetting = (string)localSettings.Values[nameof(AutomaticallyLaunchWithWindows)];
            AutomaticallyLaunchWithWindowsSetting ??= "false";
            _automaticallyLaunchWithWindows = bool.Parse(AutomaticallyLaunchWithWindowsSetting);
            
            string ShowMessageOnLaunchSetting = (string)localSettings.Values[nameof(ShowMessageOnLaunch)];
            ShowMessageOnLaunchSetting ??= "false";
            _showMessageOnLaunch = bool.Parse(ShowMessageOnLaunchSetting);
            
            string DefaultDurationSetting = (string)localSettings.Values[nameof(DefaultDuration)];
            DefaultDurationSetting ??= "480";
            _defaultDuration = int.Parse(DefaultDurationSetting);
            
            string IconSetting = (string)localSettings.Values[nameof(Icon)];
            IconSetting ??= "Mug";

            _icon = IconSetting switch
            {
                "Mug" => TrayIcon.Mug,
                "EyeWithZzz" => TrayIcon.EyeWithZzz,
                _ => TrayIcon.Default,
            };
            string DurationsSetting = (string)localSettings.Values[nameof(Durations)];
            DurationsSetting ??= "0,15,60,120,480";
            List<string> splitResult = DurationsSetting.Split(',').ToList();

            _durations = new ObservableCollection<int>();
            foreach (string item in splitResult) {
                _durations.Add(int.Parse(item));
            }
            Durations.CollectionChanged += Durations_CollectionChanged;
        }

        private void Durations_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            localSettings.Values[nameof(Durations)] = string.Join(',', Durations.ToArray());
        }
    }
}
