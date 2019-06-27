using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.UI.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Fenit.HelpTool.Module.Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _userName, _password, _message;

        public LoginWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            ExitCommand = new DelegateCommand(Close);
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


        private void Login()
        {
            if (_userService.Login(UserName, Password).Value)
                _eventAggregator.GetEvent<LoggedInEvent>().Publish();
            else
                Message = "asdasdasdasd";
        }

        private void Close()
        {
            _eventAggregator.GetEvent<CloseEvent>().Publish();
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }
    }
}