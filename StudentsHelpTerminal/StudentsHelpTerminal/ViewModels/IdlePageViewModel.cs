using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Infrastructure.Services;
using System.IO;
using System.Windows.Input;

namespace StudentsHelpTerminal.ViewModels
{
    internal class IdlePageViewModel : Base.ViewModelBase
    {
        public IdlePageViewModel() 
        {
            OpenApplicantGuideCommand = new RelayCommand(OpenApplicantGuideCommandExecute);
        }

        public bool OpenApplicantGuideVisible
        {
            get => File.Exists(SettingsInteractor.GetSettingValueByName("PathToApplicantGuide"));
        }

        public ICommand OpenApplicantGuideCommand { get; }

        private void OpenApplicantGuideCommandExecute(object p)
        {
            ProcessLauncherService.StartApplicantGuide();
        }
    }
}
