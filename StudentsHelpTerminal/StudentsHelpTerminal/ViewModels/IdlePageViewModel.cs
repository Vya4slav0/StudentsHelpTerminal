using StudentsHelpTerminal.Infrastructure.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.ViewModels
{
    internal class IdlePageViewModel : Base.ViewModelBase
    {
        public IdlePageViewModel()
        {
            IOPortsStore.IOPortsOpen();
            IOPortsStore.CardReaderSerialPort.DataReceived += CardReaderSerialPort_DataReceived;
        }

        private void CardReaderSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!(NavigationStore.CurrentViewModel is IdlePageViewModel)) return;
            NavigationStore.CurrentViewModel = new MainPageViewModel
                (
                    Convert.ToInt32(IOPortsStore.CardReaderSerialPort.ReadLine())
                );
            IOPortsStore.CardReaderSerialPort.DiscardInBuffer();
        }
    }
}
