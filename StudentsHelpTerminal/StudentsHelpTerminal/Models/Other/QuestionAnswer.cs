using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsHelpTerminal.Models.Other
{
    internal class QuestionAnswer
    {
        public QuestionAnswer(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }

        public string Question { get; set; } 
        public string Answer { get; set;}
    }
}
