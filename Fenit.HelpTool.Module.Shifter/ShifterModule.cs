using Fenit.HelpTool.Module.Shifter.ViewModels;
using Fenit.HelpTool.Module.Shifter.Views;
using Fenit.HelpTool.UI.Core;
using Fenit.Toolbox.WPF.UI;
using Fenit.Toolbox.WPF.UI.Base;
using Prism.Ioc;
using Prism.Regions;
using Unity;

namespace Fenit.HelpTool.Module.Shifter
{
    public class ShifterModule : BaseModule
    {
        public ShifterModule(IUnityContainer container) : base(container)
        {
        }


        public override void OnInitializedModule(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.Initialize(containerProvider.Resolve<MainWindow>(), ViewReservoir.ShifterModule.Main);

        }

        public override void RegisterModuleTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
            RegisterDialog<MessageWindow, MessageViewModel>(ViewReservoir.ShifterModule.MessageWindow);
        }
    }
}