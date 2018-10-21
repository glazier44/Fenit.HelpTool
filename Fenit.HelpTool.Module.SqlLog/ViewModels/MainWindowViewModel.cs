﻿using Fenit.HelpTool.Core.Service;
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

        public MainWindowViewModel(ISqlFileService sqlFileService, ILoggerService log) : base(log)
        {
            //IEventAggregator eventAggregator _eventAggregator = eventAggregator;
            _sqlFileService = sqlFileService;
            ConvertCommand = new DelegateCommand(Convert);
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
            if (res.IsError)
                MessageError(res.Message, "[SqlLoad]");
            else
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