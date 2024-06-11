using StudentsHelpTerminal.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using StudentsHelpTerminal.Infrastructure.Services;

namespace StudentsHelpTerminal.ViewModels
{
    internal class QuestionAnswerPageViewModel : Base.ViewModelBase
    {
        public QuestionAnswerPageViewModel() 
        {
            string pathToQAFile = SettingsInteractor.GetSettingValueByName("PathToQAFile");

            if (!SelfTestingService.QAFileCheck()) return;

            XmlDocument QAXmlDocument = new XmlDocument();
            QAXmlDocument.Load(pathToQAFile);

            XmlElement root = QAXmlDocument.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                string questionText = node.Attributes[0].Value;
                string answerText = node.ChildNodes[0].InnerText;

                QACollection.Add(new QuestionAnswer(questionText, answerText));
            }
        }

        #region Properties

        public List<QuestionAnswer> QACollection { get; set; } = new List<QuestionAnswer>();

        #endregion
    }
}
