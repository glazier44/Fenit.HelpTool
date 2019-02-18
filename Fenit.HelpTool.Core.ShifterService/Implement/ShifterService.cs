using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Event;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.Toolbox.Core.Answers;
using Prism.Events;

namespace Fenit.HelpTool.Core.ShifterService.Implement
{
    public class ShifterService : IShifterService
    {
        private readonly IEventAggregator _eventAggregator;

        public ShifterService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public async Task<Response> Move(ShifterConfig shifterConfig)
        {
            var res = new Response();

            if (shifterConfig != null)
                await Task.Run(() =>
                {
                    try
                    {
                        double l = 0;
                        double p = 2;
                        var dir = Directory.GetDirectories(shifterConfig.SourcePath, "*",
                            SearchOption.AllDirectories);
                        l = +dir.Length;
                        _eventAggregator.GetEvent<ProgressEvent>().Publish(p);
                        var files = Directory.GetFiles(shifterConfig.SourcePath, "*",
                            SearchOption.AllDirectories);
                        l += files.Length * 2;
                        p += 3;
                        _eventAggregator.GetEvent<ProgressEvent>().Publish(p);

                        var tick = 95 / l;

                        foreach (var dirPath in dir)
                        {
                            Directory.CreateDirectory(dirPath.Replace(shifterConfig.SourcePath,
                                shifterConfig.DestinationPath));
                            p += tick;
                            _eventAggregator.GetEvent<ProgressEvent>().Publish(p);
                        }

                        var allExtension = shifterConfig.AllExtension();
                        var allFiles = shifterConfig.AllFiles();

                        foreach (var newPath in files)
                        {
                            var info = new FileInfo(newPath);
                            if (!allExtension.Contains(info.Extension) && !allFiles.Contains(info.Name))
                                File.Copy(newPath,
                                    newPath.Replace(shifterConfig.SourcePath, shifterConfig.DestinationPath),
                                    true);

                            p += tick * 2;
                            _eventAggregator.GetEvent<ProgressEvent>().Publish(p);
                        }
                    }
                    catch (Exception e)
                    {
                        Cler(shifterConfig.DestinationPath);
                        res.AddError("Błąd zapisu plików.");
                        //TODOTK log
                    }
                });
            return res;
        }


        private void Cler(string path)
        {
            var directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}