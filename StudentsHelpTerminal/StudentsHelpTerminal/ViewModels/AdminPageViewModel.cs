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
            CloseAppCommand = new RelayCommand(CloseAppCommandExecute);
            OpenConfigFileCommand = new RelayCommand(OpenConfigFileCommandExecute);
            OpenUsersLogCommand = new RelayCommand(OpenUsersLogCommandExecute);
            ClearUsersLogCommand = new RelayCommand(ClearUsersLogCommandExecute, ClearUsersLogCommandCanExecute);
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
