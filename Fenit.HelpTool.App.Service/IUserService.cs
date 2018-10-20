using Fenit.Toolbox.Core.Response;

namespace Fenit.HelpTool.App.Service
{
    public interface IUserService
    {
        Response<bool> Login(string username, string password);
    }
}