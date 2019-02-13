using System;
using System.IO;
using System.Linq;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Shifter;

namespace Fenit.HelpTool.Core.ShifterService.Implement
{
    public class ShifterService : IShifterService
    {
        public void Move(ShifterConfig shifterConfig)
        {
            if (shifterConfig != null)
                try
                {
                    //Now Create all of the directories
                    foreach (var dirPath in Directory.GetDirectories(shifterConfig.SourcePath, "*",
                        SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(shifterConfig.SourcePath,
                            shifterConfig.DestinationPath));

                    var allExtension = shifterConfig.AllExtension();
                    var allFiles = shifterConfig.AllFiles();

                    //Copy all the files & Replaces any files with the same name
                    foreach (var newPath in Directory.GetFiles(shifterConfig.SourcePath, "*",
                        SearchOption.AllDirectories))
                    {
                        var info = new FileInfo(newPath);
                        if (!allExtension.Contains(info.Extension) && !allFiles.Contains(info.Name))
                            File.Copy(newPath, newPath.Replace(shifterConfig.SourcePath, shifterConfig.DestinationPath),
                                true);
                    }
                }
                catch (Exception)
                {
                    Cler(shifterConfig.DestinationPath);
                }
        }


        private void Cler(string path)
        {
            var directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}