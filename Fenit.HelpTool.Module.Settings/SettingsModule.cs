using Fenit.HelpTool.Module.Settings.Views;
using Fenit.HelpTool.UI.Core;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Ioc;
using Prism.Regions;
using Unity;

namespace Fenit.HelpTool.Module.Settings
{
    public class SettingsModule : BaseModule
    {
        public SettingsModule(IUnityContainer container) : base(container)
        {
        }

        public override void OnInitializedModule(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.Initialize(containerProvider.Resolve<MainWindow>(), ViewReservoir.SettingsModule.Main);
        }

        public override void RegisterModuleTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
        }
    }
}