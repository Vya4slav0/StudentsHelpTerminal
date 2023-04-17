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
        App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IdlePageViewModel idlePageViewModel = new IdlePageViewModel();
            NavigationStore.CurrentViewModel = idlePageViewModel;
            NavigationStore.CurrentIdlePageViewModel = idlePageViewModel;
            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
