using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Settings;
using Fenit.HelpTool.UI.Core.Base;
using Fenit.HelpTool.UI.Core.Dialog;
using Fenit.HelpTool.UI.Core.Events;
using Fenit.HelpTool.UI.Core.Events.KeyBinding;
using Prism.Commands;
using Prism.Events;

namespace Fenit.HelpTool.Module.Settings.ViewModels
{
    public class ShifterConfigSettingsWindowViewModel : BaseViewModel
    {
        private readonly ISerializationService _serializationService;
        private readonly OpenDialog _openDialog;
        private readonly IEventAggregator _eventAggregator;

        private ShifterConfigSettings _shifterConfig;

        public ShifterConfigSettingsWindowViewModel(ILoggerService log, ISerializationService serializationService, IEventAggregator eventAggregator) :
            base(log)
        {
            _eventAggregator = eventAggregator;
            _serializationService = serializationService;
            CreateCommand();
            _openDialog = new OpenDialog();
            eventAggregator.GetEvent<SaveKeyBindingEvent>().Subscribe(Save, ThreadOption.UIThread);

            LoadData();
        }

        public ShifterConfigSettings ShifterConfigSettings
        {
            get => _shifterConfig;
            set => SetProperty(ref _shifterConfig, value);
        }

        public DelegateCommand OpenSourcePathCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        private void LoadData()
        {
            ShifterConfigSettings = _serializationService.LoadShifterConfigSettings();
        }

        private string GetDir() //TODOTK FRD
        {
            var res = _openDialog.SelectFolder();
            if (res.IsSucces) return res.Value;
            return string.Empty;
        }

        private void CreateCommand()
        {
            OpenSourcePathCommand = new DelegateCommand(() =>
            {
                ShifterConfigSettings.ConfigPath = GetDir();
            });
            SaveCommand = new DelegateCommand(Save);
            //CancelCommand = new DelegateCommand(CancelCopy, CanCancel);
        }
        private bool Valid()
        {
            //TODOTK
            return true;
        }
        private void Save()
        {
            if (!Valid())
            {
                MessageWarning("Prosze uzupełnić wszystkie pola", "Uwaga!");
                return;
            }

            if (ShifterConfigSettings != null)
            {
                _serializationService.SaveShifterConfigSettings(ShifterConfigSettings);
                _eventAggregator.GetEvent<ReloadShiferList>().Publish();
            }
        }

    }
}