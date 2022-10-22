using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Models;

namespace StudentsHelpTerminal.ViewModels
{
    internal class AdminPageViewModel : Base.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public AdminPageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            BackToProfilePageCommand = new NavigateBackCommand(_navigationStore);
            CloseAppCommand = new RelayCommand(_closeAppCommandExecute);
            LoadDBTables();
        }

        #region Properties
        public List<Table> DBTables { get; private set; }

        #region SelectedTable

        private Table _selectedTable;

        public Table SelectedTable
        {
            get { return _selectedTable; }
            set { Set(ref _selectedTable, value); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand BackToProfilePageCommand { get; }

        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void _closeAppCommandExecute(object p) => App.Current.Shutdown();
        #endregion

        #endregion

        private void LoadDBTables()
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                Table staff = new Table() { Name = "STAFF", Items = db.STAFFs.ToArray()};
                Table staffRef = new Table() { Name = "STAFF_REF", Items = db.STAFF_REF.ToArray()};

                DBTables = new List<Table>()
                {
                    staff, staffRef
                };
            }
        }
    }
}
