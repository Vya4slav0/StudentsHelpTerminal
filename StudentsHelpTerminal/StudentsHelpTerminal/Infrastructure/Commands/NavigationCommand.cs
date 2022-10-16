using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.Infrastructure.Commands
{
    internal class NavigationCommand : Base.CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModels.Base.ViewModelBase _viewModelToShow;
        Predicate<object> _canExecute;

        public NavigationCommand(NavigationStore navigationStore, ViewModels.Base.ViewModelBase viewModelToShow, Predicate<object> canExecute = null)
        {
            _navigationStore = navigationStore;
            _viewModelToShow = viewModelToShow;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _viewModelToShow;
        }
    }
}
