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
using System.ComponentModel;
using StudentsHelpTerminal.Infrastructure.Stores;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {
        private NavigationStore _navigationStore ;

        #region CurrentViewModel property

        public Base.ViewModelBase CurrentViewModel { 
            get { return _navigationStore.CurrentViewModel; }
        }
        #endregion
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += (vm) => { OnPropertyChanged(nameof(CurrentViewModel)); };
        }
    }
}
