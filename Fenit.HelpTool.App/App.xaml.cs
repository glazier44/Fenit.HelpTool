using System.Windows;
using CommonServiceLocator;
using Fenit.HelpTool.App.Login;
using Fenit.HelpTool.Core.Logger;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.UserService;
using Fenit.HelpTool.Module.SqlLog;
using Fenit.HelpTool.UI.Core.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;

namespace Fenit.HelpTool.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private bool _rootMode;

        private void AppStartup(object sender, StartupEventArgs e)
        {
            for (var i = 0; i != e.Args.Length; ++i)
                if (e.Args[i] == "-r")
                    _rootMode = true;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserService, UserService>();

            containerRegistry.RegisterSingleton<ILoggerService, LoggerService>();

            
        }

        protected override Window CreateShell()
        {
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<LoggedInEvent>()
                .Subscribe(AfterLogin, ThreadOption.UIThread, false);
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<LoggedOutEvent>()
                .Subscribe(LogOut, ThreadOption.UIThread, false);
            if (!_rootMode) Login();

            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        private void AfterLogin(bool val)
        {
            Current.MainWindow.Show();
        }

        private void LogOut(bool user)
        {
            Current.MainWindow.Hide();
            Login();
        }


        private void Login()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var logger = ServiceLocator.Current.GetInstance<ILoggerService>();

            var model = new LoginViewModel(eventAggregator, userService, logger);
            var loginWindow = new LoginView(model) {Width = 300, Height = 300};
            loginWindow.ShowDialog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //  moduleCatalog.AddModule<ModuleLogin>();
            moduleCatalog.AddModule<ModuleSqlLog>();
        }
    }
}