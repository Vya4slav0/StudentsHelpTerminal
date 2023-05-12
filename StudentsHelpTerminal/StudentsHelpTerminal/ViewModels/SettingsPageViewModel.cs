using StudentsHelpTerminal.Models.Other;
using StudentsHelpTerminal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.ViewModels
{
    internal class SettingsPageViewModel : Base.ViewModelBase
    {
        public SettingsPageViewModel()
        {
            
        }

        #region Properties

        public List<Setting> SettingsList
        {
            get 
            { 
                List<Setting> settings = new List<Setting>();
                foreach (System.Configuration.SettingsProperty s in Properties.Settings.Default.Properties)
                {
                    settings.Add(new Setting { 
                        Name = s.Name, 
                        Type = s.PropertyType, 
                        Value = Properties.Settings.Default[s.Name]});
                }
                return settings; 
            }
        }

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



        #endregion
    }
}
