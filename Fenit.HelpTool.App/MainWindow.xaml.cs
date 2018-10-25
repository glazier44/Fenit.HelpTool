using System.Windows;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.UI.Core.Events;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IModuleManager _moduleManager;
        private readonly IRegionManager _regionManager;
        protected IUserService UserService;

        public MainWindow(IModuleManager moduleManager, IEventAggregator eventAggregator, IRegionManager regionManager,
            IUserService userService)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _regionManager = regionManager;
            UserService = userService;

            eventAggregator.GetEvent<LoggedInEvent>().Subscribe(LoginReceived, ThreadOption.UIThread);
            eventAggregator.GetEvent<CloseEvent>().Subscribe(CloseApp, ThreadOption.UIThread);

            if (UserService.IsRootMode)
            {
                LoginReceived();
            }
            else
            {
                _moduleManager.LoadModule("ModuleLogin");
            }
        }

        private void LoginReceived()
        {
            _moduleManager.LoadModule("ModuleSqlLog");
            _moduleManager.LoadModule("ModuleFooter");

            //_moduleManager.LoadModule("ModuleSqlLog");
            //var mainRegion = _regionManager.Regions["ContentRegion"];
            ////mainRegion.RequestNavigate("ModuleSqlLog");
            //var mainRegion = m_ViewModel.RegionManager.Regions["MainRegion"];
            //ModuleServices.ClearRegion(mainRegion);

            //_regionManager.RequestNavigate("ViewMainFrame", ModuleSqlLog);

            //_moduleManager.LoadModule("ModuleFooter");
            //var footerRegion = _regionManager.Regions["FooterRegion"];
            //footerRegion.RequestNavigate("ModuleSqlLog");
        }

        private void CloseApp()
        {
            Application.Current.Shutdown();
        }
    }
}