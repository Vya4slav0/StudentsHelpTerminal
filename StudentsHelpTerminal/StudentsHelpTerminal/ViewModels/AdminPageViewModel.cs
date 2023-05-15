using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Models.Other;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Controls;
using StudentsHelpTerminal.Infrastructure.Services;
using System.Collections.ObjectModel;
using DialogBoxes;

namespace StudentsHelpTerminal.ViewModels
{
    internal class AdminPageViewModel : Base.ViewModelBase
    {
        public AdminPageViewModel()
        {
            ApplySearchSortCommand = new RelayCommand(ApplySearchSortCommandCommandExecute, ApplySearchSortCommandCommandCanExecute);

            CloseAppCommand = new RelayCommand(CloseAppCommandExecute);
            OpenConfigFileCommand = new RelayCommand(OpenConfigFileCommandExecute);
            OpenUsersLogCommand = new RelayCommand(OpenUsersLogCommandExecute, OpenUsersLogCommandCanExecute);
            ClearUsersLogCommand = new RelayCommand(ClearUsersLogCommandExecute, ClearUsersLogCommandCanExecute);
            OpenSettingsPageCommnd = new RelayCommand(OpenSettingsPageCommandExecute);
            NavigateBackFromAdminCommand = new NavigateBackCommand(NavigateBackFromAdminCommandCanExecute);

            AddTableCommand = new RelayCommand(AddTableCommandExecute, AddTableCommandCanExecute);
            RenameTableCommand = new RelayCommand(RenameTableCommandExecute, RenameTableCommandCanExecute);
            RemoveTableCommand = new RelayCommand(RemoveTableCommandExecute, RemoveTableCommandCanExecute);

            CurrentSearchDescription = new SearchDescription();
            CurrentSortDescription = new SortDescription();

            _DBTables.CollectionChanged += (s, a) => { this.OnPropertyChanged(nameof(DBTables)); }; 
        }

        #region Properties

        #region DBTables

        private ObservableCollection<Table> _DBTables = new ObservableCollection<Table>();
        public ObservableCollection<Table> DBTables 
        {
            get { return _DBTables; }
            private set { Set(ref _DBTables, value); }
        }

        #endregion

        public string[] AvailableColumns
        {
            get { return (string[])DBHelper.GetAvailableColumnNames(); }
        }

        #region Sort and search

        public Models.Other.SearchDescription CurrentSearchDescription { get; set; }
        public Models.Other.SortDescription CurrentSortDescription { get; set; }

        #endregion

        #region NewTableName

        private string _NewTableName = string.Empty;

        public string NewTableName 
        { 
            get { return _NewTableName; }
            set { Set(ref _NewTableName, value); }
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
                    NewTableName = _selectedTable?.Name ?? string.Empty;
            }
        }

        #endregion

        #endregion

        #region Commands

        #region NavigateBackFromAdminCommand

        public ICommand NavigateBackFromAdminCommand { get; }

        private bool NavigateBackFromAdminCommandCanExecute(object p)
        {
            return !(NavigationStore.PrevViewModel is IdlePageViewModel);
        }

        #endregion

        #region ApplySearchSortCommand

        public ICommand ApplySearchSortCommand { get; }

        private bool ApplySearchSortCommandCommandCanExecute(object p)
        {
            return !CurrentSearchDescription.IsEmpty && !(SelectedTable is null);
        }

        private void ApplySearchSortCommandCommandExecute(object p)
        {
            SelectedTable.SearchDescription = CurrentSearchDescription;
            SelectedTable.SortDescription = CurrentSortDescription;  
            this.OnPropertyChanged(nameof(SelectedTable));
        }

        #endregion

        #region AddTableCommand

        public ICommand AddTableCommand { get; }

        private bool AddTableCommandCanExecute(object p)
        {
            return !string.IsNullOrEmpty(NewTableName?.Trim());
        }

        private void AddTableCommandExecute(object p)
        {
            SelectedTable = new Table(
                NewTableName,
                CurrentSearchDescription,
                CurrentSortDescription);
            DBTables.Add(SelectedTable);
        }

        #endregion

        #region RenameTableCommand

        public ICommand RenameTableCommand { get; }

        private bool RenameTableCommandCanExecute(object p)
        {
            return !(SelectedTable is null) && NewTableName.Trim().Length > 0;
        }

        private void RenameTableCommandExecute(object p)
        {
            SelectedTable.Name = NewTableName.Trim();
        }

        #endregion

        #region RemoveTableCommand

        public ICommand RemoveTableCommand { get; }

        private bool RemoveTableCommandCanExecute(object p)
        {
            return !(SelectedTable is null);
        }

        private void RemoveTableCommandExecute(object p)
        {
            DBTables.Remove(SelectedTable);
            SelectedTable = DBTables.FirstOrDefault();
        }

        #endregion

        #region ClearUsersLogCommand

        public ICommand ClearUsersLogCommand { get; }
        
        private void ClearUsersLogCommandExecute(object p)
        {
            if (!new ConfirmBox("Очистить лог пользователей?").ShowDialog()) return;
            StreamWriter usersLogFile = new StreamWriter(Properties.Settings.Default.PathToLogFile);
            usersLogFile.Write(string.Empty);
            usersLogFile.Close();
        }
        private bool ClearUsersLogCommandCanExecute(object p) => File.Exists(Properties.Settings.Default.PathToLogFile);

        #endregion

        #region OpenUsersLogCommand

        public ICommand OpenUsersLogCommand { get; }
        
        private void OpenUsersLogCommandExecute(object p) => Process.Start(Properties.Settings.Default.PathToLogFile);

        private bool OpenUsersLogCommandCanExecute(object p) => File.Exists(Properties.Settings.Default.PathToLogFile);

        #endregion

        #region OpenSettingsPageCommnd

        public ICommand OpenSettingsPageCommnd { get; }
        
        private void OpenSettingsPageCommandExecute(object p) => NavigationStore.CurrentViewModel = new SettingsPageViewModel();

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
    }
}
