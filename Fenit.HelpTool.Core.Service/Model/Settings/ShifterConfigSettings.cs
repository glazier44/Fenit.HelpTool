using System.Collections.Generic;
using System.Linq;
using Fenit.Toolbox.Core.Extension;
using Fenit.Toolbox.Yaml.Extension;
using InstallPackageLib.ProgramsType;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ShifterConfigSettings : BindableBase
    {
        private string _configPath = string.Empty; //, _sourcePath, _type, _version;

        public ProgramTypeList ProgramType { get; set; } = new ProgramTypeList();

        public List<string> AppVersion { get; set; } = new List<string>();

        public string ConfigPath
        {
            get => _configPath;
            set => SetProperty(ref _configPath, value);
        }

        public string FileName { get; set; } = string.Empty;

        public bool ShowArchive { get; set; }

        [JsonIgnore]
        public string ProgramTypeLabel
        {
            get => ProgramType?.ProgramType.Cast<object>().ToList().SerializationYaml();
            set
            {
                var programType = value.DeserializationYaml<List<ProgramType>>();
                if (programType != null)
                {
                    ProgramType.ProgramType = programType;
                }
            }
        }

        [JsonIgnore]
        public string AppVersionLabel
        {
            get => AppVersion.JoinText();
            set => AppVersion = value.SplitText();
        }
    }
}