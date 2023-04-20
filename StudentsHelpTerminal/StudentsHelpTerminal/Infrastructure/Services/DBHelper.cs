using StudentsHelpTerminal.Models;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class DBHelper
    {
        public static Student GetStudentByStaffId(int staffId)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                STAFF student = db.STAFFs.FirstOrDefault(st => st.ID_STAFF == staffId);
                if (student is null) return null;

                //TODO: maybe I need to make one method for this
                #region Converting photo from blob to BitmapImage
                BitmapImage image = new BitmapImage();
                MemoryStream photoMemoryStream = new MemoryStream(student.PORTRET);
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = photoMemoryStream;
                image.EndInit();
                image.Freeze();
                #endregion

                return new Student()
                {
                    Id = staffId,
                    Surname = student.LAST_NAME.Trim(),
                    Name = student.FIRST_NAME.Trim(),
                    Patronymic = student.MIDDLE_NAME.Trim(),
                    Group = db.SUBDIV_REF.First(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == staffId).SUBDIV_ID).DISPLAY_NAME.Trim(),
                    CardID = db.STAFF_CARDS.First(sc => sc.STAFF_ID == staffId).IDENTIFIER.ToString(),
                Photo = image
                };
            }
        }
        public static Student GetStudentByCardId(int cardId)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                //Getting student info
                int? staffId = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == cardId)?.STAFF_ID;
                if (staffId is null || staffId.Value == 0) return null;
                STAFF student = db.STAFFs.First(st => st.ID_STAFF == staffId);

                #region Converting photo from blob to BitmapImage
                BitmapImage image = new BitmapImage();
                MemoryStream photoMemoryStream = new MemoryStream(student.PORTRET);
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = photoMemoryStream;
                image.EndInit();
                image.Freeze();
                #endregion

                return new Student()
                {
                    Id = staffId.Value,
                    Surname = student.LAST_NAME.Trim(),
                    Name = student.FIRST_NAME.Trim(),
                    Patronymic = student.MIDDLE_NAME.Trim(), 
                    Group = db.SUBDIV_REF.First(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == staffId).SUBDIV_ID).DISPLAY_NAME.Trim(),
                    CardID = cardId.ToString(),
                    Photo = image
                };
            }
        }
    }
}
