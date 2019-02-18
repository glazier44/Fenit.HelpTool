using System;
using Fenit.HelpTool.UI.Core.Base;

namespace Fenit.HelpTool.Module.Shifter.ViewModels
{
    public class MessageContext : IDialogContext
    {
        public string Text { get; set; }
        public string NewPath { get; set; }
        public Action CloseAction { get; set; }
    }
}