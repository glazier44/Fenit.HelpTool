using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public class SelectConverter : Converter
    {
        private Sql _sql;
        public SelectConverter(Sql sql)
        {
            _sql = sql;
        }

        public override string GetSql()
        {
            var script = _sql.SqlCommand;
            foreach (var param in _sql.Param)
            {
                script = Replace(param, script);
            }
            return script;
        }

        private string Replace(Param param, string script)
        {
            if (param.Direction == "Input")
            {
                script = script.Replace($":{param.Name}", GetValue(param));
            }
            return script;
        }

        private string GetValue(Param param)
        {
            switch (param.Type)
            {
                case "Int32":
                {
                    return param.Text;
                }
                case "String":
                {
                    return $"'{param.Text}'";
                }
                default:
                    return param.Text;
            }
        }
    }
}
