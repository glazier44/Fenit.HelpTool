using System;
using System.Collections.Generic;
using System.Linq;
using Fenit.Toolbox.Core.Extension;

namespace Fenit.HelpTool.Core.SqlFileService.Model
{
    public class SqlModel
    {
        public string Text { get; set; }

        public List<string> GetEmptyParameters()
        {
            var res = new List<string>();
            var text = Text;
            do
            {
                var i = text.IndexOf(":");
                if (i > 0)
                {
                    text = text.Substring(i);
                    var name = text.Substring(0, text.FirstString(new[]
                    {
                        " ", ",", ")", ";", "", Environment.NewLine, "\n"
                    }));
                    res.Add(name);
                    text = text.Substring(name.Length);
                }
            } while (text.Contains(":"));

            return res.GroupBy(w => w).Select(w => w.Key).ToList();
        }
    }
}