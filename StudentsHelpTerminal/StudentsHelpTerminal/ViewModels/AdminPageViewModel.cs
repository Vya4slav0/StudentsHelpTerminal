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
using System.Diagnostics;
using System.IO;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Controls;

namespace StudentsHelpTerminal.ViewModels
{
    internal class AdminPageViewModel : Base.ViewModelBase
    {
        public AdminPageViewModel()
        {
            NavigationStore.CurrentViewModelChanged += OnAdminPageShow;

            BackToProfilePageCommand = new NavigateBackCommand();
            CloseAppCommand = new RelayCommand(CloseAppCommandExecute);
            OpenConfigFileCommand = new RelayCommand(OpenConfigFileCommandExecute);
            OpenUsersLogCommand = new RelayCommand(OpenUsersLogCommandExecute);
            ClearUsersLogCommand = new RelayCommand(ClearUsersLogCommandExecute, ClearUsersLogCommandCanExecute);

            SearchCommand = new RelayCommand(SearchCommandExecute);
        }

        #region Properties
        public List<Table> DBTables { get; private set; }

        #region Sorting

        private DataGridTextColumn _SortBy;

        public DataGridTextColumn SortBy
        {
            get { return _SortBy; }
            set { if(Set(ref _SortBy, value)) EnableSort = EnableSort; }
        }

        private bool _ReverseSort;

        public bool ReverseSort 
        { 
            get { return _ReverseSort; }
            set { if(Set(ref _ReverseSort, value)) EnableSort = EnableSort; } 
        }

        private bool _EnableSort;
        
        public bool EnableSort 
        { 
            get { return _EnableSort; }
            set
            {
                Set(ref _EnableSort, value);
                if (SelectedTableView != null) SelectedTableView.SortDescriptions.Clear();
                if (value)
                SelectedTableView.SortDescriptions.Add(
                    new SortDescription(SortBy.Header.ToString(), ReverseSort ? ListSortDirection.Descending : ListSortDirection.Ascending));
            } 
        }

        #endregion

        #region Searching

        public string SearchQuery { get; set; }

        #endregion

        #region SelectedTableView

        private ICollectionView _SelectedTableView;

        public ICollectionView SelectedTableView
        {
            get { return _SelectedTableView; }
            private set 
            {
                Set(ref _SelectedTableView, value);
            }
        }

        #endregion

        #region SelectedTable

        private Table _selectedTable;

        public Table SelectedTable
        {
            get { return _selectedTable; }
            set 
            { 
                if (Set(ref _selectedTable, value))
                {
                    //if (SelectedTableView != null) SelectedTableView.SortDescriptions.Clear();
                    EnableSort = false;
                    ReverseSort = false;
                    SelectedTableView = CollectionViewSource.GetDefaultView(_selectedTable.Items);
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand BackToProfilePageCommand { get; }

        #region SearchCommand

        public ICommand SearchCommand { get; }

        private void SearchCommandExecute(object p)
        {
            SelectedTableView.Filter = (r) =>
            { 
               return (r as STAFF).FIRST_NAME.ToString().ToLower().Contains(SearchQuery.ToLower()); 
            };
        }

        #endregion

        #region ClearUsersLogCommand

        public ICommand ClearUsersLogCommand { get; }
        // TODO: add ability to set path to log in config
        private void ClearUsersLogCommandExecute(object p)
        {
            StreamWriter usersLogFile = new StreamWriter(@".\users.log");
            usersLogFile.Write(string.Empty);
            usersLogFile.Close();
            // TODO: add confirmation
        }
        private bool ClearUsersLogCommandCanExecute(object p)
        {
            return File.Exists(@".\users.log");
        }

        #endregion

        #region OpenUsersLogCommand

        public ICommand OpenUsersLogCommand { get; }
        // TODO: add ability to set path to log in config and check log file exist
        private void OpenUsersLogCommandExecute(object p) => Process.Start(@".\users.log");

        #endregion

        #region OpenConfigFileCommand

        public ICommand OpenConfigFileCommand { get; }
        private void OpenConfigFileCommandExecute(object p) => Process.Start(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        
        #endregion

        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void CloseAppCommandExecute(object p) => App.Current.Shutdown();
        #endregion

        #endregion

        private async void OnAdminPageShow(Base.ViewModelBase currentVM)
        {
            if (currentVM is AdminPageViewModel)
            {
                await LoadTablesAsync();
                SelectedTable = DBTables.First();
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
        }
    }
}
