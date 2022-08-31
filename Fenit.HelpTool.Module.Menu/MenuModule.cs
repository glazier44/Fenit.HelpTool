﻿using Fenit.HelpTool.UI.Core;
using Fenit.Toolbox.WPF.UI;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.Module.Menu
{
    public class MenuModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.Initialize(containerProvider.Resolve<Views.Menu>(), ViewReservoir.MenuModule.Menu, ViewReservoir.Regions.MenuRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Menu>();
        }
    }
}