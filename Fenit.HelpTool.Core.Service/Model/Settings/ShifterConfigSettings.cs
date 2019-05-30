using System.Collections.Generic;
using Fenit.Toolbox.Core.Extension;
using Newtonsoft.Json;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ShifterConfigSettings
    {
        public List<string> AppType { get; set; } = new List<string>();
        public List<string> AppVersion { get; set; } = new List<string>();
        public string ConfigPath { get; set; }
        public string FileName { get; set; }

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