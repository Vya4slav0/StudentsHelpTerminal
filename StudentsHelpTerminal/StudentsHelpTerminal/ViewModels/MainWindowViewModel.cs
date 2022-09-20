using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.ViewModels
{
    internal class MainWindowViewModel : Base.ViewModelBase
    {
        SerialPort CardReaderSerialPort = new SerialPort("COM6", 9600);
        public MainWindowViewModel()
        {
            CardReaderSerialPort.DataReceived += (s, a) =>
            {
                
            };
            Name = "SampleName";
            Surname = "SampleSurname";
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
            get {return _Surname;}
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
    }
}
