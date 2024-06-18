using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SettingsEditor.ViewModels;
using SettingsEditor.Views;

namespace SettingsEditor
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            if (e.Args.Length > 0)
            {
                string pathToSettingsFile = e.Args[0];
                MainWindow.DataContext = new SettingsPageViewModel(pathToSettingsFile);
            }
            else
            {
                MainWindow.DataContext = new SettingsPageViewModel();
            }
            
            //MainWindow.Content = new SettingsPage();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
