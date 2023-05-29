using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;

namespace TerminalCustomControls
{
    public class XpsDocumentViewer : DocumentViewer
    {
        #region FilePathProperty
        private static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(XpsDocumentViewer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(LoadDocument)));
        
        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        private static void LoadDocument(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            string filePath = (string)e.NewValue;
            
            if (string.IsNullOrEmpty(filePath)) return;
            try
            {
                XpsDocument doc = new XpsDocument(filePath, FileAccess.Read);
                (source as DocumentViewer).Document = doc.GetFixedDocumentSequence();
            }
            catch (Exception ex)
            {
                Uri packageUri = new Uri(@"memorystream://ErrorDoc.xps");

                var package = Package.Open(new MemoryStream(Properties.Resources.ErrorDoc));
                PackageStore.AddPackage(packageUri, package);
                (source as DocumentViewer).Document = new XpsDocument(package, CompressionOption.SuperFast, packageUri.AbsoluteUri).GetFixedDocumentSequence();
            }

        }

        #endregion

        static XpsDocumentViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XpsDocumentViewer), new FrameworkPropertyMetadata(typeof(XpsDocumentViewer)));
        }
    }
}
