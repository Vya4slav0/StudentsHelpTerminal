using System.Drawing;
using System.IO;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.Models.Other
{
    internal class DocumentsListItem
    {
        public enum Type { Common, Group }

        #region Properties
        public string VisibleName { get; set; }

        public Type DocType { get; set; }

        public FileInfo FileInfo { get; set; }

        public System.Windows.Media.ImageSource DocIcon { get; set; }
        #endregion

        public DocumentsListItem(string filePath, Type type)
        {
            FileInfo = new FileInfo(filePath);
            DocType = type;
            VisibleName = FileInfo.Name;
            //Extract and convert icon
            Icon icon = Icon.ExtractAssociatedIcon(filePath);
            DocIcon = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                new System.Windows.Int32Rect(0, 0, icon.Width, icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
            DocIcon.Freeze();
        }

        public override string ToString()
        {
            return FileInfo.ToString();
        }
    }
}
