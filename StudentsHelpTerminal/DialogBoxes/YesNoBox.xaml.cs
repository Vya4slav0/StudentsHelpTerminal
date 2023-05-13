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
using System.Windows.Shapes;

namespace DialogBoxes
{
    /// <summary>
    /// Логика взаимодействия для YesNoBox.xaml
    /// </summary>
    public partial class YesNoBox : Window
    {
        public YesNoBox(string text)
        {
            InitializeComponent();
            TextBlockText.Text = text;
        }

        public new bool ShowDialog()
        {
            return base.ShowDialog().HasValue ? DialogResult.Value : false;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false;
            Close();
        }
    }
}
