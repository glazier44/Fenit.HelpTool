using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var parameters = new StringBuilder();
            var outPut = new StringBuilder();
            var ret = new StringBuilder();
            var dmbs = new StringBuilder();
            var declare = new StringBuilder();

            foreach (var param in _sql.Param.GroupBy(w => w.ParamType))
            {
                if (param.Key == ParamType.Input)
                    parameters.Append(string.Join($",{NewLine}{Tab}",
                        GetInputParameters(param.Select(w => w).ToList())));

                if (param.Key == ParamType.Output)
                {
                    outPut.Append(string.Join($",{NewLine}{Tab}",
                        GetOutParameters(param.Select(w => w).ToList())));

                    foreach (var el in param)
                    {
                        dmbs.Append(GetdbmsOutput(el));
                        declare.Append(GetDeclare(el));
                    }

                }

                if (param.Key == ParamType.Result)
                {
                    var oneParam = param.First();
                    ret.Append(GetResParameters(oneParam));
                    dmbs.Append(GetdbmsOutput(oneParam));
                    declare.Append(GetDeclare(oneParam));


                }
            }


            var script = $"DECLARE{NewLine}" +
                         $"{declare}" +
                         $"BEGIN{NewLine}" +
                         $"{ret}{_sql.SqlCommand}({NewLine}" +
                         $"{Tab}{parameters}{NewLine}" +
                         $"{Tab}{outPut}" +
                         $");{NewLine}" +
                         $"{dmbs}{NewLine}" +
                         $"END;";

            return script;
        }

        private IEnumerable<string> GetInputParameters(IEnumerable<Param> parameters)
        {
            return parameters.Select(el => $"{el.Name}=>{GetValue(el)}").ToList();
        }

        private IEnumerable<string> GetOutParameters(IEnumerable<Param> parameters)
        {
            return parameters.Select(el => $"{el.Name}=>r_{el.Name}").ToList();
        }
        private string GetResParameters(Param param)
        {
            return $"r_{param.Name}:=";
        }
        private string GetdbmsOutput(Param param)
        {
            return $" dbms_output.put_line('r_{param.Name}: ' ||r_{param.Name} );{NewLine}";
        }
    }
}