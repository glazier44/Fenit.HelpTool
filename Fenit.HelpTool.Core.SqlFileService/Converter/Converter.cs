using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenit.HelpTool.Core.SqlFileService.Converter
{
    public abstract class Converter
    {
        public abstract string GetSql();
    }
}
