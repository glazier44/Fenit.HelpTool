using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Fenit.HelpTool.UI.Core.Base
{
    public abstract class BaseView : UserControl, IView, INotifyPropertyChanged
    {
      //  protected static ILog _log = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);
        protected BaseView()
        {
        }

        protected BaseView(BaseViewModel baseViewModel)
        {
            Model = baseViewModel;
            DataContext = baseViewModel;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expr)
        {
            var body = expr.Body as MemberExpression;
            if (body != null)
            {
                OnPropertyChanged(body.Member.Name);
            }
        }

        public BaseViewModel Model { get; set; }
        protected void LogError(string name, Exception e)
        {
          //  _log.Error(name, e.Message, e);
        }

        protected void LogInfo(string name, string message)
        {
           // _log.Info(name, message);
        }
        protected void MessageError(Exception e, string name)
        {
            LogError(name, e);
            MessageBox.Show(e.Message, "Peris", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

