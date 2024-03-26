using System;

namespace SettingsEditor.Models
{
    public class Setting
    {
        private string _name;
        public string Name 
        { 
            get => _name; 
            set => _name = value.Trim(); 
        }

        private object _value;
        public object Value 
        {
            get => _value;
            set 
            {
               _value = value;
            } 
        }
        public Type Type { get; set; }

        private string _description;
        public string Description 
        {
            get => _description; 
            set => _description = value.Trim();
        }
    }
}
