using System.Collections.Generic;

namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class ShifterConfig : BaseShifterConfig

    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string Pattern { get; set; }
        public string ExcludeFile { get; set; }
        public bool RemoveAll { get; set; }
        public bool CreateCopy { get; set; }
        public bool Override { get; set; }

    }

    public static class ShifterConfigExt{

        public static string[] AllExtension(this ShifterConfig shifterConfig)
        {
            var result = new List<string>();

            if (shifterConfig != null && !string.IsNullOrEmpty(shifterConfig.Pattern))
            {
                result.AddRange(shifterConfig.Pattern.Split(','));
            }

            return result.ToArray();
        }

        public static string[] AllFiles(this ShifterConfig shifterConfig)
        {
            var result = new List<string>();

            if (shifterConfig != null && !string.IsNullOrEmpty(shifterConfig.ExcludeFile))
            {
                result.AddRange(shifterConfig.ExcludeFile.Split(','));
            }
            return result.ToArray();
        }
     
    }
}