using Prism.Mvvm;

namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class BaseShifterConfig: BindableBase
    {
        public int? Id { get; set; }
        public string Title { get; set; }
    }
}