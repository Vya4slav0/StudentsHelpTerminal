using StudentsHelpTerminal.Models.Other;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StudentsHelpTerminal.BindingConverters
{
    internal class DocTypeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == DocumentsListItem.Type.Common.ToString() ? "Общие документы" : "Для вашей группы";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
