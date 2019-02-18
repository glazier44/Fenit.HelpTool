using System.Windows;
using Fenit.HelpTool.Module.Shifter.Model;
using Fenit.HelpTool.UI.Core.Base;
using Prism.Commands;
using Prism.Mvvm;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogViewModel
    {
        private MessageContext _messageContext;

        public MessageViewModel()
        {
            CancelCommand = new DelegateCommand(() => DialogContext?.CloseAction?.Invoke());
            CopyCommand = new DelegateCommand(Copy);
        }


        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand CopyCommand { get; set; }

        public string Message => _messageContext?.Text;
        public string NewPath => _messageContext?.NewPath;

        public IDialogContext DialogContext
        {
            get => _messageContext;
            set => _messageContext = (MessageContext) value;
        }

        private void Copy()
        {
            Clipboard.SetText(NewPath);
        }
    }
}