using System;
using System.IO;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Fenit.HelpTool.Core.SqlFileService.XmlModel;
using Fenit.Toolbox.Core.Extension;

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

        public Sql Read(string val)
        {
            return ReadString(val);
        }

        public Sql Read()
        {
            var file = _type == SqlType.Procedure ? "procedure.txt" : "select.txt";
            var path = Path.Combine(_path, file);
            return ReadFile(path);
        }

        private (string xml, string sql, string error) ReadData(TextReader textReader)
        {
            var xml = string.Empty;
            var sql = string.Empty;
            var error = string.Empty;


            var s = string.Empty;
            var isError = false;
            while ((s = textReader.ReadLine()) != null)
            {
                var temp = s.Trim();
                if (!string.IsNullOrEmpty(temp))
                {
                    if (temp[0] == '<')
                    {
                        xml += temp;
                        if (temp.Contains("<ErrorInfo"))
                            isError = true;
                        else if (temp.Contains("</ErrorInfo")) isError = false;
                    }
                    else
                    {
                        var newTemp = ReduceComment(temp);
                        if (isError)
                        {
                            error += temp;
                        }
                        else if (!string.IsNullOrEmpty(newTemp))
                        {
                            sql += newTemp;
                            sql += Environment.NewLine;
                        }
                    }
                }
            }

            return (xml, sql, error);
        }

        private Sql ReadString(string text)
        {
            using (var stringReader = new StringReader(text))
            {
                string xml;
                string sql;
                string error;
                (xml, sql, error) = ReadData(stringReader);
                return CreateSql(xml, sql, error);
            }
        }


        private Sql ReadFile(string fileName)
        {
            using (var stringReader = File.OpenText(fileName))
            {
                string xml;
                string sql;
                string error;
                (xml, sql, error) = ReadData(stringReader);
                return CreateSql(xml, sql, error);
            }
        }

        private Sql CreateSql(string xml, string sql, string error)
        {
            var desSql = Deserialize(xml);
            desSql.SqlCommand = sql;
            desSql.ErrorInfo = new ErrorInfo
            {
                Text = error
            };
            return desSql;
        }

        private Sql Deserialize(string @string)
        {
            var res = @string.DeserializeFromString<Sql>();
            return res.Value;
        }

        private string ReduceComment(string sql)
        {
            var splitArray = sql.Split(new[] {"--"}, StringSplitOptions.None);
            return splitArray[0].Trim();
        }
    }
}