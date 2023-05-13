using System.Windows;

namespace DialogBoxes
{
    /// <summary>
    /// Логика взаимодействия для ConfirmBox.xaml
    /// </summary>
    public partial class ConfirmBox : Window
    {
        public ConfirmBox()
        {
            InitializeComponent();
            DialogResult = false;
        }

        public new bool ShowDialog()
        {
            return DialogResult.HasValue ? DialogResult.Value : false;
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
