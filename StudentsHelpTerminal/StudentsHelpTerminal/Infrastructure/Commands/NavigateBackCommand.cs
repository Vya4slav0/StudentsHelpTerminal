using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.Infrastructure.Commands
{
    internal class NavigateBackCommand : Base.CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Predicate<object> _canExecute;

        public NavigateBackCommand(NavigationStore navigationStore, Predicate<object> canExecute = null)
        {
            _navigationStore = navigationStore;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _navigationStore.PrevViewModel;
        }
    }
}
