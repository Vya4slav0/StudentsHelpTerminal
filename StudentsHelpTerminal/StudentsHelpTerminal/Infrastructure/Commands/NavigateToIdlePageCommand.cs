using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.ViewModels;

namespace StudentsHelpTerminal.Infrastructure.Commands
{
    internal class NavigateToIdlePageCommand : Base.CommandBase
    {
        public NavigateToIdlePageCommand() { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            NavigationStore.CurrentViewModel = new IdlePageViewModel();
        }
    }
}
