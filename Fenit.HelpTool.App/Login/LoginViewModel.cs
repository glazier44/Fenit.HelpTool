using System;
using System.Windows;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.UI.Core.Base;
using Fenit.HelpTool.UI.Core.Events;
using Prism.Commands;
using Prism.Events;

namespace Fenit.HelpTool.App.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserService _userService;

        private bool? _dialogResult;
        private string _userName, _password, _message;

        public LoginViewModel(IEventAggregator eventAggregator, IUserService userService)
        {
            _userService = userService;
            _eventAggregator = eventAggregator;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            ExitCommand = new DelegateCommand(Close);
        }

        public DelegateCommand LoginCommand { get; protected set; }
        public DelegateCommand ExitCommand { get; protected set; }

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged(() => DialogResult);
            }
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

        public event Action RequestClose;


        public void Close()
        {
            //Application.Current.Shutdown();
            _eventAggregator.GetEvent<CloseEvent>().Publish();
        }

        internal void Login()
        {
            if (_userService.Login(UserName, Password).Value)
            {
                // _eventAggregator.GetEvent<LoggedInEvent>().Publish(true);
                DialogResult = true;
                RequestClose.Invoke();
            }
            else
            {
                Message = "asdasdasdasd";
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password);
        }
    }
}