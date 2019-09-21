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
using InstallPackageLib.Android;
using Prism.Commands;
using Prism.Events;
using Unity;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        private readonly IEventAggregator _eventAggregator;
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
        private List<string> _types;

        public MainWindowViewModel(ILoggerService log, ISerializationService serializationService,
            IShifterService shifterService, IEventAggregator eventAggregator, IUnityContainer unityContainer, IFileService fileService) :
            base(log)
        {
            _serializationService = serializationService;
            _shifterService = shifterService;
            _unityContainer = unityContainer;
            _eventAggregator = eventAggregator;
            _fileService = fileService;
            _types = new List<string>();
            SetShifterConfig(new ShifterConfig());
            ReloadData();
            CreateCommand();
            _openDialog = new OpenDialog();
            EventAggregatorSubscribe();
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

        public DelegateCommand OpenDestinationZipPathCommand { get; set; }

        public DelegateCommand OpenSourcePathCommand { get; set; }
        public DelegateCommand OpenDestinationPathCommand { get; set; }
        public DelegateCommand<int?> UpCommand { get; set; }
        public DelegateCommand<int?> DownCommand { get; set; }
        public DelegateCommand<int?> ArchiveCommand { get; set; }
        public DelegateCommand<int?> RunThisCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand<int?> SelectCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand { get; set; }
        public DelegateCommand<int?> CloneCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand ReloadCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }

        public List<string> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        public List<string> Versions => _shifterConfigSettings.AppVersion;

        private void EventAggregatorSubscribe()
        {
            _eventAggregator.GetEvent<ProgressEvent>().Subscribe(Progress);
            _eventAggregator.GetEvent<SaveKeyBindingEvent>().Subscribe(Save, ThreadOption.UIThread);
            _eventAggregator.GetEvent<SaveNewKeyBindingEvent>().Subscribe(Add, ThreadOption.UIThread);
            _eventAggregator.GetEvent<ReloadKeyBindingEvent>().Subscribe(ReloadData, ThreadOption.UIThread);
            _eventAggregator.GetEvent<ReloadShiferListEvent>().Subscribe(ReloadData, ThreadOption.UIThread);
            _eventAggregator.GetEvent<RunKeyBindingEvent>().Subscribe(Run, ThreadOption.UIThread);
        }

        private void CreateCommand()
        {
            DeleteCommand = new DelegateCommand<int?>(Delete, CanDo);
            SelectCommand = new DelegateCommand<int?>(Select);
            SaveCommand = new DelegateCommand(Save);
            ClearCommand = new DelegateCommand(() =>
                SetShifterConfig(new ShifterConfig()));
            RunThisCommand = new DelegateCommand<int?>(RunThis, CanDo);
            RunCommand = new DelegateCommand(Run, CanDo);
            CloneCommand = new DelegateCommand<int?>(Clone, CanDo);
            CancelCommand = new DelegateCommand(CancelCopy, CanCancel);
            OpenSourcePathCommand = new DelegateCommand(() =>
            {
                ShifterConfig.SourcePath = ExploreOpen(_shifterConfig.SourcePath);
            });
            OpenDestinationPathCommand = new DelegateCommand(() =>
            {
                ShifterConfig.DestinationPath = ExploreOpen(_shifterConfig.DestinationPath);
            });
            OpenDestinationZipPathCommand = new DelegateCommand(() =>
            {
                ShifterConfig.DestinationZipPath = ExploreOpen(_shifterConfig.DestinationZipPath);
            });
            DownCommand = new DelegateCommand<int?>(ElementDown, CanDown);
            UpCommand = new DelegateCommand<int?>(ElementUp, CanUp);
            ArchiveCommand = new DelegateCommand<int?>(Archive);
            ReloadCommand = new DelegateCommand(ReloadData);
            AddCommand = new DelegateCommand(Add, CanDo);
            LoadCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            // if (getExtension == ".apk")
            {
                var apkReader = new ApkReader();
                //apkReader.Read(filename);
                //cbPrgType.SelectedIndex = _programTypes.GetIndex("Android");
                //return apkReader.ApkModel.VersionName;
            }
        }

        private bool CanUp(int? id)
        {
            var (up, @this, down) = SelectShifters(id);
            return up != null && @this != null;
        }

        private bool CanDo()
        {
            if (ShifterConfig != null) return !ShifterConfig.Archive;
            return false;
        }

        private bool CanDo(int? id)
        {
            var config = SelectShifter(id);
            if (config != null) return !config.Archive;
            return false;
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
            SetShifterConfig(new ShifterConfig());

            var newConfig = (ShifterConfig) SelectShifter(id).Clone();

            newConfig.Id = 0;
            newConfig.Title = $"{newConfig.Title}_copy";
            _shifterConfigsList.Add(newConfig);
            Save();
        }


        private bool Valid()
        {
            return !(string.IsNullOrEmpty(ShifterConfig.SourcePath)
                     || string.IsNullOrEmpty(ShifterConfig.DestinationPath) ||
                     string.IsNullOrEmpty(ShifterConfig.Title));
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
                Text = "The process was completed successfully.",
                NewPath = config.DestinationPath
            });
        }

        private void RunThis(int? id)
        {
            Run(SelectShifter(id));
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
            SaveList = new ObservableCollection<BaseShifterConfig>(
                _shifterConfigsList.Where(w => _shifterConfigSettings.ShowArchive || !w.Archive));
        }

        private void Add()
        {
            if (!Valid())
            {
                MessageWarning("Please complete all fields with an asterisk!", "Caution!");
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

            Save();
        }

        private void Save()
        {
            SaveAndRefreshList();
            SetShifterConfig(new ShifterConfig());
            _eventAggregator.GetEvent<LogEvent>().Publish("Save ShifterConfig");
        }

        private void SetShifterConfig(ShifterConfig shifterConfig)
        {
            if (ShifterConfig != null)
            {
                ShifterConfig.SourcePathEdit -= ShifterConfig_SourcePathEdit;
                ShifterConfig = null;
            }

            ShifterConfig = shifterConfig;
            ShifterConfig.SourcePathEdit += ShifterConfig_SourcePathEdit;
        }

        private void ShifterConfig_SourcePathEdit(string path)
        {
            var info = _fileService.GetFileInfo(path);
            if (info.IsSucces)
            {
                var fileName = info.Value;
                var p = _shifterConfigSettings.ProgramType.FindProgram(fileName.FileNameWithoutExtension);
            }
        }

        private void Select(int? id)
        {
            SetShifterConfig(SelectShifter(id));
            RunCommand.RaiseCanExecuteChanged();
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