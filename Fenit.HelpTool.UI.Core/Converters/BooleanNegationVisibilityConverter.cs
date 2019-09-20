using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fenit.HelpTool.UI.Core.Converters
{
    public class BooleanNegationVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value is EventReport)
            //{
            //    var message = value as EventReport;
            //    if (parameter != null && (string)parameter == "EventType")
            //    {
            //        if(value is EventMessage)
            //            return Visibility.Visible;
            //        else
            //            return Visibility.Collapsed;
            //    }
            //    return message.Direction == null ? Visibility.Collapsed : Visibility.Visible;
            //}
            return (bool) value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("BooleanNegationVisibilityConverter");
        }
    }
}