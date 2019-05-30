using System.Collections.Generic;
using Fenit.Toolbox.Core.Extension;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ShifterConfigSettings: BindableBase
    {
        private string _configPath;//, _sourcePath, _type, _version;

        public List<string> AppType { get; set; } = new List<string>();
        public List<string> AppVersion { get; set; } = new List<string>();
        public string ConfigPath
        {
            get => _configPath;
            set => SetProperty(ref _configPath, value);
        }
        public string FileName { get; set; }

        public bool ShowArchive { get; set; }

        [JsonIgnore]
        public string AppTypeLabel
        {
            get => string.Join(",", AppType);
            set => AppType = value.SplitText();
        }

        [JsonIgnore] public string AppVersionLabel
        {
            get => string.Join(",", AppVersion);
            set => AppVersion = value.SplitText();
        }

    }
}