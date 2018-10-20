﻿using System.Windows;
using CommonServiceLocator;
using Fenit.HelpTool.App.Login;
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
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<LoggedInEvent>()
                .Subscribe(AfterLogin, ThreadOption.UIThread, false);
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<LoggedOutEvent>()
                .Subscribe(LogOut, ThreadOption.UIThread, false);


            Login();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserService, UserService>();
        }

        protected override Window CreateShell()
        {
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

            var model = new LoginViewModel(eventAggregator, userService);
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