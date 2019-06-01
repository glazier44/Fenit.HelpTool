using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Fenit.HelpTool.UI.Core.Converters
{
    public class ShifterItemForegroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isArchive)
            {
                return isArchive ? Brushes.Gray : Brushes.Black;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}