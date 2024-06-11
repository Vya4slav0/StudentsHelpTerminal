using DialogBoxes;
using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.ViewModels;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;

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

                if (!(NavigationStore.CurrentViewModel is IdlePageViewModel && DBHelper.HasStudent(cardId)))
                    return;
                NavigationStore.CurrentViewModel = new MainPageViewModel(cardId);
                CardReaderSerialPort.DiscardInBuffer();
            };
        }

        public static bool CheckPorts(string portName = null)
        {
            if (portName == null) 
                return SerialPort.GetPortNames().Contains(CardReaderSerialPort.PortName);
            return SerialPort.GetPortNames().Contains(portName);
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
                ErrorHandlerService.CardReaderUnableHandle(_COMName, SerialPort.GetPortNames(), ex.Message);
            }
        }
    }
}
