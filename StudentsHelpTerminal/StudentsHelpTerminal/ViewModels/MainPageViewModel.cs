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

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainPageViewModel : Base.ViewModelBase
    {
        public MainPageViewModel() { }

        public MainPageViewModel(int cardId)
        {
            #region Commands definition

            AdminWindowOpenCommand = new NavigationCommand(new AdminPageViewModel());
            CollegeMapOpenCommand = new NavigationCommand(new ImageViewindPageViewModel(Properties.Settings.Default.PathToCollegeMapsFolder));

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

        #endregion

        #region Commands

        public ICommand AdminWindowOpenCommand { get; }

        public ICommand CollegeMapOpenCommand { get; }

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

            //Writing in log
            WriteLog(student);
            
            //Searching for available documents
            string commonDocs = Properties.Settings.Default.CommonDocsDirectoryName;
            string groupDocs = string.Format("{0}\\{1}",
                Properties.Settings.Default.PathToMainFolder,
                student.Group);

            if (!Directory.Exists(groupDocs)) Directory.CreateDirectory(groupDocs);

            //Searching for documents for all students
            foreach (string filePath in Directory.GetFiles(commonDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Common);
                if (file.FileInfo.Extension == ".xps")
                    AvailableDocs.Add(file);
            }
            
            //Searching for this group documents
            foreach(string filePath in Directory.GetFiles(groupDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Group);
                if (file.FileInfo.Extension == ".xps")
                    AvailableDocs.Add(file);
            }
        }

        private void WriteLog(Student student)
        {   
            StreamWriter swLog;

            //Create log if doesn't exist
            if (!File.Exists(Properties.Settings.Default.PathToLogFile)) 
                swLog = new StreamWriter(File.Create(Properties.Settings.Default.PathToLogFile));
            else
                swLog = new StreamWriter(Properties.Settings.Default.PathToLogFile, true);

            FileInfo logFileInfo = new FileInfo(Properties.Settings.Default.PathToLogFile);

            swLog.WriteLine(string.Format("{0} - {1} {2} - {3}",
                student.CardID, 
                student.Name,
                student.Surname,
                DateTime.Now.ToString("dd.MM.yyyy, HH:mm")));

            swLog.Close();

            //Check if log is too large
            if (!(logFileInfo.Length > Properties.Settings.Default.MaxLogSizeMB * 1048576)) return;
            string[] lines = File.ReadAllLines(logFileInfo.FullName);
            File.WriteAllLines(logFileInfo.FullName, lines.Skip(lines.Length / 2).ToArray());
        }
    }
}
