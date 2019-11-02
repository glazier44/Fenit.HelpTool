using System.IO;
using Fenit.Toolbox.Core.Answers;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Fenit.HelpTool.UI.Core.Dialog
{
    public class OpenDialog
    {
        public Response<string> SelectFolder()
        {
            return SelectFolderFromPath(string.Empty);
        }

        public Response<string> SelectFolder(string path)
        {
            return SelectFolderFromPath(path);
        }

        private static Response<string> SelectFolderFromPath(string path)
        {
            var res = new Response<string>();
            var dialog = new CommonOpenFileDialog {IsFolderPicker = true};
            if (!string.IsNullOrEmpty(path)) dialog.InitialDirectory = path;

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