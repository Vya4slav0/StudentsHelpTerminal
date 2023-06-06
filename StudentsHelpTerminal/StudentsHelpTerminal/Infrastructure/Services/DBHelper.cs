using StudentsHelpTerminal.Models.Database;
using StudentsHelpTerminal.Models.Other;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class DBHelper
    {
        #region AvailableColumnNames

        public const string NAME = "Имя";
        public const string SURNAME = "Фамилия";
        public const string PATRONYMIC = "Отчество";
        public const string ID = "ID";
        public const string CARD_ID = "Код пропуска";

        #endregion

        public static BitmapImage BitmapImageFromBlob(byte[] blob)
        {
            BitmapImage image = new BitmapImage();
            MemoryStream photoMemoryStream = new MemoryStream(blob);
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = photoMemoryStream;
            image.EndInit();
            image.Freeze();
            return image;
        }
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public static IEnumerable<string> GetAvailableColumnNames()
        {
            return new string[] { ID, NAME, SURNAME, PATRONYMIC, CARD_ID };
        }
        public static IEnumerable<Student> FindStudents(string value, string columnName, bool notFull)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                List<STAFF> stuff = new List<STAFF>();
                switch (columnName)
                {
                    case NAME:
                        stuff = db.STAFFs.Where(s => s.FIRST_NAME.Contains(value)).ToList();
                        break; 
                    case SURNAME:
                        stuff = db.STAFFs.Where(s => s.LAST_NAME.Contains(value)).ToList();
                        break;
                    case PATRONYMIC:
                        stuff = db.STAFFs.Where(s => s.MIDDLE_NAME.Contains(value)).ToList();
                        break;
                    case ID:
                        int id = ForcedConvertToInt(value);
                        stuff = db.STAFFs.Where(s => s.ID_STAFF == id).ToList();
                        break;
                    case CARD_ID:
                        int idCard = ForcedConvertToInt(value);
                        int? idStuff = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == idCard)?.STAFF_ID;
                        if (idStuff is null) break;
                        stuff = db.STAFFs.Where(s => s.ID_STAFF == idStuff.Value).ToList();
                        break;
                }
                return GetStudentsFromStuffList(stuff);
            }
        }
        private static int ForcedConvertToInt(string value)
        {
            if (!Regex.IsMatch(value, @"^\d+$"))
            {
                value = Regex.Replace(value, @"\D", string.Empty);
            }
            return Convert.ToInt32(value);
        }
        private static IEnumerable<Student> GetStudentsFromStuffList(List<STAFF> stuff)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                List<Student> students = new List<Student>();
                foreach (STAFF s in stuff)
                {
                    students.Add(new Student()
                    {
                        Id = s.ID_STAFF,
                        Name = s.FIRST_NAME.Trim(),
                        Surname = s.LAST_NAME.Trim(),
                        Patronymic = s.MIDDLE_NAME.Trim(),
                        Photo = s.PORTRET is null ? BitmapToBitmapImage(Properties.Resources.no_photo) : BitmapImageFromBlob(s.PORTRET),
                        Group = db.SUBDIV_REF.FirstOrDefault(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == s.ID_STAFF).SUBDIV_ID)?.DISPLAY_NAME.Trim() ?? "NO",
                        CardID = db.STAFF_CARDS.FirstOrDefault(sc => sc.STAFF_ID == s.ID_STAFF)?.IDENTIFIER.ToString() ?? "NO"
                    });
                }
                return students;
            }   
        }
        public static Student GetStudentByStaffId(int stuffId)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                STAFF student = db.STAFFs.FirstOrDefault(st => st.ID_STAFF == stuffId);
                if (student is null) return null;

                return new Student()
                {
                    Id = stuffId,
                    Surname = student.LAST_NAME.Trim(),
                    Name = student.FIRST_NAME.Trim(),
                    Patronymic = student.MIDDLE_NAME.Trim(),
                    Group = db.SUBDIV_REF.First(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == stuffId).SUBDIV_ID).DISPLAY_NAME.Trim(),
                    CardID = db.STAFF_CARDS.First(sc => sc.STAFF_ID == stuffId).IDENTIFIER.ToString(),
                    Photo = BitmapImageFromBlob(student.PORTRET)
                };
            }
        }
        public static Student GetStudentByCardId(long cardId)
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                //Getting student info
                int? stuffId = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == cardId)?.STAFF_ID;
                if (stuffId is null || stuffId.Value == 0) return null;
                STAFF student = db.STAFFs.First(st => st.ID_STAFF == stuffId);

                return new Student()
                {
                    Id = stuffId.Value,
                    Surname = student.LAST_NAME.Trim(),
                    Name = student.FIRST_NAME.Trim(),
                    Patronymic = student.MIDDLE_NAME.Trim(), 
                    Group = db.SUBDIV_REF.First(sdr => sdr.ID_REF == db.STAFF_REF.FirstOrDefault(sr => sr.STAFF_ID == stuffId).SUBDIV_ID).DISPLAY_NAME.Trim(),
                    CardID = cardId.ToString(),
                    Photo = BitmapImageFromBlob(student.PORTRET)
                };
            }
        }
        public static bool ChangeCardIdByStuffId(int stuffId, long newCardId, string reasonOfChangement, out string errorText)
        {
            errorText = string.Empty;
            using (StudentsDBContext db = new StudentsDBContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        STAFF_CARDS currentCard = db.STAFF_CARDS.FirstOrDefault(sc => sc.STAFF_ID == stuffId);
                        if (currentCard is null)
                        {
                            errorText = "Изменить идентификатор карты невозможно, т.к. отсутствует запись в таблице карт-пропусков, ";
                            return false;
                        }

                        //Create new card by template, but with different identifier and key
                        STAFF_CARDS newCard = new STAFF_CARDS()
                        {
                            SYSTEM_TYPE = currentCard.SYSTEM_TYPE,
                            STAFF_ID = stuffId,
                            DATE_BEGIN = currentCard.DATE_BEGIN,
                            DATE_END = currentCard.DATE_END,
                            VALID = currentCard.VALID,
                            VALID_TRANSFER = currentCard.VALID_TRANSFER,
                            TEMPORARY_ACC = currentCard.TEMPORARY_ACC,
                            DOCUMENTS_ID = currentCard.DOCUMENTS_ID,
                            HISTORY_DATE = currentCard.HISTORY_DATE,
                            PROHIBIT = currentCard.PROHIBIT,
                            IDENTIFIER = newCardId,
                            TYPE_IDENTIFIER = currentCard.TYPE_IDENTIFIER,
                            IDENTIFIER_TRANSFORMED = newCardId,
                            WITHDRAW_TO_STOP_LIST = currentCard.WITHDRAW_TO_STOP_LIST,
                            LAST_TIMESTAMP = currentCard.LAST_TIMESTAMP,
                            USER_ID = currentCard.USER_ID
                        };

                        //Add new card to db
                        db.STAFF_CARDS.Add(newCard);

                        //Zero old card's stuff id
                        currentCard.STAFF_ID = 0;
                        db.SaveChanges();

                        //Add old card to stop cards
                        db.STOP_CARDS_DESCRIPTION.Add(new STOP_CARDS_DESCRIPTION() 
                        { 
                            CARD_ID = currentCard.ID_CARD,
                            DESCRIPTION = reasonOfChangement,
                            POSSIBLE_RECALL = 1
                        });
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        errorText = ex.Message;
                        transaction.Rollback();
                        return false;
                    }
                    return true;
                }
            }
        }
        public static bool HasStudent(long cardId)
        {
            using(StudentsDBContext db = new StudentsDBContext())
            {
                int? stuffId = db.STAFF_CARDS.FirstOrDefault(sc => sc.IDENTIFIER == cardId).STAFF_ID;
                return stuffId.HasValue && stuffId.Value != 0;
            }
        }
    }
}
