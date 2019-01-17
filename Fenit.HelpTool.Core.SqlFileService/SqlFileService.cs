using System;
using Fenit.HelpTool.Core.SqlFileService.Converter;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.Core.SqlFileService.Model;
using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.SqlFileService
{
    public class SqlFileService : ISqlFileService
    {
        public Response<string> Read()
        {
            throw new NotImplementedException();
        }

        public Response<SqlModel> ReadSelect(string sqlString)
        {
            var result = new Response<SqlModel>();
            try
            {
                var readSqlFile = new ReadSqlFile(SqlType.Select);
                var sql = readSqlFile.Read(sqlString);
                var selectConverter = new SelectConverter(sql);
                var model = new SqlModel {Text = selectConverter.GetSql()};
                result.AddValue(model);
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public Response<SqlModel> ReadProcedure(string sqlString)
        {
            var result = new Response<SqlModel>();
            try
            {
                var readSqlFile = new ReadSqlFile(SqlType.Procedure);
                var sql = readSqlFile.Read(sqlString);
                var selectConverter = new ProcedureConverter(sql);
                var model = new SqlModel {Text = selectConverter.GetSql()};
                result.AddValue(model);
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }

            return result;
        }
    }
}