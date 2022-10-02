using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Media.Imaging;
using System.IO;
using StudentsHelpTerminal.Models;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using StudentsHelpTerminal.Infrastructure.Commands;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {
        #region CurrentViewModel property
        private Base.ViewModelBase _CurrentViewModel;
        public Base.ViewModelBase CurrentViewModel { 
            get { return _CurrentViewModel; }
            set { Set(ref _CurrentViewModel, value); }
        }
        #endregion
        public MainWindowViewModel()
        {
            CurrentViewModel = new MainPageViewModel();
        }
    }
}
