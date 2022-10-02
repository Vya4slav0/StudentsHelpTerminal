using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class ShowViewAsNewWindow
    {
        public static void ShowWindow(object viewModel)
        {
            Window window = new Window();
            window.Content = viewModel;
            window.DataContext = viewModel;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Show();
        }
    }
}
