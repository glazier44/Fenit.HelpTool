using System.IO;
using Fenit.Toolbox.Core.Answers;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Fenit.HelpTool.UI.Core.Dialog
{
    public class OpenDialog
    {
        public Response<string> SelectFolder()
        {
            var res = new Response<string>();


            var dialog = new CommonOpenFileDialog {IsFolderPicker = true};
            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                var dirToProcess = Directory.Exists(dialog.FileName)
                    ? dialog.FileName
                    : Path.GetDirectoryName(dialog.FileName);
                res.AddValue(dirToProcess);
            }
            else
            {
                res.AddError("No folder selected");
            }

            return res;
        }
    }
}