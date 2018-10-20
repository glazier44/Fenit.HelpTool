using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using Prism.Mvvm;

namespace Fenit.HelpTool.UI.Core.Base
{
    public abstract class BaseViewModel : BindableBase, INotifyPropertyChanged
    {
        public string Title { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expr)
        {
            if (expr.Body is MemberExpression body)
            {
                OnPropertyChanged(body.Member.Name);
            }
        }

        protected void LogError(string name, Exception e)
        {
          //  _log.Error(name, e.Message, e);
        }

        protected void MessageError(Exception e, string name)
        {
            LogError(name, e);
            MessageBox.Show(e.Message, "Peris", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}