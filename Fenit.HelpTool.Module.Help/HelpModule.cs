using Fenit.HelpTool.Module.Help.Views;
using Fenit.HelpTool.UI.Core;
using Fenit.Toolbox.WPF.UI;
using Fenit.Toolbox.WPF.UI.Base;
using Prism.Ioc;
using Prism.Regions;
using Unity;

namespace Fenit.HelpTool.Module.Help
{
    public class HelpModule : BaseModule
    {
        public HelpModule(IUnityContainer container) : base(container)
        {
        }


        public override void OnInitializedModule(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.Initialize(containerProvider.Resolve<MainWindow>(), ViewReservoir.HelpModule.Main);
            regionManager.Initialize(containerProvider.Resolve<AboutWindow>(), ViewReservoir.HelpModule.About);
        }

        public override void RegisterModuleTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
        }
    }
}