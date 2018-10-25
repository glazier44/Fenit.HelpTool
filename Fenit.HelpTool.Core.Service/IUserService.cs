using Fenit.Toolbox.Core.Response;

namespace Fenit.HelpTool.Core.Service
{
    public interface IUserService
    {
        Response<bool> Login(string username, string password);
        bool IsRootMode { get; set; }
        bool IsLogged { get; }
    }
}