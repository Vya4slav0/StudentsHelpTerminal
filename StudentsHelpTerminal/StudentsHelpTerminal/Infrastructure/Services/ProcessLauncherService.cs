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
                string unexistableFiles =
                    (!appSettingsFile.Exists ? appSettingsFile.FullName + '\n' : "") +
                    (!settingsEditor.Exists ? settingsEditor.FullName : "");
                ErrorHandlerService.FileNotExist(unexistableFiles);
                return;
            }
            ProcessStartInfo settingsEditorStartInfo = new ProcessStartInfo();
            settingsEditorStartInfo.FileName = settingsEditor.FullName;
            settingsEditorStartInfo.Arguments = $"\"{appSettingsFile.FullName}\"";
            Process.Start(settingsEditorStartInfo);
        }

        public static void StartApplicantGuide()
        {
            FileInfo applicantGuideApp = new FileInfo(SettingsInteractor.GetSettingValueByName("PathToApplicantGuide"));
            if (!applicantGuideApp.Exists)
            {
                ErrorHandlerService.FileNotExist(applicantGuideApp.FullName);
                return;
            }
            Process.Start(applicantGuideApp.FullName);
        }
    }
}
