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
        private ViewModelBase _CurrentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set 
            { 
                _CurrentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action CurrentViewModelChanged;
    }
}
