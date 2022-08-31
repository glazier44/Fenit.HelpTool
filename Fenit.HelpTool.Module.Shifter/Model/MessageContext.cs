using System;
using Fenit.Toolbox.WPF.UI.Base;

namespace Fenit.HelpTool.Module.Shifter.Model
{
    public class MessageContext : IDialogContext
    {
        public string Text { get; set; }
        public string NewPath { get; set; }
        public Action CloseAction { get; set; }
    }
}