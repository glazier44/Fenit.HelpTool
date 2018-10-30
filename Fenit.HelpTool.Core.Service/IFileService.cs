using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.Service
{
    public interface IFileService
    {
        Response<T> LoadXml<T>() where T : class, new();
        Response<T> Load<T>() where T : class, new();
        Response SaveXml<T>(T obj) where T : class, new();
        Response Save<T>(string text) where T : class, new();
    }
}