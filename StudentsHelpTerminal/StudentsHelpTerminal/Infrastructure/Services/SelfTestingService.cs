using DialogBoxes;
using StudentsHelpTerminal.Models.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SettingsEditor.Models;
using SettingsEditor.Infrastructure.Services;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class SelfTestingService
    {
        private static string[] ErrorsMessages = 
        {
            "Нет доступа/отсутствует файл настроек приложения",
            "Ошибка подключения к базе данных",
            "Ошибка при чтении файла с данными администраторов",
            "Ошибка при чтении файла с частыми вопросами",
            "Папка с картами колледжа не существует",
            "Папка со справочными документами не существует"
        };

        public static byte AllFunctionsCheck()
        {
            string adminsException;
            string QAException;

            bool[] checkResults =
            {
                !AppSettingsFileCheck(),
                !DBCheck(),
                !(AdministratorsFileCheck(out adminsException) || adminsException != string.Empty),
                !(QAFileCheck(out QAException) || QAException != string.Empty),
                !MapsDirectoryCheck(),
                !MainInfoDirectoryCheck(),
            };

            byte errorCode = ConvertBoolArrayToByte(checkResults);
            if (errorCode != 0)
            {
                List<string> messages = new List<string>();
                int index = 0;
                foreach (bool error in checkResults)
                {
                    if (error)
                    {
                        string message = ErrorsMessages[index];
                        switch (index)
                        {
                            case 1:
                                message = AppendErrorDescription(message, adminsException);
                                break;
                            case 2:
                                message = AppendErrorDescription(message, QAException);
                                break;
                        }

                        messages.Add(message);
                    }
                    index++;
                }
                new AlertBox("Обнаружены ошибки: \n" + string.Join("\n", messages)).ShowDialog();
            }

            return errorCode;
        }
        public static bool AppSettingsFileCheck()
        {
            return File.Exists(SettingsInteractor.PathToSettingsFile);
        }
        public static bool DBCheck()
        {
            using (StudentsDBContext db = new StudentsDBContext())
            {
                return db.Database.Exists();
            }
        }
        public static bool AdministratorsFileCheck(out string exceptionMessage)
        {
            return XMLFileCheck(SettingsInteractor.GetSettingValueByName("PathToAdminsFile"), out exceptionMessage);
        }
        public static bool AdministratorsFileCheck()
        {
            return XMLFileCheck(SettingsInteractor.GetSettingValueByName("PathToAdminsFile"), out _);
        }
        public static bool QAFileCheck(out string exceptionMessage)
        {
            return XMLFileCheck(SettingsInteractor.GetSettingValueByName("PathToQAFile"), out exceptionMessage);
        }
        public static bool QAFileCheck()
        {
            return XMLFileCheck(SettingsInteractor.GetSettingValueByName("PathToQAFile"), out _);
        }
        public static bool MapsDirectoryCheck()
        {
            return Directory.Exists(SettingsInteractor.GetSettingValueByName("PathToCollegeMapsFolder"));
        }
        public static bool MainInfoDirectoryCheck()
        {
            return Directory.Exists(SettingsInteractor.GetSettingValueByName("PathToMainFolder"));
        }

        private static bool XMLFileCheck(string path, out string exceptionMessage)
        {
            exceptionMessage = string.Empty;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                return xmlDoc.DocumentElement != null;
            }
            catch (Exception ex) 
            { 
                exceptionMessage = ex.Message;
                new AlertBox($"Ошибка: {ex.Message}").ShowDialog();
            }
            return false;
        }      
        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;

            int index = 8 - source.Length;

            foreach (bool b in source)
            {
                if (b) result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }
        private static string AppendErrorDescription(string error, string description)
        {
            return error + (description == string.Empty ? "" : $": {description}".ToLower());
        }
    }
}
