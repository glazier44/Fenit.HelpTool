using Fenit.HelpTool.Core.Service;
using Fenit.Toolbox.Core.Response;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Fenit.HelpTool.Module.SqlLog.ViewModels
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        //private readonly IEventAggregator _eventAggregator;
        private string _resultText, _sourceText;
        private ISqlFileService _sqlFileService;
        public MainWindowViewModel(ISqlFileService sqlFileService)
        {
            //IEventAggregator eventAggregator _eventAggregator = eventAggregator;
            _sqlFileService = sqlFileService;
            ConvertCommand = new DelegateCommand(Convert, CanConvert);
            LoadFileCommand = new DelegateCommand(LoadFile);
        }

        public string ResultText
        {
            get => _resultText;
            set => SetProperty(ref _resultText, value);
        }

        public string SourceText
        {
            get => _sourceText;
            set
            {
                SetProperty(ref _sourceText, value);
                ConvertCommand.RaiseCanExecuteChanged();
            }
        }



        public DelegateCommand ConvertCommand { get; set; }
        public DelegateCommand LoadFileCommand { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        private void Convert()
        {
            var res = _sqlFileService.Read(SourceText);
            ResultText = res.Value;
        }

        private bool CanConvert()
        {
            return !string.IsNullOrEmpty(_sourceText);
        }

        private void LoadFile()
        {
        }
    }
}