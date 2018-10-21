﻿using System;
using Fenit.HelpTool.Core.Service;
using Fenit.HelpTool.Core.SqlFileService.Converter;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.Toolbox.Core.Response;

namespace Fenit.HelpTool.Core.SqlFileService
{
    public class SqlFileService : ISqlFileService
    {
        public Response<string> Read()
        {
            throw new NotImplementedException();
        }

        public Response<string> Read(string sqlString)
        {
            var result = new Response<string>();
            try
            {
                var readSqlFile = new ReadSqlFile(SqlType.Select);
                var sql = readSqlFile.Read(sqlString);
                var selectConverter = new SelectConverter(sql);
                result.AddValue(selectConverter.GetSql());
            }
            catch (Exception e)
            {
                //log
                result.AddError(e.Message);
            }

            return result;
        }
    }
}