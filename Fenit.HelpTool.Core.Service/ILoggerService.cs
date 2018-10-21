using System;
using Fenit.Toolbox.Core.Response;
using Prism.Logging;

namespace Fenit.HelpTool.Core.Service
{
    public interface ILoggerService : ILoggerFacade
    {
        void Error(string message);
        void Error(string name, Exception e);
        void Warn(string message);
        void Info(string message);
        void Trace(string message);

    }
}