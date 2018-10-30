using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.Service
{
    public interface ISqlFileService
    {
        Response<string> Read();

        Response<string> ReadSelect(string sqlString);

        Response<string> ReadProcedure(string sqlString);

    }
}