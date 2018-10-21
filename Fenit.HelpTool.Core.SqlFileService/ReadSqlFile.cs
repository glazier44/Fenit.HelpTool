using System.IO;
using System.Xml.Serialization;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.Core.SqlFileService.XmlModel;

namespace Fenit.HelpTool.Core.SqlFileService
{
    public class ReadSqlFile
    {
        private readonly string _path;
        private readonly SqlType _type;

        public ReadSqlFile(SqlType type)
        {
            var dirs = Directory.GetCurrentDirectory();
            _path = Path.Combine(dirs, "SampleSql");
            _type = type;
        }

        public Sql Read()
        {
            var file = _type == SqlType.Procedure ? "procedure.txt" : "select.txt";
            var path = Path.Combine(_path, file);
            return ReadFile(path);
        }

        private Sql ReadFile(string fileName)
        {
            var xml = string.Empty;
            var sql = string.Empty;


            using (var sr = File.OpenText(fileName))
            {
                var s = string.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    var temp = s.Trim();
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (temp[0] == '<')
                            xml += temp;
                        else
                            sql += temp;
                    }
                }

                var desSql = Deserialize(xml);
                desSql.SqlCommand = sql;
                return desSql;
            }
        }

        private Sql Deserialize(string @string)
        {
            var serializer = new XmlSerializer(typeof(Sql), new XmlRootAttribute("Sql"));
            var stringReader = new StringReader(@string);
            var sql = (Sql) serializer.Deserialize(stringReader);
            return sql;
        }
    }
}