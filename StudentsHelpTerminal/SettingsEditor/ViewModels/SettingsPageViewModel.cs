using SettingsEditor.Infrastructure.Commands;
using SettingsEditor.Infrastructure.Services;
using SettingsEditor.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using DialogBoxes;

namespace SettingsEditor.ViewModels
{
    internal class SettingsPageViewModel : Base.ViewModelBase
    {
        SettingsManagerService _settingsManager;

        public SettingsPageViewModel(string pathToSettingsXML)
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
            SelectFileDirectoryCommand = new RelayCommand(SelectFileDirectoryCommandExecute, SelectFileDirectoryCommandCanExecute);
            OpenSettingsFileCommand = new RelayCommand(OpenSettingsFileExecute);
            AppCloseCommand = new RelayCommand(AppCloseCommandExecute);

            FillSettingsGrid(pathToSettingsXML);
        }

        public SettingsPageViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
            SelectFileDirectoryCommand = new RelayCommand(SelectFileDirectoryCommandExecute, SelectFileDirectoryCommandCanExecute);
            OpenSettingsFileCommand = new RelayCommand(OpenSettingsFileExecute);
            AppCloseCommand = new RelayCommand(AppCloseCommandExecute);
        }

        private void FillSettingsGrid(string pathToSettingsXML)
        {
            _settingsManager = new SettingsManagerService(pathToSettingsXML);
            SettingsList = new ObservableCollection<SettingViewModel>();
            SettingsList.Clear();
            foreach (Setting setting in _settingsManager.LoadAllSettings())
            {
                SettingsList.Add(new SettingViewModel(setting));
            }
            OnPropertyChanged(nameof(Title));
        }

        #region Properties

        #region SettingsList

        private ObservableCollection<SettingViewModel> _SettingsList;

        public ObservableCollection<SettingViewModel> SettingsList
        {
            get => _SettingsList;
            set { Set(ref _SettingsList, value); }
        }

        #endregion

        #region SelectedSetting

        private SettingViewModel _SelectedSetting;

        public SettingViewModel SelectedSetting
        {
            get => _SelectedSetting;
            set { Set(ref _SelectedSetting, value); }
        }

        #endregion

        #region Title

        public string Title => _settingsManager?.PathToSettingsXML;

        #endregion

        #endregion

        #region Commands

        #region SaveSettingsCommand

        public ICommand SaveSettingsCommand { get; }

        private void SaveSettingsCommandExecute(object p)
        {
            List<Setting> settings = new List<Setting>();
            foreach (var settingVM in SettingsList)
            {
                settings.Add(settingVM.GetSetting());
            }
            _settingsManager.SaveSettings(settings);
            new AlertBox("Сохранено").ShowDialog();
        }

        #endregion

        #region SelectFileDirectoryCommand

        public ICommand SelectFileDirectoryCommand { get; }

        private void SelectFileDirectoryCommandExecute(object p)
        {
            if (SelectedSetting.Type == typeof(DirectoryInfo))
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        SelectedSetting.Value = fbd.SelectedPath;
                    }
                }
            }
            
            if (SelectedSetting.Type == typeof(FileInfo))
            {
                using (var ofd = new System.Windows.Forms.OpenFileDialog())
                {
                    DialogResult result = ofd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                    {
                        SelectedSetting.Value = ofd.FileName;
                    }
                }
            }
            OnPropertyChanged(nameof(SelectedSetting));
        }

        private bool SelectFileDirectoryCommandCanExecute(object p)
        {
            if (SelectedSetting is null) return false;
            return (SelectedSetting.Type == typeof(DirectoryInfo)) || 
                (SelectedSetting.Type == typeof(FileInfo));
        }

        #endregion

        #region OpenSettingsFileCommand

        public ICommand OpenSettingsFileCommand { get; }

        private void OpenSettingsFileExecute(object p)
        {
            using (OpenFileDialog openSettingsFileDialog = new OpenFileDialog())
            {
                openSettingsFileDialog.Filter = "Settings XML file|*.xml";
                openSettingsFileDialog.InitialDirectory = "\\.";
                if (openSettingsFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FillSettingsGrid(openSettingsFileDialog.FileName);
                }
            }
        }

        #endregion

        #region AppCloseCommand

        public ICommand AppCloseCommand { get; }

        private void AppCloseCommandExecute(object p)
        {
            App.Current.Shutdown();
        }

        #endregion

        #endregion
    }
}
