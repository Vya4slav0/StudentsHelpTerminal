using StudentsHelpTerminal.Infrastructure.Commands;
using StudentsHelpTerminal.Infrastructure.Stores;
using StudentsHelpTerminal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainPageViewModel : Base.ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MainPageViewModel(NavigationStore navigationStore, int cardId)
        {
            _navigationStore = navigationStore;

            #region Commands definition

            AdminWindowOpenCommand = new NavigationCommand(_navigationStore, new AdminPageViewModel(_navigationStore));

            ToIdlePageCommand = new NavigationCommand(_navigationStore, _navigationStore.CurrentIdlePageViewModel);

            #endregion

            FillPropertiesByCardId(cardId);
        }

        #region Properties

        #region Name
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                Set(ref _Name, value);
            }
        }
#endregion
#region Surname
        private string _Surname;
        public string Surname
        {
            get { return _Surname; }
            set
            {
                Set(ref _Surname, value);
            }
        }
#endregion
#region Group
        private string _Group;
        public string Group
        {
            get { return "Группа: " + _Group; }
            set
            {
                Set(ref _Group, value);
            }
        }
#endregion
#region CardNum
        private string _CardNum;
        public string CardNum
        {
            get { return "Номер карты: " + _CardNum; }
            set
            {
                Set(ref _CardNum, value);
            }
        }
#endregion
#region Photo
        private BitmapImage _Photo;
        public BitmapImage Photo
        {
            get { return _Photo; }
            set
            {
                Set(ref _Photo, value);
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand AdminWindowOpenCommand { get; }

        public ICommand ToIdlePageCommand { get; }

        #endregion

        private void CardReaderSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //FillPropertiesByCardId(Convert.ToInt32(CardReaderSerialPort.ReadLine()));            
        }

        private void FillPropertiesByCardId(int cardId)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                int? staffId = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == cardId)?.STAFF_ID;
                if (staffId is null || staffId.Value == 0) return; // mb i need to notify smth about this
                STAFF student = db.STAFFs.First(st => st.ID_STAFF == staffId);
                Name = student.FIRST_NAME;
                Surname = student.LAST_NAME;

                #region Converting photo from blob to BitmapImage
                BitmapImage image = new BitmapImage();
                MemoryStream photoMemoryStream = new MemoryStream(student.PORTRET);
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = photoMemoryStream;
                image.EndInit();
                image.Freeze();
                #endregion

                Photo = image;
                Group = db.SUBDIV_REF.First(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == staffId).SUBDIV_ID).DISPLAY_NAME;
                CardNum = cardId.ToString();
            }
        }
    }
}
