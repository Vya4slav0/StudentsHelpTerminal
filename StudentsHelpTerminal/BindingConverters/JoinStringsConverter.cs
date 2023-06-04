using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BindingConverters
{
    public class JoinStringsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(parameter.ToString(), values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var separator = parameter as string ?? " ";
            return (value as string)?.Split(new[] { separator }, StringSplitOptions.None).Cast<object>().ToArray();
        }
    }
}
