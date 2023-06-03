using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace StudentsHelpTerminal.Infrastructure.Services
{
    internal static class AdminAutorizer
    {
        private static string _AdminPasswordHash = string.Empty;

        public static bool CheckAdminByStuffId(int id)
        {
            string pathToAdminsFile = @"./Administrators.xml";
            if (!File.Exists(pathToAdminsFile)) return false;

            XmlDocument adminsXML = new XmlDocument();
            adminsXML.Load(pathToAdminsFile);
            XmlElement root = adminsXML.DocumentElement;
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Attributes["id"].Value == id.ToString())
                    {
                        _AdminPasswordHash = node.ChildNodes[0].InnerText;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckPassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(data);

            string hashString = string.Empty;
            foreach (byte b in hash)
            {
                hashString += string.Format("{0:x2}", b);
            }
            return _AdminPasswordHash.Equals(hashString);
        }
    }
}
