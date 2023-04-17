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

        public static void IOPortsOpen()
        {
            try { CardReaderSerialPort.Open(); }
            catch (IOException)
            {
                //TODO: make port is not connected alert
                #if !DEBUG
                
                #endif
            }
        }
    }
}
