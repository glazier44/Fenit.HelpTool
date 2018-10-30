using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.Service
{
    public interface IFileService
    {
        Response<T> LoadXml<T>() where T : class, new();
        string Load();
        Response SaveXml<T>(T obj) where T : class, new();
        Response Save(string text);
    }
}