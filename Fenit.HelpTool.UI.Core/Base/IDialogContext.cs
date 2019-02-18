using System;

namespace Fenit.HelpTool.UI.Core.Base
{
    public interface IDialogContext
    {
        Action CloseAction { get; set; }
    }
}
