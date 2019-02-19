using System;

namespace Fenit.HelpTool.UI.Core.Base
{
    public interface IDialogView
    {
        void Show();

        void ShowDialog<T>(T context) where T : IDialogContext;
        //void ShowDialog<T>(T context) where T : IDialogViewModel;
        //void ShowDialog<T>(T context, Action<T> callback) where T : IDialogViewModel;
    }


}
