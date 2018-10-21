using Fenit.HelpTool.Core.SqlFileService;
using Fenit.HelpTool.Core.SqlFileService.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fenit.HelpTool.Core.SqlFileServiceTests
{
    [TestClass()]
    public class ReadSqlFileTests
    {
        [TestMethod()]
        public void ReadSqlFileTest()
        {
            var readSqlFile = new ReadSqlFile(SqlType.Procedure);
            Assert.IsTrue(readSqlFile != null);
        }

        [TestMethod()]
        public void ReadTestProcedure()
        {
            var readSqlFile = new ReadSqlFile(SqlType.Procedure);
            var sql = readSqlFile.Read();
            Assert.IsTrue(sql.SqlCommand.Contains("PS2000_INWENTARYZACJA.OkreslBufor"));
        }

        [TestMethod()]
        public void ReadTestSelect()
        {
            var readSqlFile = new ReadSqlFile(SqlType.Select);
            var sql = readSqlFile.Read();
            Assert.IsTrue(sql.SqlCommand.Contains("SELECT"));
        }
    }
}