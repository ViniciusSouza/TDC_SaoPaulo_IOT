using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC2014WP
{
    class SettingsManager
    {
        public void SetValue(string settingName, DateTime value)
        {
            settingName = settingName.ToLower();
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                IsolatedStorageSettings.ApplicationSettings[settingName] = value;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(settingName, value);
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public DateTime GetValue(string settingName, DateTime defaultValue)
        {
            settingName = settingName.ToLower();
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                return (DateTime)IsolatedStorageSettings.ApplicationSettings[settingName];
            }
            else
            {
                return defaultValue;
            }
        }

    }
}
