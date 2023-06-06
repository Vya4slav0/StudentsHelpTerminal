using DialogBoxes;
using StudentsHelpTerminal.ViewModels;
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
            CardReaderSerialPort.DiscardInBuffer();
        }

        private static void IOPortsOpen()
        {
            try { CardReaderSerialPort.Open(); }
            catch (IOException)
            {
                string message = $"Порт {_COMName} не доступен."; 
                if (SerialPort.GetPortNames().Length > 0)
                    message += $"\nОбнаружены порты: {string.Join(", ", SerialPort.GetPortNames())}";
                message += "\nОткрыть панель администратора?";
                if (new YesNoBox(message).ShowDialog())
                {
                    //TODO: make admin autorization
                    NavigationStore.CurrentViewModel = new AdminPageViewModel();
                    return;
                }
                App.Current.Shutdown();
            }
        }
    }
}
