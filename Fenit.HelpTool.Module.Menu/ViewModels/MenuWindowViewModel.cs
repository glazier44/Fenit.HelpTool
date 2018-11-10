using Fenit.HelpTool.UI.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Fenit.HelpTool.Module.Menu.ViewModels
{
    public class MenuWindowViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MenuWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommand(regionManager);
        }

        public DelegateCommand TestShowCommand { get; set; }


        private void RegisterCommand(IRegionManager regionManager)
        {
            TestShowCommand = new DelegateCommand(() => regionManager.Activate(ViewReservoir.SqlLogModule.Main));
        }
    }
}