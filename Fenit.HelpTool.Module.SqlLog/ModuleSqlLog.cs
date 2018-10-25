using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.SqlFileService;
using Fenit.HelpTool.Module.SqlLog.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.Module.SqlLog
{
    public class ModuleSqlLog : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(MainWindow));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
            containerRegistry.RegisterSingleton<ISqlFileService, SqlFileService>();
        }
    }
}