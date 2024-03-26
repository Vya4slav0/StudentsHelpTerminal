using SettingsEditor.Infrastructure.Commands;
using SettingsEditor.Infrastructure.Services;
using SettingsEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SettingsEditor.ViewModels
{
    internal class SettingsPageViewModel : Base.ViewModelBase
    {
        SettingsManagerService _settingsManager;

        public SettingsPageViewModel(string pathToSettingsXML)
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
            _settingsManager = new SettingsManagerService(pathToSettingsXML);
            SettingsList = _settingsManager.LoadAllSettings();
        }

        public SettingsPageViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
        }

        #region Properties

        #region SettingsList

        private List<Setting> _SettingsList;

        public List<Setting> SettingsList
        {
            get => _SettingsList;
            set { Set(ref _SettingsList, value); }
        }

        #endregion

        #region SelectedSetting

        private Setting _SelectedSetting;

        public Setting SelectedSetting
        {
            get => _SelectedSetting;
            set { Set(ref _SelectedSetting, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region SaveSettingsCommand

        public ICommand SaveSettingsCommand { get; }

        private void SaveSettingsCommandExecute(object p)
        {
            _settingsManager.SaveSettings(SettingsList);
        }

        #endregion

        #endregion
    }
}
