using System.Windows;
using CommonServiceLocator;
using Fenit.HelpTool.Core.SerializationService.Implement;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.ShifterService.Implement;
using Fenit.HelpTool.Module.Footer;
using Fenit.HelpTool.Module.Help;
using Fenit.HelpTool.Module.Menu;
using Fenit.HelpTool.Module.Runner;
using Fenit.HelpTool.Module.Settings;
using Fenit.HelpTool.Module.Shifter;
using Fenit.HelpTool.Module.SqlLog;
using Fenit.Toolbox.Logger;
using Prism.Ioc;
using Prism.Modularity;

namespace Fenit.HelpTool.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            //for (var i = 0; i != e.Args.Length; ++i)
            //    if (e.Args[i] == "-r")
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoggerService, LoggerService>();
            containerRegistry.RegisterSingleton<ISerializationService, SerializationService>();
            containerRegistry.RegisterSingleton<IShifterService, ShifterService>();
        }

        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }


        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            AddModule<SqlLogModule>(moduleCatalog);
            AddModule<FooterModule>(moduleCatalog);
            AddModule<MenuModule>(moduleCatalog);
            AddModule<ShifterModule>(moduleCatalog);
            AddModule<SettingsModule>(moduleCatalog);
            AddModule<RunnerModule>(moduleCatalog);
            AddModule<HelpModule>(moduleCatalog);
        }

        private void AddModule<T>(IModuleCatalog moduleCatalog)
            where T : IModule
        {
            var module = typeof(T);
            moduleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = module.Name,
                ModuleType = module.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }
    }
}