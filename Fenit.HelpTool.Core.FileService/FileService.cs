using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.Toolbox.Core.Answers;
using Fenit.Toolbox.Core.Extension;
using FileInfo = Fenit.HelpTool.Core.Service.Model.Shifter.FileInfo;

namespace Fenit.HelpTool.Core.FileService
{
    public class FileService : IFileService
    {
        public Response<T> LoadXml<T>() where T : class, new()
        {
            return OpenDialog().DeserializeFromFile<T>();
        }

        public Response<string> Load()
        {
            return OpenDialog().LoadFile();
        }

        public Response SaveXml<T>(T obj) where T : class, new()
        {
            return obj.XmlSerialize(OpenSaveDialog());
        }

        public Response Save(string text)
        {
            return text.SaveFile(OpenSaveDialog());
        }

        public Response<FileInfo> GetFileInfo(string path)
        {
            var res = new FileInfo();
            if (Directory.Exists(path))
            {
                res.FileName = path.GetFileBasedExtensionsFile("exe", "cab", "ex_", "dll", "apk");
            }
            return Response<FileInfo>.Create(res);
        }



        private static string OpenDialog()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "xml",
                Filter = "XML-File | *.xml"
            };

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK) return dialog.FileName;

            return string.Empty;
        }

        private static Stream OpenSaveDialog()
        {
            var dialog =
                new SaveFileDialog
                {
                    Filter = "XML-File | *.xml",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.OpenFile() : null;
        }
    }
}