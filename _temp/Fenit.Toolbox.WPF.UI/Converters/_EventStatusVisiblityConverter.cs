//using System;
//using System.Globalization;
//using System.Windows;
//using System.Windows.Data;

//namespace Fenit.HelpTool.UI.Core.Converters
//{
//    public class EventStatusVisiblityConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            var status = value as EventStatus;
//            if (status != null &&
//                (status == EventStatus.Canceled || status == EventStatus.Closed || status == EventStatus.Confirmed))
//            {
//                return Visibility.Visible;
//            }
//            return Visibility.Collapsed;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}