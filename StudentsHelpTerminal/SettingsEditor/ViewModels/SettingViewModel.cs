using SettingsEditor.Models;
using System;

namespace SettingsEditor.ViewModels
{
    internal class SettingViewModel : Base.ViewModelBase
    {
        private readonly Setting _setting;

        public SettingViewModel(Setting setting)
        {
            _setting = setting;
        }

        #region Name 

        public string Name
        {
            get => _setting.Name;
        }

        #endregion
        #region Value

        public object Value
        {
            get => _setting.Value;
            set
            {
                if (_setting.Value == value) return;
                _setting.Value = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValid));
                OnPropertyChanged(nameof(InvalidMessage));
            }
        }

        #endregion
        #region Type

        public Type Type { get => _setting.Type; }

        #endregion
        #region Description

        public string Description
        {
            get => _setting.Description;
        }

        #endregion
        #region Section

        public string Section
        {
            get => _setting.Section;
        }

        #endregion
        #region Validation parameters

        public bool IsValid 
        { 
            get => _setting.IsValid;
        }
        public string InvalidMessage 
        { 
            get => _setting.InvalidMessage;
        }

        #endregion

        public Setting GetSetting()
        {
            return _setting;
        }
    }
}
