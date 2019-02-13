using Fenit.HelpTool.Core.SqlFileService.Model;
using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.SqlFileService
{
    public interface ISqlFileService
    {
        Response<string> Read();

        Response<SqlModel> ReadSelect(string sqlString);

        Response<SqlModel> ReadProcedure(string sqlString);
    }
}