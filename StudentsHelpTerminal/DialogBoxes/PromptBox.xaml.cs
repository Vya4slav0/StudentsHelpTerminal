using System.Windows;

namespace DialogBoxes
{
    /// <summary>
    /// Логика взаимодействия для PromptBox.xaml
    /// </summary>
    public partial class PromptBox : Window
    {
        public PromptBox(string text)
        {
            InitializeComponent();
            TextBlockText.Text = text;
            TextBoxInput.Focus();
        }

        public new string ShowDialog()
        {
            base.ShowDialog();
            return TextBoxInput.Text;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
