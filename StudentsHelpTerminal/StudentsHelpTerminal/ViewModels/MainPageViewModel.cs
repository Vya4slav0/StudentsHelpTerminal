using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Models.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Security.Cryptography;
using DialogBoxes;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainPageViewModel : Base.ViewModelBase
    {
        public MainPageViewModel() { }

        public MainPageViewModel(long cardId)
        {
            #region Commands definition

            AdminWindowOpenCommand = new RelayCommand(AdminWindowOpenCommandExecute);
            CollegeMapOpenCommand = new NavigationCommand(new ImageViewindPageViewModel(Properties.Settings.Default.PathToCollegeMapsFolder));
            QuestionAnswerOpenCommand = new NavigationCommand(new QuestionAnswerPageViewModel());

            #endregion

            FillPropertiesByCardId(cardId);
        }

        #region Properties

        #region Name
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                Set(ref _Name, value);
            }
        }
        #endregion
        #region Surname
        private string _Surname;

        public string Surname
        {
            get { return _Surname; }
            set
            {
                Set(ref _Surname, value);
            }
        }
        #endregion
        #region Group
        private string _Group;

        public string Group
        {
            get { return _Group; }
            set { Set(ref _Group, value); }
        }

        public string GroupWithPrefix
        {
            get { return "Группа: " + _Group; }
        }

        public string DocsForGroup
        {
            get { return "Документы для группы " + _Group; }
        }
        #endregion
        #region CardNum
        private string _CardNum;

        public string CardNum
        {
            get { return _CardNum; }
            set
            {
                Set(ref _CardNum, value);
            }
        }

        public string CardNumWithPrefix
        {
            get { return "Номер карты: " + _CardNum; }
        }
        #endregion
        #region Photo
        private BitmapImage _Photo;

        public BitmapImage Photo
        {
            get { return _Photo; }
            set
            {
                Set(ref _Photo, value);
            }
        }

        #endregion
        #region Available documents
        private List<DocumentsListItem> _AvailableDocs = new List<DocumentsListItem>();

        public List<DocumentsListItem> AvailableDocs
        {
            get { return _AvailableDocs; }
            set
            {
                Set(ref _AvailableDocs, value);
            }
        }
        #endregion
        #region Is administrator
        private bool _IsAdministrator;

        public bool IsAdministrator
        {
            get { return _IsAdministrator; }
            private set { Set(ref _IsAdministrator, value); }
        }
        #endregion

        #endregion

        #region Commands

        public ICommand AdminWindowOpenCommand { get; }

        private void AdminWindowOpenCommandExecute(object p)
        {
            if (AdminAutorizer.CheckPassword(new PromptBox("Ввведите ваш пароль администратора").ShowDialog())) 
                new NavigationCommand(new AdminPageViewModel()).Execute(p);
        }

        public ICommand CollegeMapOpenCommand { get; }

        public ICommand QuestionAnswerOpenCommand { get; }

        #endregion

        private void FillPropertiesByCardId(long cardId)
        {
            Student student = DBHelper.GetStudentByCardId(cardId);

            //Filling properties
            Name = student.Name;
            Surname = student.Surname;
            Photo = student.Photo;
            Group = student.Group;
            CardNum = cardId.ToString();
            IsAdministrator = AdminAutorizer.CheckAdminByStuffId(student.Id);

            //Writing in log
            Logger.ClearLogIfFull();
            Logger.WriteLog(student);

            AvailableDocs.AddRange(DocumentsOperator.GetCommonDocuments());
            AvailableDocs.AddRange(DocumentsOperator.GetGroupDocuments(student.Group));
        }
    }
}
