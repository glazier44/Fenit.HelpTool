using System.Windows;
using Fenit.HelpTool.Module.Login;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IModuleManager _moduleManager;
        readonly IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;

        public MainWindow(IModuleManager moduleManager, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<LoginSentEvent>().Subscribe(LoginReceived, ThreadOption.UIThread);

            _moduleManager.LoadModule("ModuleLogin");
        }

        private void LoginReceived(bool login)
        {
            if (login)
            {
                _regionManager.Regions["ContentRegion"].RemoveAll();
                _moduleManager.LoadModule("ModuleSqlLog");
            }
            else Application.Current.Shutdown();
        }
    }
}