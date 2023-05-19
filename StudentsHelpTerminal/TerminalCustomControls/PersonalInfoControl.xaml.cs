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

namespace TerminalCustomControls
{
    /// <summary>
    /// Логика взаимодействия для PersonalInfoControl.xaml
    /// </summary>
    public partial class PersonalInfoControl : UserControl
    {
        #region FrameStyleProperty

        private static readonly DependencyProperty FrameStyleProperty = DependencyProperty.Register(nameof(FrameStyle), typeof(Style), typeof(PersonalInfoControl));

        public Style FrameStyle
        {
            get { return (Style)GetValue(FrameStyleProperty);}
            set { SetValue(FrameStyleProperty, value);}
        }

        #endregion

        #region MainTextStyleProperty

        private static readonly DependencyProperty MainTextStyleProperty = DependencyProperty.Register(nameof(MainTextStyle), typeof(Style), typeof(PersonalInfoControl));

        public Style MainTextStyle
        {
            get { return (Style)GetValue(MainTextStyleProperty); }
            set { SetValue(MainTextStyleProperty, value); }
        }

        #endregion

        #region OtherTextStyleProperty

        private static readonly DependencyProperty OtherTextStyleProperty = DependencyProperty.Register(nameof(OtherTextStyle), typeof(Style), typeof(PersonalInfoControl));

        public Style OtherTextStyle
        {
            get { return (Style)GetValue(OtherTextStyleProperty); }
            set { SetValue(OtherTextStyleProperty, value); }
        }

        #endregion


        #region FirstNameProperty

        private static readonly DependencyProperty FirsttNameProperty = DependencyProperty.Register(nameof(FirstName), typeof(string), typeof(PersonalInfoControl));

        public string FirstName
        {
            get { return (string)GetValue(FirsttNameProperty); }
            set { SetValue(FirsttNameProperty, value);}
        }

        #endregion

        #region StudentSurnameProperty

        private static readonly DependencyProperty SurnameProperty = DependencyProperty.Register(nameof(Surname), typeof(string), typeof(PersonalInfoControl));

        public string Surname
        {
            get { return (string)GetValue(SurnameProperty); }
            set { SetValue(SurnameProperty, value); }
        }

        #endregion

        #region GroupProperty

        private static readonly DependencyProperty GroupProperty = DependencyProperty.Register(nameof(Group), typeof(string), typeof(PersonalInfoControl));

        public string Group
        {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        #endregion

        #region CardNumProperty

        private static readonly DependencyProperty CardNumProperty = DependencyProperty.Register(nameof(CardNum), typeof(string), typeof(PersonalInfoControl));

        public string CardNum
        {
            get { return (string)GetValue(CardNumProperty); }
            set { SetValue(CardNumProperty, value); }
        }

        #endregion

        #region PhotoProperty

        private static readonly DependencyProperty PhotoProperty = DependencyProperty.Register(nameof(Photo), typeof(BitmapImage), typeof(PersonalInfoControl));

        public BitmapImage Photo
        {
            get { return (BitmapImage)GetValue(PhotoProperty); }
            set { SetValue(PhotoProperty, value); }
        }

        #endregion


        public PersonalInfoControl()
        {
            InitializeComponent();
        }
    }
}
