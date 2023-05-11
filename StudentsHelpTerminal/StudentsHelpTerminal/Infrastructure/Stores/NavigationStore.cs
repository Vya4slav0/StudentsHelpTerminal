using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal static class NavigationStore
    {
        public static IdlePageViewModel CurrentIdlePageViewModel { get; set; }

        #region PrevViewModel

        //History of view models
        private static Stack<ViewModelBase> _NavigationStack = new Stack<ViewModelBase>();

        public static bool PrevViewModelAvailable => _NavigationStack.Count > 0;

        public static ViewModelBase PrevViewModel 
        {
            get { return _NavigationStack.Pop(); }
            set { _NavigationStack.Push(value); }
        }

        #endregion

        #region CurrentViewModel

        private static ViewModelBase _CurrentViewModel;

        public static ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            {
                PrevViewModel = _CurrentViewModel;
                _CurrentViewModel = value;
                CurrentViewModelChanged?.Invoke(CurrentViewModel);
            }
        }

        #endregion

        // Contains new view model
        public static event Action<ViewModelBase> CurrentViewModelChanged;
    }
}
