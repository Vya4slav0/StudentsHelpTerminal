using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.ViewModels.Base;
using System;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal static class NavigationStore
    {
        public static IdlePageViewModel CurrentIdlePageViewModel { get; set; }
        public static ViewModelBase PrevViewModel { get; private set; }

        private static ViewModelBase _CurrentViewModel;

        public static ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            {
                PrevViewModel = _CurrentViewModel;
                _CurrentViewModel = value;
                CurrentViewModelChanged?.Invoke(_CurrentViewModel);
            }
        }

        // Contains new view model
        public static event Action<ViewModelBase> CurrentViewModelChanged;
    }
}
