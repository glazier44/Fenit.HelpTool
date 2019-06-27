using System.Diagnostics;
using System.Reflection;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Events;

namespace Fenit.HelpTool.Module.Footer.ViewModels
{
    public class FooterViewModel : BaseViewModel
    {
        private bool _footerVisibility;
        private string _version, _log;

        public FooterViewModel(ILoggerService log, IEventAggregator eventAggregator) : base(log)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            var version = versionInfo.ProductVersion;
            _version = $"Wersja: {version}";
            _footerVisibility = true;
        }

        public bool FooterVisibility
        {
            get => _footerVisibility;
            set => SetProperty(ref _footerVisibility, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public string Log
        {
            get => _log;
            set => SetProperty(ref _log, value);
        }
    }
}