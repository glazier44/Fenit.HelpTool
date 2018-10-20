using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Fenit.HelpTool.UI.Core.Converters
{
    public class DoubleFormatConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((value.GetType() == typeof(double) || value.GetType() == typeof(float)))
                {
                    return ((double)value).ToString("F1", CultureInfo.InvariantCulture);
                }
                if (value is string)
                {
                    var d = double.Parse((string)value, NumberStyles.Float, CultureInfo.InvariantCulture);
                    return d.ToString("F1", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                //_log.WarnFormat("Convert", "{0} {1} {2} {3}", value, targetType, parameter, culture);
                return "unknown";
            }

            throw new ArgumentException("value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}