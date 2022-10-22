using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Infrastructure.Commands;

namespace StudentsHelpTerminal.ViewModels
{
    internal class AdminPageViewModel : Base.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public AdminPageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            BackToProfilePageCommand = new NavigateBackCommand(_navigationStore);
            CloseAppCommand = new RelayCommand(_closeAppCommandExecute);
        }

        #region Properties

        #endregion

        #region Commands

        public ICommand BackToProfilePageCommand { get; }

        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void _closeAppCommandExecute(object p) => App.Current.Shutdown();
        #endregion

        #endregion
    }
}
