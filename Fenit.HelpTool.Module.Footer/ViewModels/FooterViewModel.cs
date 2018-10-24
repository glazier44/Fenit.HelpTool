using System.Diagnostics;
using System.Reflection;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Regions;

namespace Fenit.HelpTool.Module.Footer.ViewModels
{
    public class FooterViewModel : BaseViewModel //, INavigationAware
    {
        private string _version, _userName;


        public FooterViewModel(ILoggerService log) : base(log)
        {
            Title = "User Control 1";

            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            var version = versionInfo.ProductVersion;
            var user = "testowy";
            _version = $"Wersja: {version}";
            _userName = $"Użytkownik: {user}"; ;
        }


        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }


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
            // throw new System.NotImplementedException();
        }
    }
}