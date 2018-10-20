using System.Windows;
using Fenit.HelpTool.Login;
using Prism.Events;
using Prism.Modularity;

namespace Fenit.HelpTool.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IModuleManager _moduleManager;
        readonly IEventAggregator _eventAggregator;

        public MainWindow(IModuleManager moduleManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<LoginSentEvent>().Subscribe(LoginReceived, ThreadOption.UIThread);

            _moduleManager.LoadModule("ModuleLogin");
        }

        private void LoginReceived(bool login)
        {
            if (login) _moduleManager.LoadModule("ModuleLogin");
            else Application.Current.Shutdown();
        }
    }
}