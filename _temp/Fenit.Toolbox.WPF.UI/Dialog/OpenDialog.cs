using System;
using System.Collections.Generic;
using System.IO;
using Fenit.Toolbox.Core.Answers;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Fenit.Toolbox.WPF.UI.Dialog
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
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
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
        private static Response<string> SelectFileFromPath(string path,Dictionary<string,string> extension )
        {
            var res = new Response<string>();
            var dialog = new CommonOpenFileDialog { IsFolderPicker = false };
            foreach (var row in extension)
            {
                dialog.Filters.Add(new CommonFileDialogFilter(row.Value, row.Key));
            }
            
            //dialog.Filters.Add(new CommonFileDialogFilter("FT Files", "*.ft"));



            if (!string.IsNullOrEmpty(path))
            {
                dialog.InitialDirectory = path;

            }
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                if (File.Exists(dialog.FileName))
                {
                    res.AddValue(dialog.FileName);
                }
                else
                {
                    res.AddError("Selected file does not exist");
                }
                
                
            }
            else
            {
                res.AddError("No folder selected");
            }

            return res;
        }
        public Response<string> SelectFile(string path, Dictionary<string, string> extension)
        {
            return SelectFileFromPath(path,extension);
        }
    }
}