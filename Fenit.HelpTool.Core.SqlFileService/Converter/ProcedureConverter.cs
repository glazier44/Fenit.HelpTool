using System.Collections.Generic;
using System.Linq;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public class ProcedureConverter : Converter
    {
        private readonly Sql _sql;

        public ProcedureConverter(Sql sql)
        {
            _sql = sql;
        }

        public override string GetSql()
        {
            var parameters = string.Empty;

            foreach (var param in _sql.Param.GroupBy(w => w.ParamType))
            {
                if (param.Key == ParamType.Input)
                    parameters += string.Join($",{NewLine}{Tab}",
                        GetInputParameters(param.Select(w => w).ToList()));

                if (param.Key == ParamType.Output || param.Key == ParamType.Result)
                    parameters += string.Join($",{NewLine}{Tab}",
                        GetOutParameters(param.Select(w => w).ToList()));
            }


            var script = $"DECLARE{NewLine}" +
                         $"" +
                         $"BEGIN{NewLine}" +
                         $"{_sql.SqlCommand}({NewLine}" +
                         $"{Tab}{parameters}" +
                         $"" +
                         $"" +
                         $");{NewLine}" +
                         $"--tu bedom outputy{NewLine}" +
                         $"" +
                         $"END";

            return script;
        }

        public List<string> GetInputParameters(ICollection<Param> parameters)
        {
            return parameters.Select(el => $"{el.Name}=>{GetValue(el)}").ToList();
        }

        public List<string> GetOutParameters(ICollection<Param> parameters)
        {
            return parameters.Select(el => $"{el.Name}=>r_{el.Name}").ToList();
        }
    }
}