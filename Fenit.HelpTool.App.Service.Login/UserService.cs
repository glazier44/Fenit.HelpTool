using Fenit.HelpTool.Core.Service;
using Fenit.Toolbox.Core.Response;

namespace Fenit.HelpTool.Core.UserService
{
    public class UserService : IUserService
    {
        public Response<bool> Login(string username, string password)
        {
            var res = new Response<bool>();
            if (username == "admin" && password == "admin321")
                res.AddValue(true);

            return res;
        }
    }
}