using DialogBoxes;
using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.ViewModels;
using System;
using System.IO;
using System.IO.Ports;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal static class IOPortsStore
    {
        #region Card reader COM port

        private static readonly string _COMName = StudentsHelpTerminal.Properties.Settings.Default.CardReaderPortName;
        private static readonly int _COMSpeed = StudentsHelpTerminal.Properties.Settings.Default.CardReaderPortBaudRate;
        private static readonly SerialPort _cardReaderSerialPort = new SerialPort(_COMName, _COMSpeed);

        public static SerialPort CardReaderSerialPort
        {
            get { return _cardReaderSerialPort; }
        }

        #endregion

        static IOPortsStore()
        {
            IOPortsOpen();
        }

        public static void Initialize()
        {
            CardReaderSerialPort.DataReceived += (s, a) =>
            {
                long cardId = Convert.ToInt64(CardReaderSerialPort.ReadLine());

                if (!(NavigationStore.CurrentViewModel is IdlePageViewModel) && DBHelper.HasStudent(cardId)) return;
                NavigationStore.CurrentViewModel = new MainPageViewModel(cardId);
                CardReaderSerialPort.DiscardInBuffer();
            };
        }

        private static void IOPortsOpen()
        {
            try 
            { 
                CardReaderSerialPort.Open();
                CardReaderSerialPort.DiscardInBuffer();  
            }
            catch (IOException ex)
            {
                string message = $"Порт {_COMName} не доступен. Причина: {ex.Message}"; 
                if (SerialPort.GetPortNames().Length > 0)
                    message += $"\nОбнаружены порты: {string.Join(", ", SerialPort.GetPortNames())}";
                message += "\nОткрыть панель администратора?";
                if (new YesNoBox(message).ShowDialog() &&
                    AdminAutorizer.AutorizeAnyAdministrator(new PromptBox("Введите ваш пароль администратора").ShowDialog()))
                {

                    NavigationStore.CurrentViewModel = new AdminPageViewModel();
                    return;
                }
                App.Current.Shutdown();
                return;
            }
        }
    }
}
