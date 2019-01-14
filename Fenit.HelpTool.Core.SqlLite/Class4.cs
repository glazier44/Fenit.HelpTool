using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenit.HelpTool.Core.SqlLite
{
    public static class test
    {
        public static void Test()
        {

            using (var db = new SqlLiteContext())
            {
                var gg = db.AnimalTypes.ToArray();
            }
        }
    }
}
