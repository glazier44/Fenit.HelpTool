using Fenit.Toolbox.Core.Response;

namespace Fenit.HelpTool.Core.Service
{
    public interface ISqlFileService
    {
        Response<string> Read();

        Response<string> ReadSelect(string sqlString);

        Response<string> ReadProcedure(string sqlString);

    }
}