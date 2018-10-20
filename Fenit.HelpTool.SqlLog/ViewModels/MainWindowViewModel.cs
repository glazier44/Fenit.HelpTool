using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Fenit.HelpTool.SqlLog.ViewModels
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        private string _userName, _password, _message;
        readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            //LoginCommand = new DelegateCommand(Login, CanLogin);
            //ExitCommand = new DelegateCommand(Close);
        }

        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        //private void Login()
        //{
        //    if (_userService.Login(UserName, Password).Value)
        //    {
        //        _eventAggregator.GetEvent<LoginSentEvent>().Publish(true);
        //    }
        //    else
        //    {
        //        Message = "asdasdasdasd";
        //    }
        //}

        //private void Close()
        //{
        //    _eventAggregator.GetEvent<LoginSentEvent>().Publish(false);
        //}

        //private bool CanLogin()
        //{
        //    return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        //}
    }
}