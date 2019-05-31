using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Event;
using Fenit.HelpTool.Core.Service.Model.Settings;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.HelpTool.Module.Shifter.Model;
using Fenit.HelpTool.UI.Core;
using Fenit.HelpTool.UI.Core.Base;
using Fenit.HelpTool.UI.Core.Dialog;
using Fenit.HelpTool.UI.Core.Events;
using Fenit.HelpTool.UI.Core.Events.KeyBinding;
using Prism.Commands;
using Prism.Events;
using Unity;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly OpenDialog _openDialog;
        private readonly ISerializationService _serializationService;
        private readonly IShifterService _shifterService;
        private readonly IUnityContainer _unityContainer;
        private bool _canCancel;
        private bool _isProgressBarVisible;
        private double _progressValue;
        private ObservableCollection<BaseShifterConfig> _saveList;
        private ShifterConfig _shifterConfig;
        private ShifterConfigSettings _shifterConfigSettings;
        private List<ShifterConfig> _shifterConfigsList;

        public MainWindowViewModel(ILoggerService log, ISerializationService serializationService,
            IShifterService shifterService, IEventAggregator eventAggregator, IUnityContainer unityContainer) :
            base(log)
        {
            _serializationService = serializationService;
            _shifterService = shifterService;
            _unityContainer = unityContainer;
            ShifterConfigClear();
            ReloadData();
            CreateCommand();
            _openDialog = new OpenDialog();
            EventAggregatorSubscribe(eventAggregator);
        }

        public ObservableCollection<BaseShifterConfig> SaveList
        {
            get => _saveList;
            set => SetProperty(ref _saveList, value);
        }

        public ShifterConfig ShifterConfig
        {
            get => _shifterConfig;
            set
            {
                SetProperty(ref _shifterConfig, value);
                RunCommand?.RaiseCanExecuteChanged();
                OpenDestinationPathCommand?.RaiseCanExecuteChanged();
                OpenSourcePathCommand?.RaiseCanExecuteChanged();
            }
        }

        public bool IsProgressBarVisible
        {
            get => _isProgressBarVisible;
            set => SetProperty(ref _isProgressBarVisible, value);
        }

        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public DelegateCommand OpenSourcePathCommand { get; set; }
        public DelegateCommand OpenDestinationPathCommand { get; set; }
        public DelegateCommand<int?> UpComand { get; set; }
        public DelegateCommand<int?> DownComand { get; set; }
        public DelegateCommand<int?> ArchiveComand { get; set; }
        public DelegateCommand<int?> RunThisCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand<int?> SelectCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand { get; set; }
        public DelegateCommand<int?> CloneCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand ReloadCommand { get; set; }

        public List<string> Types => _shifterConfigSettings.AppType;
        public List<string> Versions => _shifterConfigSettings.AppVersion;

        private void EventAggregatorSubscribe(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ProgressEvent>().Subscribe(Progress);
            eventAggregator.GetEvent<SaveKeyBindingEvent>().Subscribe(Save, ThreadOption.UIThread);
            eventAggregator.GetEvent<ReloadKeyBindingEvent>().Subscribe(ReloadData, ThreadOption.UIThread);
            eventAggregator.GetEvent<ReloadShiferList>().Subscribe(ReloadData, ThreadOption.UIThread);
            eventAggregator.GetEvent<RunKeyBindingEvent>().Subscribe(Run, ThreadOption.UIThread);
        }

        private void CreateCommand()
        {
            DeleteCommand = new DelegateCommand<int?>(Delete);
            SelectCommand = new DelegateCommand<int?>(Select);
            SaveCommand = new DelegateCommand(Save);
            ClearCommand = new DelegateCommand(ShifterConfigClear);
            RunThisCommand = new DelegateCommand<int?>(RunThis);
            RunCommand = new DelegateCommand(Run);
            CloneCommand = new DelegateCommand<int?>(Clone);
            CancelCommand = new DelegateCommand(CancelCopy, CanCancel);
            OpenSourcePathCommand = new DelegateCommand(() =>
            {
                ShifterConfig.SourcePath = ExploreOpen(_shifterConfig.SourcePath);
            });
            OpenDestinationPathCommand = new DelegateCommand(() =>
            {
                ShifterConfig.DestinationPath = ExploreOpen(_shifterConfig.DestinationPath);
            });
            DownComand = new DelegateCommand<int?>(ElementDown, CanDown);
            UpComand = new DelegateCommand<int?>(ElementUp, CanUp);
            ArchiveComand = new DelegateCommand<int?>(Archive);
            ReloadCommand = new DelegateCommand(ReloadData);
        }

        private bool CanUp(int? id)
        {
            var (up, @this, down) = SelectShifters(id);
            return up != null && @this != null;
        }

        private bool CanDown(int? id)
        {
            var (up, @this, down) = SelectShifters(id);
            return down != null && @this != null;
        }


        private string GetDir()
        {
            var res = _openDialog.SelectFolder();
            if (res.IsSucces) return res.Value;
            return string.Empty;
        }

        private string ExploreOpen(string path)
        {
            //TODOTK new dialogs
            //if (!Directory.Exists(path))
            //{
            //    path = _tempPath;
            //}
            //  return _tempPath;
            return GetDir();
        }


        private void ChangeCancel()
        {
            _canCancel = !_canCancel;
            CancelCommand.RaiseCanExecuteChanged();
        }

        private bool CanCancel()
        {
            return _canCancel;
        }

        private void CancelCopy()
        {
            _shifterService.Cancel();
        }

        private void ElementDown(int? id)
        {
            var (up, @this, down) = SelectShifters(id);
            if (@this != null && down != null)
            {
                @this.Order = @this.Order + 1;
                down.Order = down.Order - 1;
                SaveAndRefreshList();
            }
        }

        private void Archive(int? id)
        {
            var config = SelectShifter(id);
            if (config != null)
            {
                config.Archive = !config.Archive;
                SaveAndRefreshList();
            }
        }

        private void ElementUp(int? id)
        {
            var (up, @this, down) = SelectShifters(id);
            if (@this != null && up != null)
            {
                up.Order = up.Order + 1;
                @this.Order = @this.Order - 1;
                SaveAndRefreshList();
            }
        }

        private void Clone(int? id)
        {
            var newConfig = SelectShifter(id);
            if (newConfig != null)
            {
                newConfig.Id = 0;
                newConfig.Title = $"{newConfig.Title}_kopia";
                ShifterConfig = newConfig;
            }
        }


        private bool Valid()
        {
            var g = ShifterConfig;
            //TODOTK
            return true;
        }


        private void ClearProgress()
        {
            IsProgressBarVisible = false;
            ProgressValue = 0;
        }

        private void Progress(double obj)
        {
            ProgressValue = obj;
        }

        private void Run()
        {
            Run(ShifterConfig);
        }

        private async Task Run(ShifterConfig config)
        {
            ChangeCancel();
            if (config != null)
            {
                IsProgressBarVisible = true;
                var res = await _shifterService.Move(config);
                if (res.IsError)
                    MessageError(res.Message, "ShifterCopy");
                else
                    ShowDialog(config);
                ClearProgress();
            }

            ChangeCancel();
        }

        private void ShowDialog(ShifterConfig config)
        {
            var dialog = _unityContainer.Resolve<IDialogView>(ViewReservoir.ShifterModule.MessageWindow);
            dialog.ShowDialog(new MessageContext
            {
                Text = "Proces zakończony pomyślnie.",
                NewPath = config.DestinationPath
            });
        }

        private void RunThis(int? id)
        {
            Run(SelectShifter(id));
        }

        private void ShifterConfigClear()
        {
            ShifterConfig = null;
            ShifterConfig = new ShifterConfig();
        }

        private void SaveAndRefreshList()
        {
            SaveToFile();
            RefreshList();
        }

        private void ReloadData()
        {
            _shifterConfigSettings = _serializationService.LoadShifterConfigSettings();
            RefreshList();
            RaisePropertyChanged(nameof(Types));
            RaisePropertyChanged(nameof(Versions));
        }

        private void RefreshList()
        {
            _shifterConfigsList = _serializationService.LoadConfig();
            SaveList = new ObservableCollection<BaseShifterConfig>(_shifterConfigsList);
        }

        private void Save()
        {
            if (!Valid())
            {
                MessageWarning("Prosze uzupełnić wszystkie pola", "Uwaga!");
                return;
            }

            if (ShifterConfig != null)
            {
                if (ShifterConfig.Order == 0)
                    ShifterConfig.Order =
                        (_shifterConfigsList.Any() ? _shifterConfigsList.OrderBy(w => w.Id).Last().Order : 0) + 1;

                if (ShifterConfig.Id.HasValue)
                {
                    var el = SelectShifter(ShifterConfig.Id);
                    el.Title = ShifterConfig.Title;
                }
                else
                {
                    ShifterConfig.Id =
                        (_shifterConfigsList.Any() ? _shifterConfigsList.OrderBy(w => w.Id).Last().Id : 0) +
                        1;
                    _shifterConfigsList.Add(ShifterConfig);
                }
            }

            SaveAndRefreshList();
            ShifterConfigClear();
        }

        private void Select(int? id)
        {
            ShifterConfig = SelectShifter(id);
        }

        private ShifterConfig SelectShifter(int? id)
        {
            return _shifterConfigsList.FirstOrDefault(w => w.Id == id);
        }

        private (ShifterConfig, ShifterConfig, ShifterConfig) SelectShifters(int? id)
        {
            ShifterConfig up = null, @this = null, down = null;

            var thisIndes = _shifterConfigsList.FindIndex(w => w.Id == id);
            if (thisIndes >= 0)
            {
                @this = _shifterConfigsList[thisIndes];

                if (thisIndes > 0) up = _shifterConfigsList[thisIndes - 1];

                if (thisIndes < _shifterConfigsList.Count - 1) down = _shifterConfigsList[thisIndes + 1];
            }

            return (up, @this, down);
        }

        private void Delete(int? id)
        {
            _shifterConfigsList.Remove(SelectShifter(id));
            SaveAndRefreshList();
        }

        private void SaveToFile()
        {
            _serializationService.SaveShifterConfig(_shifterConfigsList);
        }
    }
}