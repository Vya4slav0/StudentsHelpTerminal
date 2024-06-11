using SettingsEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SettingsEditor.Infrastructure.Services
{
    public class SettingsManagerService
    {
        private readonly XDocument _settingsXML;
        private readonly string _pathToSettingsXML;

        public SettingsManagerService(string pathToSettingsXML) 
        {
            _settingsXML = XDocument.Load(pathToSettingsXML);
            _pathToSettingsXML = pathToSettingsXML;
        }

        public Setting LoadSettingByName(string name)
        {
            XElement root = _settingsXML.Root;
            XElement node = root.Elements("setting").First(s => s.Attribute("name").Value == name);
            return new Setting
            {
                Name = name,
                Type = Type.GetType(node.Element("type").Value),
                Value = node.Element("value").Value,
                Description = node.Element("description").Value
            };
        }

        public List<Setting> LoadAllSettings()
        {
            XElement root = _settingsXML.Root;
            return root.Elements("setting")
                .Select(s => new Setting 
                {
                    Name = s.Attribute("name").Value,
                    Type = Type.GetType(s.Element("type").Value),
                    Value = s.Element("value").Value,
                    Description = s.Element("description").Value
                })
                .ToList();
        }

        public void SaveSettings(List<Setting> newSettings)
        {
            XElement root = _settingsXML.Root;
            foreach (XElement setting in root.Elements())
            {
                setting.Element("value").Value = newSettings.First(s => s.Name == setting.Attribute("name").Value).Value.ToString();
            }
            _settingsXML.Save(_pathToSettingsXML);
        }
    }
}
