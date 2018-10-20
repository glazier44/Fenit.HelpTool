﻿using System.Windows;
using Fenit.HelpTool.App.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Fenit.HelpTool.Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase, INavigationAware
    {
        private string _userName, _password, _message;
        private readonly IUserService _userService;
        public LoginWindowViewModel(IUserService userService)
        {
            _userService = userService;
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

        private void Login()
        {
            if (_userService.Login(UserName, Password).Value)
            {
                Message = "ok";

            }
            else
            {
                Message = "asdasdasdasd";
            }
        }

        private void Close()
        {
            Application.Current.Shutdown();
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }
    }
}