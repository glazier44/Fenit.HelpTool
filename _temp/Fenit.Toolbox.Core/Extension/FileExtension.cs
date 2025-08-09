using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Fenit.Toolbox.Core.Extension
{
    public static class FileExtension
    {
        public static string GetFileBasedExtension(this string path, string ext)
        {
            return GetExeFileName(Directory.GetFiles(path, $"*.{ext}"));
        }
        public static string GetFileBasedExtensionsFile(this string path, params string[] ext)
        {
            foreach (var el in ext)
            {
                var res= path.GetFileBasedExtension(el);
                if (!string.IsNullOrEmpty(res))
                {
                    return res;
                }
            }
            return string.Empty;
        }
        public static string GetVersion(this string path, bool removeFeature)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            if (removeFeature)
            {
                var sp = versionInfo.ProductVersion.Split('-');
                return sp[0];
            }
            return versionInfo.ProductVersion;
        }


        public static bool HasFileExtension(this string path, params string[] ext)
        {
            foreach (var el in ext)
            {
                var res = path.GetFileBasedExtension(el);
                if (!string.IsNullOrEmpty(res))
                {
                    return true;
                }
            }
            return false;
        }
        public static string GetFileWithoutExtension(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return Path.GetFileNameWithoutExtension(fileName);
        }

        private static string GetExeFileName(string[] exeFiles)
        {
            foreach (var exeFile in exeFiles.OrderBy(w => w))
            {
                if (exeFile.EndsWith(".vshost.exe", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                return Path.GetFileName(exeFile);
            }

            return string.Empty;
        }
    }
}