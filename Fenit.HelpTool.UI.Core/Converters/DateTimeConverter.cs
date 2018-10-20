using System;
using System.Globalization;
using System.Windows.Data;

namespace Fenit.HelpTool.UI.Core.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime)
            {
                var time = (DateTime)value;
                return time.ToString("yyyy-MM-dd HH:mm:ss");
            }

            throw new ArgumentException("value is of wrong type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}