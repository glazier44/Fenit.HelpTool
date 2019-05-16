using Fenit.HelpTool.UI.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Fenit.HelpTool.Module.Menu.ViewModels
{
    public class MenuViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public MenuViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommand(regionManager);
        }

        public DelegateCommand SQLCommand { get; set; }
        public DelegateCommand ShifterCommand { get; set; }
        public DelegateCommand TrayCommand { get; set; }
        public DelegateCommand SettingsCommand { get; set; }

        

        private void RegisterCommand(IRegionManager regionManager)
        {
            SQLCommand = new DelegateCommand(() => regionManager.Activate(ViewReservoir.SqlLogModule.Main));
            ShifterCommand = new DelegateCommand(() => regionManager.Activate(ViewReservoir.ShifterModule.Main));
            SettingsCommand = new DelegateCommand(() => regionManager.Activate(ViewReservoir.SettingsModule.Main));

            //TrayCommand = new DelegateCommand(() => regionManager.Activate(ViewReservoir.SqlLogModule.Main));
        }
    }
}