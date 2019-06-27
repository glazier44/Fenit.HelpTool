using Prism.Mvvm;

namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class BaseShifterConfig : BindableBase
    {
        private string _title;
        public int? Id { get; set; }
        public int Order { get; set; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}