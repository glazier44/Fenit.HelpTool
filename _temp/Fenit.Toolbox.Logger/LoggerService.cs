using System;
using NLog;

namespace Fenit.Toolbox.Logger
{
    public class LoggerService : ILoggerService
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

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