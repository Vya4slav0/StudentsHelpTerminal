using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentsHelpTerminal.ViewModels
{
    internal class SettingsPageViewModel : Base.ViewModelBase
    {
        public SettingsPageViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
        }

        #region Properties

        #region SettingsList

        private List<Setting> _SettingsList;

        public List<Setting> SettingsList
        {
            get 
            {
                _SettingsList = new List<Setting>();
                foreach (System.Configuration.SettingsProperty s in Properties.Settings.Default.Properties)
                {
                    _SettingsList.Add(new Setting { 
                        Name = s.Name, 
                        Type = s.PropertyType, 
                        Value = Properties.Settings.Default[s.Name]});
                }
                return _SettingsList; 
            }
            set
            {
                _SettingsList = value;
            }
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
            foreach (Setting s in _SettingsList)
            {
                Properties.Settings.Default[s.Name] = s.Value;
            }
            Properties.Settings.Default.Save();
        }

        #endregion

        #endregion
    }
}
