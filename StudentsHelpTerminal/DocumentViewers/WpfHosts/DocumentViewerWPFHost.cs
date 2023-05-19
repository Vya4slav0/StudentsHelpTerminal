using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;

namespace DocumentViewers
{
    public class DocumentViewerWPFHost : WindowsFormsHost
    {
        #region CurrentFileProperty

        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register("FilePath", typeof(string), typeof(DocumentViewerWPFHost), new PropertyMetadata(FilePathPropertyChanged));

        public string FilePath
        {
            get
            {
                return (string)GetValue(FilePathProperty);
            }

            set
            {
                SetValue(FilePathProperty, value);
            }
        }

        #endregion
        #region Available file extensions const
        public static readonly string[] AvailableExtensions = { ".pdf", ".docx", ".doc" };
        #endregion

        private static void FilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DocumentViewerWPFHost host = (DocumentViewerWPFHost)d;
            host.LoadFile((string)e.NewValue);
        }

        private void LoadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            if (!File.Exists(fileName)) throw new FileNotFoundException();

            FileInfo docInfo = new FileInfo(fileName);
            switch (docInfo.Extension.ToLower())
            {
                case ".pdf":
                    Child = new PDFViewer();
                    break;
                case ".doc":
                case ".docx":
                    Child = new DocxViewer();
                    break;
            }

            (Child as IDocumentViewer).LoadFile(fileName);
        }
    }
}
