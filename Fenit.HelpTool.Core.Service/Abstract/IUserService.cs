using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.Service.Abstract
{
    public interface IUserService
    {
        bool IsRootMode { get; set; }
        bool IsLogged { get; }
        Response<bool> Login(string username, string password);
    }
}