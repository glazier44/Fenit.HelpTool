using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Commands;
using Prism.Regions;

namespace Fenit.HelpTool.Module.SqlLog.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, INavigationAware
    {
        private readonly ISqlFileService _sqlFileService;

        //private readonly IEventAggregator _eventAggregator;
        private string _resultText, _sourceText;
        private SqlType SqlType;

        public MainWindowViewModel(ISqlFileService sqlFileService, ILoggerService log) : base(log)
        {
            //IEventAggregator eventAggregator _eventAggregator = eventAggregator;
            _sqlFileService = sqlFileService;
            ConvertCommand = new DelegateCommand(Convert);
            LoadFileCommand = new DelegateCommand(LoadFile, False);
            RadioButonCommand = new DelegateCommand<object>(RadioButonClick);
        }

        public DelegateCommand<object> RadioButonCommand { get; set; }
        public DelegateCommand ConvertCommand { get; set; }
        public DelegateCommand LoadFileCommand { get; set; }

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


        private void RadioButonClick(object parameter)
        {
            SqlType = SqlType.Non;
        }


        private bool False()
        {
            return false;
        }

        private bool CanConvert()
        {
            return !string.IsNullOrEmpty(_sourceText);
        }

        private void Convert()
        {
            var res = SqlType == SqlType.Select
                ? _sqlFileService.ReadSelect(SourceText)
                : _sqlFileService.ReadProcedure(SourceText);
            if (res.IsError)
                MessageError(res.Message, "[SqlLoad]");
            else
                ResultText = res.Value;
        }

        private void LoadFile()
        {
        }
    }
}