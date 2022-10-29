using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsHelpTerminal.ViewModels.Base;
using StudentsHelpTerminal.ViewModels;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal class NavigationStore
    {
        public IdlePageViewModel CurrentIdlePageViewModel { get; set; }
        public ViewModelBase PrevViewModel { get; private set; }

        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
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
        public event Action<ViewModelBase> CurrentViewModelChanged;
    }
}
