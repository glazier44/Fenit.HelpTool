using System.Collections.Generic;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ShifterConfigSettings
    {
        public List<string> AppType { get; set; } = new List<string>();
        public List<string> AppVersion { get; set; } = new List<string>();
        public string ConfigPath { get; set; }
        public string FileName { get; set; }

    }
}