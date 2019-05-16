using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Event;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.HelpTool.Module.Shifter.Model;
using Fenit.HelpTool.UI.Core;
using Fenit.HelpTool.UI.Core.Base;
using Fenit.HelpTool.UI.Core.Dialog;
using Prism.Commands;
using Prism.Events;
using Unity;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly OpenDialog _openDialog;
        private readonly ISerializationService _serializationService;
        private readonly List<ShifterConfig> _shifterConfigsList;
        private readonly IShifterService _shifterService;
        private readonly IUnityContainer _unityContainer;
        private bool _canCancel;
        private bool _isProgressBarVisible;
        private double _progressValue;
        private ObservableCollection<BaseShifterConfig> _saveList;
        private ShifterConfig _shifterConfig;

        public MainWindowViewModel(ILoggerService log, ISerializationService serializationService,
            IShifterService shifterService, IEventAggregator eventAggregator, IUnityContainer unityContainer) :
            base(log)
        {
            _serializationService = serializationService;
            _shifterService = shifterService;
            _unityContainer = unityContainer;
            _shifterConfigsList = _serializationService.LoadConfig();
            ShifterConfigClear();
            RefreshList();
            CreateCommand();
            eventAggregator.GetEvent<ProgressEvent>().Subscribe(Progress);
            _openDialog = new OpenDialog();
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

        public DelegateCommand<int?> RunThisCommand { get; set; }
        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand<int?> SelectCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand { get; set; }
        public DelegateCommand<int?> CloneCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

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

        private void RefreshList()
        {
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

            SaveToFile();
            RefreshList();
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

        private void Delete(int? id)
        {
            _shifterConfigsList.Remove(SelectShifter(id));
            SaveToFile();
            RefreshList();
        }

        private void SaveToFile()
        {
            _serializationService.SaveShifterConfig(_shifterConfigsList);
        }
    }
}