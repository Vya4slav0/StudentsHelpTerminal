using StudentsHelpTerminal.Infrastructure.Stores;
using System;

namespace StudentsHelpTerminal.Infrastructure.Commands
{
    internal class NavigateBackCommand : Base.CommandBase
    {
        private readonly Predicate<object> _canExecute;

        public NavigateBackCommand(Predicate<object> canExecute = null)
        {
            _canExecute = canExecute;
        }  

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? NavigationStore.PrevViewModelAvailable;

        public override void Execute(object parameter)
        {
            NavigationStore.CurrentViewModel = NavigationStore.PrevViewModel;
        }
    }
}
