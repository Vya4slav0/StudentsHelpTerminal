using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.Models
{
    internal class Table
    {
        public string Name { get; set; }
        public ICollection<object> Items { get; set; }

        
    }
}
