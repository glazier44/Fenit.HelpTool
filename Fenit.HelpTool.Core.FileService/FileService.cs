using System.IO;
using System.Windows.Forms;
using Fenit.HelpTool.Core.Service;
using Fenit.Toolbox.Core.Answers;
using Fenit.Toolbox.Core.Extension;

namespace Fenit.HelpTool.Core.FileService
{
    public class FileService : IFileService
    {
        public Response<T> LoadXml<T>() where T : class, new()
        {
            return OpenDialog().DeserializeFromFile<T>();
        }

        public Response<T> Load<T>() where T : class, new()
        {
            return OpenDialog().DeserializeFromString<T>();
        }

        public Response SaveXml<T>(T obj) where T : class, new()
        {
            return obj.XmlSerialize(OpenSaveDialog());
        }

        public Response Save<T>(string text) where T : class, new()
        {
            return text.StringSerialize(OpenSaveDialog());
        }


        private static string OpenDialog()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "xml",
                Filter = "XML-File | *.xml",
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