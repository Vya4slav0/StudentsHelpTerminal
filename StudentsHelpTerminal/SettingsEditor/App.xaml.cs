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
            string pathToSettingsFile = @"E:\Users\vya4s\Documents\Visual Studio projects\StudentsHelpTerminal\StudentsHelpTerminal\StudentsHelpTerminal\Settings.xml";
            MainWindow = new MainWindow();
            MainWindow.DataContext = new SettingsPageViewModel(pathToSettingsFile);
            MainWindow.Content = new SettingsPage();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
