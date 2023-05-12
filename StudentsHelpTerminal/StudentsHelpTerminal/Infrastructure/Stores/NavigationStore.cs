using StudentsHelpTerminal.ViewModels;
using StudentsHelpTerminal.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal static class NavigationStore
    {

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

        private static ViewModelBase _CurrentViewModel = new IdlePageViewModel();

        public static ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            {
                if (value is null) return;
                if (value is IdlePageViewModel)
                    _NavigationStack.Clear();       
                else if (PrevViewModelAvailable && _NavigationStack.Contains(value)) 
                    _NavigationStack.Pop();
                else
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
