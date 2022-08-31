using Fenit.HelpTool.Module.Settings.Views;
using Fenit.HelpTool.UI.Core;
using Fenit.Toolbox.WPF.UI;
using Fenit.Toolbox.WPF.UI.Base;
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
            regionManager.Initialize(containerProvider.Resolve<ShifterConfigSettingsWindow>(), ViewReservoir.SettingsModule.ShifterConfigSettings);
        }

        public override void RegisterModuleTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
        }
    }
}