using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caffeinated;

namespace Caffeinated.Helpers
{
    public class AppSettings
    {
        NameValueCollection appSettings = ConfigurationManager.AppSettings;

        private bool _activateOnLaunch;

        public bool ActivateOnLaunch
        {
            get { return _activateOnLaunch; }
            set 
            { 
                _activateOnLaunch = value;
                appSettings[nameof(ActivateOnLaunch)] = _activateOnLaunch.ToString();
            }
        }

        private bool _automaticallyLaunchWithWindows;

        public bool AutomaticallyLaunchWithWindows
        {
            get { return _automaticallyLaunchWithWindows; }
            set 
            { 
                _automaticallyLaunchWithWindows = value;
                appSettings[nameof(AutomaticallyLaunchWithWindows)] = _automaticallyLaunchWithWindows.ToString();
            }
        }


        private bool _showMessageOnLaunch;

        public bool ShowMessageOnLaunch
        {
            get { return _showMessageOnLaunch; }
            set 
            { 
                _showMessageOnLaunch = value;
                appSettings[nameof(ShowMessageOnLaunch)] = _showMessageOnLaunch.ToString();
            }
        }


        private int _defaultDuration;

        public int DefaultDuration
        {
            get { return _defaultDuration; }
            set 
            { 
                _defaultDuration = value; 
                appSettings[nameof(DefaultDuration)] = _defaultDuration.ToString();
            }
        }


        private TrayIcon _icon;

        public TrayIcon Icon
        {
            get { return _icon; }
            set 
            { 
                _icon = value; 
                appSettings[nameof(Icon)] = _icon.ToString();
            }
        }


        private List<int> _durations;

        public List<int> Durations
        {
            get { return _durations; }
            set 
            { 
                _durations = value;
                appSettings[nameof(Durations)] = string.Join(',',Durations.ToArray());
            }
        }


        public AppSettings()
        {
            try 
            {
                string ActivateOnLaunchresult = appSettings[nameof(ActivateOnLaunch)];
                _activateOnLaunch = bool.Parse(ActivateOnLaunchresult);
            }
            catch (ConfigurationErrorsException)
            {
                _activateOnLaunch = false;
                Console.WriteLine($"Error reading _activateOnLaunch app settings");
            }

            try
            {
                string AutomaticallyLaunchWithWindowsresult = appSettings[nameof(AutomaticallyLaunchWithWindows)];
                _automaticallyLaunchWithWindows = bool.Parse(AutomaticallyLaunchWithWindowsresult);
            }
            catch (ConfigurationErrorsException)
            {
                _automaticallyLaunchWithWindows = false;
                Console.WriteLine("Error reading _automaticallyLaunchWithWindows app settings");
            }

            try
            {
                string ShowMessageOnLaunchresult = appSettings[nameof(ShowMessageOnLaunch)];
                _showMessageOnLaunch = bool.Parse(ShowMessageOnLaunchresult);
            }
            catch (ConfigurationErrorsException)
            {
                _showMessageOnLaunch = true;
                Console.WriteLine($"Error reading _showMessageOnLaunch app settings");
            }

            try
            {
                string DefaultDurationresult = appSettings[nameof(DefaultDuration)];
                _defaultDuration = int.Parse(DefaultDurationresult);
            }
            catch (ConfigurationErrorsException)
            {
                _defaultDuration = 0;
                Console.WriteLine("Error reading _defaultDuration app settings");
            }

            try
            {
                string Iconresult = appSettings[nameof(Icon)];

                switch (Iconresult)
                {
                    case "Mug":
                        _icon = TrayIcon.Mug;
                        break;
                    case "Eye-ZZZ":
                        _icon = TrayIcon.EyeWithZzz;
                        break;
                    default:
                        _icon = TrayIcon.Default;
                        break;
                }
            }
            catch (ConfigurationErrorsException)
            {
                _icon = TrayIcon.Default;
                Console.WriteLine($"Error reading Icon app settings");
            }

            try
            {
                string Durationsresult = appSettings[nameof(Durations)];
                List<string> splitResult = Durationsresult.Split(',').ToList();

                _durations = new List<int>();
                foreach (string item in splitResult)
                {
                    _durations.Add(int.Parse(item));
                }
            }
            catch (ConfigurationErrorsException)
            {
                _durations = new List<int> { 0, 15, 60, 120, 480 };
                Console.WriteLine($"Error reading Durations app settings");
            }
        }
    }
}
