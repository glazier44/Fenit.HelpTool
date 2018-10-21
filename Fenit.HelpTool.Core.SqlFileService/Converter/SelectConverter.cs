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
            var ss = _sql.SqlCommand;



            return ss;
        }
    }
}
