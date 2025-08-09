using System;

namespace Fenit.Toolbox.WPF.UI.Base
{
    public interface IDialogContext
    {
        Action CloseAction { get; set; }
    }
}
