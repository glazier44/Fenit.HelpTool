using System.Collections.Generic;
using Fenit.HelpTool.Core.Service.Model.Shifter;

namespace Fenit.HelpTool.Core.Service.Model.Extension
{
    public static class ShifterConfigExt
    {
        public static string[] AllExtension(this ShifterConfig shifterConfig)
        {
            var result = new List<string>();

            if (shifterConfig != null && !string.IsNullOrEmpty(shifterConfig.Pattern))
                result.AddRange(shifterConfig.Pattern.Split(','));

            return result.ToArray();
        }

        public static string[] AllFiles(this ShifterConfig shifterConfig)
        {
            var result = new List<string>();

            if (shifterConfig != null && !string.IsNullOrEmpty(shifterConfig.ExcludeFile))
                result.AddRange(shifterConfig.ExcludeFile.Split(','));
            return result.ToArray();
        }
    }
}