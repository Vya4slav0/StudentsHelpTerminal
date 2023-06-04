using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DialogBoxes;
using StudentsHelpTerminal.Infrastructure.Commands;

namespace StudentsHelpTerminal.ViewModels
{
    internal class ChangeCardIdPageViewModel : Base.ViewModelBase
    {
        public ChangeCardIdPageViewModel() { }

        public ChangeCardIdPageViewModel(Student student)
        {
            StudentToChangeCardId = student;

            ApplyChangesCommand = new RelayCommand(ApplyChangesCommandExecute, ApplyChangesCommandCanExecute);
        }

        #region Properties

        public Student StudentToChangeCardId { get; set; }

        public string StudentFullName 
        { 
            get 
            {
                return StudentToChangeCardId != null ?
                    $"{StudentToChangeCardId.Surname} {StudentToChangeCardId.Name} {StudentToChangeCardId.Patronymic}" :
                    string.Empty;
            }
        }

        public string NewCardID { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        public bool Confirmed { get; set; }

        #endregion

        #region Commands

        #region ApplyChangesCommand

        public ICommand ApplyChangesCommand { get; }

        private void ApplyChangesCommandExecute(object p)
        {
            long newCardId;
            try
            {
                newCardId = Convert.ToInt64(NewCardID);
            }
            catch (Exception ex)
            {
                new AlertBox($"{ex.GetType().Name}: {ex.Message}").ShowDialog();
                return;
            }
            string reasonOfChangement = 
                string.Format("{0}{1}", IsDeleted ? "Удален " : "Утеряна - ", Date.ToString("dd.MM.yyyy"));
            string errorText;
            if (DBHelper.ChangeCardIdByStuffId(StudentToChangeCardId.Id, newCardId, reasonOfChangement, out errorText))
            {
                new AlertBox("Идентификатор карты успешно изменён").ShowDialog();
                return;
            }
            new AlertBox("Ошибка: " + errorText).ShowDialog();
        }

        private bool ApplyChangesCommandCanExecute(object p) => !string.IsNullOrEmpty(NewCardID?.Trim()) && Confirmed;

        #endregion

        #endregion
    }
}
