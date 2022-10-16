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
        App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MainPageViewModel(_navigationStore);
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel(_navigationStore) };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
