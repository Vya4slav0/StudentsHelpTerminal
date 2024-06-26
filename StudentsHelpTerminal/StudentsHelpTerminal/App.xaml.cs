using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.Views.Windows;
using System;
using System.Linq;
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
            if (!e.Args.Contains("debugWithoutCard"))
                IOPortsStore.Initialize();
            else
            {
                try
                {
                    long cardId = Convert.ToInt64(e.Args[e.Args.ToList().IndexOf("debugWithoutCard") + 1]);
                    NavigationStore.CurrentViewModel = new MainPageViewModel(cardId);
                }
                catch (Exception ex)
                {
                    new DialogBoxes.AlertBox("Похоже, господа Вы обосрались в аргументах: \n" + ex.Message) { Topmost = true }.ShowDialog();
                    App.Current.Shutdown();
                }
            }
            TaskbarService.Hide();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            TaskbarService.Show();
            Logger.CloseLogger();
            base.OnExit(e);
        }
    }
}
