using Fenit.Toolbox.Core.Extension;

namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class FileInfo
    {
        public string FileName { get; set; } = string.Empty;
        public string FileNameWithoutExtension => FileName.GetFileWithoutExtension();

        public string Version { get; set; } = string.Empty;
    }
}