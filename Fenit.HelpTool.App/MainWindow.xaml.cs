using System.Windows;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly IModuleManager _moduleManager;
        private IRegionManager _regionManager;

        public MainWindow(IModuleManager moduleManager, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<LoggedInEvent>().Subscribe(LoginReceived, ThreadOption.UIThread);
            _eventAggregator.GetEvent<CloseEvent>().Subscribe(CloseApp, ThreadOption.UIThread);


            //ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<CloseEvent>()
            //    .Subscribe(Close, ThreadOption.UIThread, false);
            // _moduleManager.LoadModule("ModuleLogin");
        }

        private void LoginReceived(bool login)
        {
            if (login)
                _moduleManager.LoadModule("ModuleSqlLog");

           // else CloseApp();
        }

        private void CloseApp()
        {
            base.Close();
           // Application.Current.Shutdown();
        }
    }
}