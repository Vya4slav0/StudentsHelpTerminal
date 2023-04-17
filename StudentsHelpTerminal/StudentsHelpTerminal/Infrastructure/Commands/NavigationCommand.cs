using StudentsHelpTerminal.Infrastructure.Stores;
using System;

namespace StudentsHelpTerminal.Infrastructure.Commands
{
    internal class NavigationCommand : Base.CommandBase
    {
        private readonly ViewModels.Base.ViewModelBase _viewModelToShow;
        Predicate<object> _canExecute;

        public NavigationCommand(ViewModels.Base.ViewModelBase viewModelToShow, Predicate<object> canExecute = null)
        {
            _viewModelToShow = viewModelToShow;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            NavigationStore.CurrentViewModel = _viewModelToShow;
        }
    }
}
