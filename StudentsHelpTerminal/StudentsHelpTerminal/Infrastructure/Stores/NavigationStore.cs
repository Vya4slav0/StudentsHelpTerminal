using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal static class NavigationStore
    {
        static NavigationStore()
        {
            //Initialize navigation
            IdlePageViewModel idlePageViewModel = new IdlePageViewModel();
            _CurrentViewModel = idlePageViewModel;
            CurrentIdlePageViewModel = idlePageViewModel;
        }

        public static IdlePageViewModel CurrentIdlePageViewModel { get; }

        #region PrevViewModel

        //History of view models
        private static Stack<ViewModelBase> _NavigationStack = new Stack<ViewModelBase>();

        public static bool PrevViewModelAvailable => _NavigationStack.Count > 0;

        public static ViewModelBase PrevViewModel 
        {
            get { return _NavigationStack.Peek(); }
        }

        #endregion

        #region CurrentViewModel

        private static ViewModelBase _CurrentViewModel;

        public static ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            {
                if (PrevViewModelAvailable && _NavigationStack.Contains(value)) 
                    _NavigationStack.Pop();
                else if (_CurrentViewModel != null)
                    _NavigationStack.Push(_CurrentViewModel);

                _CurrentViewModel = value;
                CurrentViewModelChanged?.Invoke(CurrentViewModel);
            }
        }

        #endregion

        // Contains new view model
        public static event Action<ViewModelBase> CurrentViewModelChanged;
    }
}
