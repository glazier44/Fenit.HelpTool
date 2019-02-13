using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Commands;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly List<ShifterConfig> _shifterConfigsList;
        private ObservableCollection<BaseShifterConfig> _saveList;
        private ShifterConfig _shifterConfig;

        public MainWindowViewModel(ILoggerService log) :
            base(log)
        {
            _shifterConfigsList = new List<ShifterConfig>();
            ShifterConfigClear();
            RefreshList();

            DeleteCommand = new DelegateCommand<int?>(Delete);
            SelectCommand = new DelegateCommand<int?>(Select);
            SaveCommand = new DelegateCommand(Save);
            ClearCommand = new DelegateCommand(ShifterConfigClear);
        }

        public ObservableCollection<BaseShifterConfig> SaveList
        {
            get => _saveList;
            set => SetProperty(ref _saveList, value);
        }

        public ShifterConfig ShifterConfig
        {
            get => _shifterConfig;
            set => SetProperty(ref _shifterConfig, value);
        }

        public DelegateCommand<int?> SelectCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }


        public DelegateCommand<int?> DeleteCommand { get; set; }

        private void ShifterConfigClear()
        {
            ShifterConfig = new ShifterConfig();
        }

        private void RefreshList()
        {
            SaveList = new ObservableCollection<BaseShifterConfig>(_shifterConfigsList);
        }

        private void Save()
        {
            if (ShifterConfig.Id.HasValue)
            {
                var el = SelectShifter(ShifterConfig.Id);
                el.Title = ShifterConfig.Title;
            }
            else
            {
                ShifterConfig.Id = (_shifterConfigsList.Any()? _shifterConfigsList.OrderBy(w => w.Id).Last().Id : 0) + 1;
                _shifterConfigsList.Add(ShifterConfig);
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
        }
    }
}