using DialogBoxes;
using System.IO.Ports;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class ErrorHandlerService
    {
        public static void CardReaderUnableHandle(string unablePortName, string[] discoveredPorts, string unableReason)
        {
            string message = $"Порт {unablePortName} не доступен. Причина: {unableReason}";
            if (SerialPort.GetPortNames().Length > 0)
                message += $"\nОбнаружены порты: {string.Join(", ", discoveredPorts)}";
            message += "\nОткрыть окно параметров приложения?";
            if (new YesNoBox(message).ShowDialog() &&
                AdminAutorizer.AutorizeAnyAdministrator(new PromptBox("Введите ваш пароль администратора").ShowDialog()))
            {
                ProcessLauncherService.StartSettingsEditor();
                return;
            }
            App.Current.Shutdown();
            return;
        }

        public static void FileNotExist(string fileName, bool shutdownApp = false)
        {
            new AlertBox("Вы пытаетесь открыть то, чего нет:\n" +  fileName).ShowDialog();
            if (shutdownApp) 
                App.Current.Shutdown();
        }
    }
}
