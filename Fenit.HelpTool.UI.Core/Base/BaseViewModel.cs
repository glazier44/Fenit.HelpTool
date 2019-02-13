using System;
using System.Windows;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.Service.Abstract;
using Prism.Mvvm;

namespace Fenit.HelpTool.UI.Core.Base
{
    public abstract class BaseViewModel : BindableBase //, INotifyPropertyChanged
    {
        protected readonly ILoggerService Log;

        protected BaseViewModel(ILoggerService log)
        {
            Log = log;
        }

        //public string Title { get; protected set; }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}

        //protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expr)
        //{
        //    if (expr.Body is MemberExpression body)
        //    {
        //        OnPropertyChanged(body.Member.Name);
        //    }
        //}

        protected void MessageError(string message, string name)
        {
            Log.Error($"[{name}] {message}");
            MessageBox.Show(message, "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected void MessageError(Exception e, string name)
        {
            Log.Error(name, e);
            MessageBox.Show(e.Message, "B³¹d", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}