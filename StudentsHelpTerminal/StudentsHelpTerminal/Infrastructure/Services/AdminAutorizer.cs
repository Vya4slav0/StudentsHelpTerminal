using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class AdminAutorizer
    {
        private static string adminPasswordHash = string.Empty;
        private static string pathToAdminsFile = SettingsInteractor.GetSettingValueByName("PathToAdminsFile");

        public static bool CheckAdminByStuffId(int id)
        {        
            if (!SelfTestingService.AdministratorsFileCheck()) return false;

            XmlDocument adminsXML = new XmlDocument();
            adminsXML.Load(pathToAdminsFile);
            XmlElement root = adminsXML.DocumentElement;

            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Attributes["id"].Value == id.ToString())
                    {
                        adminPasswordHash = node.ChildNodes[0].InnerText;
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool CheckPassword(string password)
        {
            return adminPasswordHash.Equals(GetHashString(GetPasswordHash(password)));
        }
        public static bool AutorizeAnyAdministrator(string password)
        {
            if (!SelfTestingService.AdministratorsFileCheck()) return false;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToAdminsFile);
            XmlNode root = xmlDocument.DocumentElement;

            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    adminPasswordHash = node.ChildNodes[0].InnerText;
                    if (adminPasswordHash.Equals(GetHashString(GetPasswordHash(password)))) return true;
                }
            }

            return false;
        }

        private static byte[] GetPasswordHash(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(data);
        }
        private static string GetHashString(byte[] hash)
        {
            string hashString = string.Empty;
            foreach(byte b in hash)
            {
                hashString += string.Format("{0:x2}", b);
            }
            return hashString;
        }
    }
}
