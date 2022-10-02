using StudentsHelpTerminal.Infrastructure.Commands;
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
        private static readonly string COMName = Properties.Settings.Default.CardReaderPortName;
        private static readonly int COMSpeed = Properties.Settings.Default.CardReaderPortBaudRate;
        private SerialPort CardReaderSerialPort = new SerialPort(COMName, COMSpeed);

        public MainPageViewModel()
        {
            //CardReaderSerialPort.Open();
            CardReaderSerialPort.DataReceived += (s, a) =>
            {
                //Fill view model properties
                int cardId = Convert.ToInt32(CardReaderSerialPort.ReadLine());
                using (StudentsDBContext db = new StudentsDBContext())
                {
                    int? staffId = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == cardId)?.STAFF_ID;
                    if (staffId is null || staffId.Value == 0) return; // mb i need to notify smth about this
                    STAFF student = db.STAFFs.First(st => st.ID_STAFF == staffId);
                    Name = student.FIRST_NAME;
                    Surname = student.MIDDLE_NAME;
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
            };
            AdminWindowOpenCommand = new RelayCommand(OnAdminWindowOpenCommandExecute);
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

        #region AdminWindowOpen
        public ICommand AdminWindowOpenCommand { get; }
        private void OnAdminWindowOpenCommandExecute(object p) { }
        #endregion

        #endregion
    }
}
