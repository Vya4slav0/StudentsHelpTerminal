using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentsHelpTerminal
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
            NavigationStore.CurrentViewModel = new IdlePageViewModel();
            IOPortsStore.CardReaderSerialPort.DataReceived += (s, a) =>
            {
                long cardId = Convert.ToInt64(IOPortsStore.CardReaderSerialPort.ReadLine());

                if (!(NavigationStore.CurrentViewModel is IdlePageViewModel) && DBHelper.HasStudent(cardId)) return;
                NavigationStore.CurrentViewModel = new MainPageViewModel(cardId);
                IOPortsStore.CardReaderSerialPort.DiscardInBuffer();
            };
            base.OnStartup(e);
        }
    }
}
