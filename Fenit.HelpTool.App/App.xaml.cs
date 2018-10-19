using System.Windows;
using CommonServiceLocator;
using Fenit.HelpTool.Login;
using Prism.Ioc;
using Prism.Modularity;

namespace Fenit.HelpTool.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleLogin>();

            //var moduleLogin = typeof(ModuleLogin);
            //moduleCatalog.AddModule(new ModuleInfo()
            //{
            //    ModuleName = moduleLogin.Name,
            //    ModuleType = moduleLogin.AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.OnDemand
            //});

            //var modulbType = typeof(ModuleBModule);
            //moduleCatalog.AddModule(new ModuleInfo()
            //{
            //    ModuleName = modulbType.Name,
            //    ModuleType = modulbType.AssemblyQualifiedName,
            //    InitializationMode = InitializationMode.OnDemand
            //});
        }

    }
}
