using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DialogBoxes;

namespace StudentsHelpTerminal.ViewModels
{
    internal class ImageViewindPageViewModel : Base.ViewModelBase
    {
        // Empty constructor only for XAML designer
        public ImageViewindPageViewModel() { }

        public ImageViewindPageViewModel(string pathToImagesFolder)
        {
            const string alertNoImagesMessage = "Нет изображений к просмотру";

            string[] imageFormats = { ".png", ".jpg", ".jpeg" };

            DirectoryInfo directoryWithImages = new DirectoryInfo(pathToImagesFolder);

            if (!directoryWithImages.Exists)
            {
                directoryWithImages.Create();
                //new AlertBox(alertNoImagesMessage).ShowDialog();
                return;
            }

            foreach (FileInfo image in directoryWithImages.GetFiles())
            {
                if (imageFormats.Contains(image.Extension)) ImagePathes.Add(image);
            }    

            //if (ImagePathes.Count() == 0) new AlertBox(alertNoImagesMessage).ShowDialog();
        }

        #region Properties

        public List<FileInfo> ImagePathes { get; } = new List<FileInfo> { };

        #endregion
    }
}
