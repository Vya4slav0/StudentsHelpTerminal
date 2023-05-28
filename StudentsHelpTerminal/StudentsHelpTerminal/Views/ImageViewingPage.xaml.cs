using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsHelpTerminal.Views
{
    /// <summary>
    /// Логика взаимодействия для ImageViewingPage.xaml
    /// </summary>
    public partial class ImageViewingPage : Page
    {
        public ImageViewingPage()
        {
            InitializeComponent();
        }

        private void Image_TouchMove(object sender, TouchEventArgs e)
        {
            
        }

        Point previousMousePos;

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is ScrollViewer)) return;
            ScrollViewer scrollViewer = sender as ScrollViewer;
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (previousMousePos == new Point()) previousMousePos = e.GetPosition(scrollViewer);
                if (previousMousePos == e.GetPosition(scrollViewer)) return;

                Point delta = new Point(e.GetPosition(scrollViewer).X - previousMousePos.X,
                    e.GetPosition(scrollViewer).Y - previousMousePos.Y);
                
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - delta.X);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - delta.Y);
                previousMousePos = e.GetPosition(scrollViewer);
            }
            else { previousMousePos = new Point(); }
        }
    }
}
