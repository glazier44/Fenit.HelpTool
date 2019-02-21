using System;
using System.IO;
using System.Threading.Tasks;
using Fenit.HelpTool.Core.Service.Abstract;
using Fenit.HelpTool.Core.Service.Model.Event;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.HelpTool.Core.ShifterService.Helpers;
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
                        var mover = new Mover(shifterConfig, Send, Clear);
                        mover.Work();
                    }
                    catch (Exception e)
                    {
                       // Clear(shifterConfig.DestinationPath);
                        res.AddError("Błąd zapisu plików.");
                        //TODOTK log
                    }
                });
            return res;
        }

        private void Send(double percentage)
        {
            _eventAggregator.GetEvent<ProgressEvent>().Publish(percentage);
        }

        private void Clear(string path)
        {
            var directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}