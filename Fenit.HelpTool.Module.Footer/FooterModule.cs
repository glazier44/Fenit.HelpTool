using Fenit.HelpTool.Module.Footer.Views;
using Fenit.HelpTool.UI.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.Module.Footer
{
    public class FooterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.Initialize(containerProvider.Resolve<FooterView>(), ViewReservoir.FooterModule.Footer,
                ViewReservoir.Regions.FooterRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FooterView>();
        }
    }
}