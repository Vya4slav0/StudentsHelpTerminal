using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsEditor.Infrastructure.Services;
using SettingsEditor.Models;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class SettingsInteractor
    {
        public static readonly string PathToSettingsFile = "./AppSettings.xml";

        private static SettingsManagerService _settingsManager = new SettingsManagerService(PathToSettingsFile);

        public static string GetSettingValueByName(string settingName)
        {
            Setting setting = _settingsManager.LoadSettingByName(settingName);
            return setting.Value?.ToString() ?? string.Empty;
        }

        public static int GetSettingIntValueByName(string settingName)
        {
            Setting setting = _settingsManager.LoadSettingByName(settingName);
            return Convert.ToInt32(setting.Value);
        }
    }
}
