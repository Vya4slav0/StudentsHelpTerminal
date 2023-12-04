using StudentsHelpTerminal.Models.Other;
using System;
using System.IO;
using System.Linq;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class Logger
    {
        private static readonly StreamWriter swLog;
        private static readonly FileInfo logFileInfo;

        static Logger()
        {
            //Create log if doesn't exist
            if (!File.Exists(Properties.Settings.Default.PathToLogFile))
                swLog = new StreamWriter(File.Create(Properties.Settings.Default.PathToLogFile));
            else
                swLog = new StreamWriter(Properties.Settings.Default.PathToLogFile, true);

            logFileInfo = new FileInfo(Properties.Settings.Default.PathToLogFile);
        }

        public static void WriteLog(Student student)
        {
            swLog.WriteLine(string.Format("{0} - {1} {2} - {3}",
                student.CardID,
                student.Name,
                student.Surname,
                DateTime.Now.ToString("dd.MM.yyyy, HH:mm")));
        }

        public static void ClearLogIfFull()
        {
            //Check if log is too large
            if (!(logFileInfo.Length > Properties.Settings.Default.MaxLogSizeMB * 1048576)) return;
            string[] lines = File.ReadAllLines(logFileInfo.FullName);
            File.WriteAllLines(logFileInfo.FullName, lines.Skip(lines.Length / 2).ToArray());
        }

        public static void CloseLogger()
        {
            swLog.Close();
        }
    }
}
