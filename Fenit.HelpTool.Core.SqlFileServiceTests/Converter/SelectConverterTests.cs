using Fenit.HelpTool.Core.SqlFileService;
using Fenit.HelpTool.Core.SqlFileService.Converter;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fenit.HelpTool.Core.SqlFileServiceTests.Converter
{
    [TestClass]
    public class SelectConverterTests
    {
        [TestMethod]
        public void SelectConverterTest()
        {
            var readSqlFile = new ReadSqlFile(SqlType.Select);
            var sql = readSqlFile.Read();
            var selectConverter = new SelectConverter(sql);
            Assert.IsTrue(selectConverter != null);
        }

        [TestMethod]
        public void GetSqlTest()
        {
            var readSqlFile = new ReadSqlFile(SqlType.Select);
            var sql = readSqlFile.Read();
            var selectConverter = new SelectConverter(sql);
            var result = selectConverter.GetSql();
            Assert.IsNotNull(result);
        }
    }
}