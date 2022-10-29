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
        private readonly NavigationStore _navigationStore;
        private readonly IOPortsStore _IOPortsStore;

        public IdlePageViewModel(NavigationStore navigationStore, IOPortsStore portsStore)
        {
            _navigationStore = navigationStore;
            _IOPortsStore = portsStore;
            _IOPortsStore.CardReaderSerialPort.DataReceived += CardReaderSerialPort_DataReceived;
        }

        private void CardReaderSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!(_navigationStore.CurrentViewModel is IdlePageViewModel)) return;
            _navigationStore.CurrentViewModel = new MainPageViewModel
                (
                    _navigationStore,
                    Convert.ToInt32(_IOPortsStore.CardReaderSerialPort.ReadLine())
                );
        }
    }
}
