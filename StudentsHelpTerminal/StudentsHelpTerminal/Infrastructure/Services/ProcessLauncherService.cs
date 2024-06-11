using System.Diagnostics;
using System.IO;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class ProcessLauncherService
    {
        public static void StartSettingsEditor()
        {
            FileInfo appSettingsFile = new FileInfo("./AppSettings.xml");
            FileInfo settingsEditor = new FileInfo("./SettingsEditor.exe");
            if (!(appSettingsFile.Exists && settingsEditor.Exists))
            {
                return;
            }
            ProcessStartInfo settingsEditorStartInfo = new ProcessStartInfo();
            settingsEditorStartInfo.FileName = settingsEditor.FullName;
            settingsEditorStartInfo.Arguments = $"\"{appSettingsFile.FullName}\"";
            Process.Start(settingsEditorStartInfo);
        }
    }
}
