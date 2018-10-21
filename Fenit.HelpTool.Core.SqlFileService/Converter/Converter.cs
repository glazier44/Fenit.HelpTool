using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public abstract class Converter
    {
        public abstract string GetSql();

        protected string Replace(Param param, string script)
        {
            if (param.Direction == "Input") script = script.Replace($":{param.Name}", GetValue(param));
            return script;
        }

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
                    return $"'{param.Text}'";
                }
                default:
                    return param.Text;
            }
        }
    }
}