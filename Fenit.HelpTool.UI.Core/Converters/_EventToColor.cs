//using System;
//using System.Globalization;
//using System.Windows.Data;
//using System.Windows.Media;

//namespace Fenit.HelpTool.UI.Core.Converters
//{
//    public class EventToColor : IValueConverter
//    {
//        #region Implementation of IValueConverter
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (!(value is EventReport)) return Brushes.Black;
//            if (value is EventMessage)
//            {
//                var eventMessage = value as EventMessage;
//                if (eventMessage.IsSelected)
//                {
//                    return Brushes.Gray;
//                }
//                if (eventMessage.EventType == EventType.Loitering)
//                    return new BrushConverter().ConvertFrom("#e78799");

//                if (eventMessage.EventType == EventType.LeavingLuggage)
//                    return new BrushConverter().ConvertFrom("#ff848f");

//                if (eventMessage.EventType == EventType.Running)
//                    return new BrushConverter().ConvertFrom("#99FF84");

//                if (eventMessage.EventType == EventType.SlipFall)
//                    return new BrushConverter().ConvertFrom("#F3FF84");
//                return Brushes.Black;
//            }
//            var eventReport = value as EventReport;

//            if (eventReport.EventType == EventType.Detection)
//            {
//                if (eventReport.DeviceName == "CDAQ" && eventReport.CustomDataStr.StartsWith("Sodar"))
//                {
//                    if (eventReport.CustomDataStr.StartsWith("Sodar;micPairNr=1"))
//                        return Brushes.Orange;
//                    if (eventReport.CustomDataStr.StartsWith("Sodar;micPairNr=0"))
//                        return Brushes.Yellow;
//                    if (eventReport.CustomDataStr.StartsWith("Sodar;Merged"))
//                        return Brushes.DarkRed; 
//                }
//                else if (eventReport.DeviceName == "CDAQ" && eventReport.CustomDataStr.StartsWith("Microflown"))
//                {
//                    if (eventReport.CustomDataStr.StartsWith("Microflown;Merged"))
//                        return Brushes.DarkRed;                    
//                    return Brushes.Red; 
//                }                
//                else if (eventReport.DeviceName == "GRIDS-UNIT")
//                    return Brushes.ForestGreen;
//                return Brushes.SlateGray;
//            }
//            if (eventReport.EventType == EventType.Unknown)
//                return Brushes.Yellow;
//            /*if (eventReport == EventType.Diagnostic)
//                    return Brushes.Green;*/
//            if (eventReport.EventType == EventType.Unknown)
//                return Brushes.Teal;


//            return Brushes.Black;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion
//    }
//}