using System.Windows;
using System.Windows.Input;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.UI.Core;
using Fenit.HelpTool.UI.Core.Events;
using Fenit.HelpTool.UI.Core.Events.KeyBinding;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IModuleManager _moduleManager;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;

        public MainWindow(IModuleManager moduleManager, IEventAggregator eventAggregator, IRegionManager regionManager,
            IModuleCatalog moduleCatalog,
            IUserService userService)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;

            _userService = userService;

            eventAggregator.GetEvent<LoggedInEvent>().Subscribe(LoadApp, ThreadOption.UIThread);
            eventAggregator.GetEvent<CloseEvent>().Subscribe(CloseApp, ThreadOption.UIThread);
            KeyBinding(eventAggregator);
        }

        private void KeyBinding(IEventAggregator eventAggregator)
        {
            var saveKeyBinding =
                new KeyBinding(
                    new DelegateCommand(() => { eventAggregator.GetEvent<SaveKeyBindingEvent>().Publish(); }), Key.S,
                    ModifierKeys.Control);
            var reloadKeyBinding =
                new KeyBinding(
                    new DelegateCommand(() => { eventAggregator.GetEvent<ReloadKeyBindingEvent>().Publish(); }), Key.F5,
                    ModifierKeys.None);



            InputBindings.Add(saveKeyBinding);
            InputBindings.Add(reloadKeyBinding);

        }

        private void LoadApp()
        {
            _regionManager.ClearRegion("ContentRegion");
            _moduleManager.LoadModuleIfExist(_moduleCatalog, ViewReservoir.FooterModule.Name);
            _moduleManager.LoadModuleIfExist(_moduleCatalog, ViewReservoir.MenuModule.Name);
            _moduleManager.LoadModuleIfExist(_moduleCatalog, ViewReservoir.SqlLogModule.Name);
            _moduleManager.LoadModuleIfExist(_moduleCatalog, ViewReservoir.ShifterModule.Name);
            _moduleManager.LoadModuleIfExist(_moduleCatalog, ViewReservoir.SettingsModule.Name);

            _regionManager.Activate(ViewReservoir.MenuModule.Menu, ViewReservoir.Regions.MenuRegion);
            _regionManager.Activate(ViewReservoir.FooterModule.Footer, ViewReservoir.Regions.FooterRegion);
            _regionManager.Activate(ViewReservoir.SqlLogModule.Main);
        }

        private void CloseApp()
        {
            Application.Current.Shutdown();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_userService.IsRootMode)
                LoadApp();
            else
                _moduleManager.LoadModule("ModuleLogin");
        }
    }
}