using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fenit.HelpTool.Login.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.Login
{
    public class ModuleLogin : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(LoginWindow));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginWindow>();
            //containerRegistry.RegisterForNavigation<PersonDetail>();
        }
    }
}
