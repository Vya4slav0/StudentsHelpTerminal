using System;
using System.Collections.Generic;
using System.Linq;
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

        public System.Configuration.SettingsPropertyCollection SettingsList
        {
            get { return Properties.Settings.Default.Properties; }
        }

        #endregion

        #region Commands



        #endregion
    }
}
