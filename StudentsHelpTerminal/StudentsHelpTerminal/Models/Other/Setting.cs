using StudentsHelpTerminal.Properties;
using System;
using System.Resources;

namespace StudentsHelpTerminal.Models.Other
{
    internal class Setting
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }
        public string Description { get => new ResourceManager(typeof(Resources)).GetString(Name); }
    }
}
