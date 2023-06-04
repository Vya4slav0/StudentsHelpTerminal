using StudentsHelpTerminal.ViewModels.Base;
using StudentsHelpTerminal.Views.Windows;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class ShowInToolWindowService
    {
        public static void ShowViewByViewModel(ViewModelBase viewModel)
        {
            new ToolWindow() { Content = viewModel }.Show();
        }
    }
}
