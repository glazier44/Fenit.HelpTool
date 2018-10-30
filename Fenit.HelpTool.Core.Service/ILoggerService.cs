using System;

namespace Fenit.HelpTool.Core.Service
{
    public interface ILoggerService
    {
        void Error(string message);
        void Error(string name, Exception e);
        void Warn(string message);
        void Info(string message);
        void Trace(string message);
    }
}