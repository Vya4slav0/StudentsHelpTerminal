using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Group { get; set; }
        public string CardID { get; set; }
        public BitmapImage Photo { get; set; }
    }
}
