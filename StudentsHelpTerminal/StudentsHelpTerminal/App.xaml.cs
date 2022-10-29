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

namespace StudentsHelpTerminal
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly IOPortsStore _ioPortsStore;

        App()
        {
            _navigationStore = new NavigationStore();
            _ioPortsStore = new IOPortsStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IdlePageViewModel idlePageViewModel = new IdlePageViewModel(_navigationStore, _ioPortsStore);
            _navigationStore.CurrentViewModel = idlePageViewModel;
            _navigationStore.CurrentIdlePageViewModel = idlePageViewModel;
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel(_navigationStore) };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
