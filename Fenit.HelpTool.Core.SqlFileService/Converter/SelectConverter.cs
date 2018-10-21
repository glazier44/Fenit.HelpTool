using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public class SelectConverter : Converter
    {
        private readonly Sql _sql;

        public SelectConverter(Sql sql)
        {
            _sql = sql;
        }

        public override string GetSql()
        {
            var script = _sql.SqlCommand;
            foreach (var param in _sql.Param) script = Replace(param, script);
            return script;
        }
    }
}