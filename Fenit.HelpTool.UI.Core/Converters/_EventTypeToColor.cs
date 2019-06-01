//using System;
//using System.Globalization;
//using System.Windows.Data;
//using System.Windows.Media;

//namespace Fenit.HelpTool.UI.Core.Converters
//{
//    public class EventTypeToColor : IValueConverter
//    {
//        #region Implementation of IValueConverter
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (value is EventType)
//            {
//                var evStatus = value as EventType;

//                if (evStatus == EventType.Detection)
//                    return Brushes.Red;
//                if (evStatus == EventType.Unknown)
//                    return Brushes.Yellow;
//                if (evStatus == EventType.Diagnostic)
//                    return Brushes.Green;
//                if (evStatus == EventType.Unknown)
//                    return Brushes.Teal;

//                return Brushes.Black;
//            }

//            return Brushes.Black;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}