using StudentsHelpTerminal.Infrastructure.Stores;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {

        #region CurrentViewModel property

        public Base.ViewModelBase CurrentViewModel { 
            get { return NavigationStore.CurrentViewModel; }
        }
        #endregion
        public MainWindowViewModel()
        {
            NavigationStore.CurrentViewModelChanged += (vm) => { OnPropertyChanged(nameof(CurrentViewModel)); };
        }
    }
}
