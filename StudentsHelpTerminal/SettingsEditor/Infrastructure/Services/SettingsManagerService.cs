using SettingsEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SettingsEditor.Infrastructure.Services
{
    public class SettingsManagerService
    {
        private readonly XDocument _settingsXML;
        private readonly string _pathToSettingsXML;

        /// <summary>
        /// Creates new SettingsManagerService object that operates with .xml settings file 
        /// </summary>
        /// <param name="pathToSettingsXML">Path to .xml settings file</param>
        public SettingsManagerService(string pathToSettingsXML) 
        {
            _settingsXML = XDocument.Load(pathToSettingsXML);
            _pathToSettingsXML = pathToSettingsXML;
        }

        /// <summary>
        /// Returns SettingsEditor.Models.Setting from loaded .xml settings file by setting's name
        /// </summary>
        /// <param name="name">Name of setting</param>
        /// <returns>SettingsEditor.Models.Setting</returns>s
        public Setting LoadSettingByName(string name)
        {
            XElement root = _settingsXML.Root;
            XElement[] allSections = root.Elements("section").ToArray();
            foreach (XElement section in allSections)
            {
                XElement node = section.Elements("setting").First(s => s.Attribute("name").Value == name);
                return new Setting
                {
                    Name = name,
                    Type = Type.GetType(node.Element("type").Value),
                    Value = node.Element("value").Value,
                    Description = node.Element("description").Value,
                    Section = section.Attribute("visibleName").Value
                };
            }
            return null;
        }

        /// <summary>
        /// Loads all settings from loaded .xml file
        /// </summary>
        /// <returns>IEnumerable of settings</returns>
        public IEnumerable<Setting> LoadAllSettings()
        {
            XElement root = _settingsXML.Root;
            return root.Elements("section").Elements("setting")
                .Select(s => new Setting
                {
                    Name = s.Attribute("name").Value,
                    Type = Type.GetType(s.Element("type").Value),
                    Value = s.Element("value").Value,
                    Description = s.Element("description").Value,
                    Section = s.Parent.Attribute("visibleName").Value
                });
        }

        /// <summary>
        /// Saves all settings from settings list to .xml settings file
        /// </summary>
        /// <param name="newSettings">List of settings</param>
        public void SaveSettings(IEnumerable<Setting> newSettings)
        {
            XElement root = _settingsXML.Root;
            foreach (XElement setting in root.Elements("section").Elements())
            {
                setting.Element("value").Value = newSettings.First(s => s.Name == setting.Attribute("name").Value).Value.ToString();
            }
            _settingsXML.Save(_pathToSettingsXML);
        }
    }
}
