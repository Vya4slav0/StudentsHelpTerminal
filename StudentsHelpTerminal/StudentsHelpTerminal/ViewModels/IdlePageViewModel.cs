using StudentsHelpTerminal.Infrastructure.Stores;
using System;

namespace StudentsHelpTerminal.ViewModels
{
    internal class IdlePageViewModel : Base.ViewModelBase
    {
        static IdlePageViewModel()
        {
            IOPortsStore.CardReaderSerialPort.DataReceived += (s, a) =>
            {
                if (!(NavigationStore.CurrentViewModel is IdlePageViewModel)) return;
                NavigationStore.CurrentViewModel = new MainPageViewModel
                    (
                        Convert.ToInt32(IOPortsStore.CardReaderSerialPort.ReadLine())
                    );
                IOPortsStore.CardReaderSerialPort.DiscardInBuffer();
            };
        }
    }
}
