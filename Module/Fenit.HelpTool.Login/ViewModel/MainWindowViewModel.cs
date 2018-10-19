using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Fenit.HelpTool.Login.ViewModel
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        private string _login, _password, _message;

        public MainWindowViewModel()
        {
            LoginCommand = new DelegateCommand(Login, CanLogin);
        }
        private void Login()
        {
            Message = "asdasdasdasd";
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        public string UserName
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
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
    }
}
