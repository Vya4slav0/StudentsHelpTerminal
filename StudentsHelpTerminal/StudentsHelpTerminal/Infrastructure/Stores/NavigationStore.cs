using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsHelpTerminal.ViewModels.Base;

namespace StudentsHelpTerminal.Infrastructure.Stores
{
    internal class NavigationStore
    {
        public ViewModelBase PrevViewModel { get; private set; }

        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            {
                PrevViewModel = _CurrentViewModel;
                _CurrentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action CurrentViewModelChanged;
    }
}
