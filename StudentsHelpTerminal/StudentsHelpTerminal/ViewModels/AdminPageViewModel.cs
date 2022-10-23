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
using System.Data.Entity;

namespace StudentsHelpTerminal.ViewModels
{
    internal class AdminPageViewModel : Base.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public AdminPageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnAdminPageShow;
            BackToProfilePageCommand = new NavigateBackCommand(_navigationStore);
            CloseAppCommand = new RelayCommand(_closeAppCommandExecute);
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

        private async void OnAdminPageShow(Base.ViewModelBase currentVM)
        {
            if (currentVM is AdminPageViewModel)
            {
                await LoadTablesAsync();
            }
        }

        private async Task LoadTablesAsync()
        {
            if (DBTables != null) return;
            using (StudentsDBContext db = new StudentsDBContext())
            {
                DBTables = new List<Table>()
                {
                    new Table() { Name = "STAFF", Items = await db.STAFFs.AsNoTracking().ToArrayAsync() },
                    new Table() { Name = "STAFF_REF", Items = await db.STAFF_REF.AsNoTracking().ToArrayAsync() },
                    new Table() { Name = "STAFF_CARDS", Items = await db.STAFF_CARDS.AsNoTracking().ToArrayAsync() },
                    new Table() { Name = "SUBDIV_REF", Items = await db.SUBDIV_REF.AsNoTracking().ToArrayAsync() },
                    new Table() { Name = "STOP_CARDS", Items = await db.STOP_CARDS_DESCRIPTION.AsNoTracking().ToArrayAsync() }
                };
            }
            OnPropertyChanged(nameof(DBTables));
            SelectedTable = DBTables.First();
        }
    }
}
