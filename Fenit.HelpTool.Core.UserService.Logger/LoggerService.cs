using System;
using Fenit.HelpTool.Core.Service;
using NLog;
using Prism.Logging;

namespace Fenit.HelpTool.Core.Logger
{
    public class LoggerService : ILoggerService
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void Log(string message, Category category, Priority priority)
        {
            //throw new NotImplementedException();
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error(string name, Exception e)
        {
            Logger.Error(e);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Trace(string message)
        {
            Logger.Trace(message);
        }
    }
}