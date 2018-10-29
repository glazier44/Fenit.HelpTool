using System;
using System.Text.RegularExpressions;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public abstract class Converter
    {
        public abstract string GetSql();

        protected string ReplaceInput(Param param, string script)
        {
            if (param.ParamType == ParamType.Input)
                script = Regex.Replace(script, $":{param.Name}", GetValue(param), RegexOptions.IgnoreCase);

            return script;
        }

        protected string NewLine => Environment.NewLine;
        protected string Tab => "\t\t";


        protected string GetValue(Param param)
        {
            switch (param.Type)
            {
                case "Int32":
                    {
                        return param.Text;
                    }
                case "String":
                    {
                        var txt = param.Text;
                        if (string.IsNullOrEmpty(txt))
                        {
                            txt = " ";
                        }

                        return $"'{txt}'";
                    }
                case "DateTime":
                    {
                        return $"TO_DATE('{param.Text}','DD.MM.YYYY HH24:MI:SS')";
                    }
                default:
                    return param.Text;
            }
        }

        protected string GetDeclare(Param param)
        {
            switch (param.Type)
            {
                case "Int32":
                    {
                        return $"r_{param.Name} number;{NewLine}";
                    }
                case "String":
                    {
                        return $"r_{param.Name} varchar2(1000);{NewLine}";
                    }

                default:
                    return $"r_{param.Name} varchar2(1000);{NewLine}";
            }
        }
    }
}