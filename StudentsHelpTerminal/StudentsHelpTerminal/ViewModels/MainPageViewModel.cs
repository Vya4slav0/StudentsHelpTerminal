using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Infrastructure.Services;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Models.Other;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainPageViewModel : Base.ViewModelBase
    {
        public MainPageViewModel(int cardId)
        {
            #region Commands definition

            AdminWindowOpenCommand = new NavigationCommand(new AdminPageViewModel());

            ToIdlePageCommand = new NavigationCommand(NavigationStore.CurrentIdlePageViewModel);

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
            get { return "Группа: " + _Group; }
            set
            {
                Set(ref _Group, value);
            }
        }
#endregion
#region CardNum
        private string _CardNum;
        public string CardNum
        {
            get { return "Номер карты: " + _CardNum; }
            set
            {
                Set(ref _CardNum, value);
            }
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
        #endregion

        #region Commands

        public ICommand AdminWindowOpenCommand { get; }

        public ICommand ToIdlePageCommand { get; }

        #endregion

        private void FillPropertiesByCardId(int cardId)
        {
            Student student = DBHelper.GetStudentByCardId(cardId);

            //Filling properties
            Name = student.Name;
            Surname = student.Surname;
            Photo = student.Photo;
            Group = student.Group;
            CardNum = cardId.ToString();
            
            //Searching for available documents
            string commonDocs = string.Format("{0}\\{1}", 
                Properties.Settings.Default.PathToMainFolder,
                Properties.Settings.Default.CommonDocsDirectoryName);
            string groupDocs = string.Format("{0}\\{1}",
                Properties.Settings.Default.PathToMainFolder,
                student.Group);

            if (!Directory.Exists(groupDocs)) Directory.CreateDirectory(groupDocs);

            //Searching for documents for all students
            foreach (string filePath in Directory.GetFiles(commonDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Common);
                if (DocumentViewers.DocumentViewerWPFHost.AvailableExtensions.Contains(file.FileInfo.Extension))
                    AvailableDocs.Add(file);
            }
            
            //Searching for this group documents
            foreach(string filePath in Directory.GetFiles(groupDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Group);
                if (DocumentViewers.DocumentViewerWPFHost.AvailableExtensions.Contains(file.FileInfo.Extension))
                    AvailableDocs.Add(file);
            }
        }
    }
}
