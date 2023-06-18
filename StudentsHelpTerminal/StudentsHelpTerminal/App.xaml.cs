using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.Views.Windows;
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
            int errorCode = SelfTestingService.AllFunctionsCheck();
            if(errorCode != 0)
            {
                Application.Current.Shutdown(errorCode);
                return;
            };

            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
            MainWindow.Show();
            NavigationStore.CurrentViewModel = new IdlePageViewModel();
            IOPortsStore.Initialize(); 

            base.OnStartup(e);
        }
    }
}
