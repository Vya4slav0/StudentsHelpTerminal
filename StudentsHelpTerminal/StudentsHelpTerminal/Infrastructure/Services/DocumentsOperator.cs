using StudentsHelpTerminal.Models.Other;
using System.Collections.Generic;
using System.IO;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class DocumentsOperator
    {
        //Searching for available documents
        private static readonly string commonDocs = Properties.Settings.Default.CommonDocsDirectoryName;

        public static IEnumerable<DocumentsListItem> GetGroupDocuments(string groupName)
        {
            string groupDocs = string.Format("{0}\\{1}", Properties.Settings.Default.PathToMainFolder, groupName);

            //Create documents directory if not exist
            if (!Directory.Exists(groupDocs)) Directory.CreateDirectory(groupDocs);

            List<DocumentsListItem> result = new List<DocumentsListItem>();
            //Searching for the group documents
            foreach (string filePath in Directory.GetFiles(groupDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Group);
                if (file.FileInfo.Extension == ".xps")
                    result.Add(file);
            }
            return result;
        }

        public static IEnumerable<DocumentsListItem> GetCommonDocuments()
        {
            List<DocumentsListItem> result = new List<DocumentsListItem>();
            //Searching for documents for all students
            foreach (string filePath in Directory.GetFiles(commonDocs))
            {
                DocumentsListItem file = new DocumentsListItem(filePath, DocumentsListItem.Type.Common);
                if (file.FileInfo.Extension == ".xps")
                    result.Add(file);
            }
            return result;
        }
    }
}
