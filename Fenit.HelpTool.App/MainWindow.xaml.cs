using System.Windows;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.SqlLite;
using Fenit.HelpTool.UI.Core;
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

            eventAggregator.GetEvent<LoggedInEvent>().Subscribe(LoadApp, ThreadOption.UIThread);
            eventAggregator.GetEvent<CloseEvent>().Subscribe(CloseApp, ThreadOption.UIThread);
        }

        private void LoadApp()
        {
            _regionManager.ClearRegion("ContentRegion");

            _moduleManager.LoadModule("FooterModule");
            _moduleManager.LoadModule("MenuModule");

            _moduleManager.LoadModule("SqlLogModule");


            _regionManager.Activate(ViewReservoir.MenuModule.Menu, ViewReservoir.Regions.MenuRegion);
            _regionManager.Activate(ViewReservoir.FooterModule.Footer, ViewReservoir.Regions.FooterRegion);

            _regionManager.Activate(ViewReservoir.SqlLogModule.Main);
        }

        private void CloseApp()
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (UserService.IsRootMode)
                LoadApp();
            else
                _moduleManager.LoadModule("ModuleLogin");


            test.Test();
        }
    }
}