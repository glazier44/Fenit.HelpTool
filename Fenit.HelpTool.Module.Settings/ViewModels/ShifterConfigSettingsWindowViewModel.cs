using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Settings;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Commands;

namespace Fenit.HelpTool.Module.Settings.ViewModels
{
    public class ShifterConfigSettingsWindowViewModel : BaseViewModel
    {
        //private readonly OpenDialog _openDialog;
        private readonly ISerializationService _serializationService;

        private ShifterConfigSettings _shifterConfig;

        public ShifterConfigSettingsWindowViewModel(ILoggerService log, ISerializationService serializationService) :
            base(log)
        {
            _serializationService = serializationService;
            CreateCommand();
            //_openDialog = new OpenDialog();
            LoadData();
        }

        public ShifterConfigSettings ShifterConfigSettings
        {
            get => _shifterConfig;
            set => SetProperty(ref _shifterConfig, value);
        }


        public DelegateCommand<int?> CloneCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        private void LoadData()
        {
            ShifterConfigSettings = _serializationService.LoadShifterConfigSettings();
        }

        private void CreateCommand()
        {
            //CloneCommand = new DelegateCommand<int?>(Clone);
            //CancelCommand = new DelegateCommand(CancelCopy, CanCancel);
        }
    }
}