using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlCommandVisualizer
{
    [TestClass]
    public class SqlCommandExtTest
    {
        [TestMethod]
        public void RawSqlText_Query_Int()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 10);
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=10";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_Int1()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@param1", 10);
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=10";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_IntEnum()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", SomeEnum.One);
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=1";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_String()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", "string");
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1='string'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_IntString()
        {
            var query = @"select * from Table where Column1=@param1 and Column2=@param2";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 10);
            sqlCommand.Parameters.AddWithValue("param2", "string");
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=10 and Column2='string'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_Float()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 1.2);
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=1.2";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_Decimal()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 1.2m);
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1=1.2";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Query_DateTime()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", DateTime.Parse("01.10.2000"));
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"select * from Table where Column1='2000-10-01'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RawSqlText_Procedure_IntString()
        {
            var query = @"ProcedureName";
            var sqlCommand = new SqlCommand(query) { CommandType = CommandType.StoredProcedure };
            sqlCommand.Parameters.AddWithValue("param1", 10);
            sqlCommand.Parameters.AddWithValue("param2", "string");
            var actual = sqlCommand.GetSqlText().RawSqlText;
            var expected = @"exec [ProcedureName] [@param1]=10, [@param2]='string'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Text_FormatedSqlText_LeadSpacesTrim()
        {
            var query = @"select *
                          from table
                          where a=1";

            var expected = @"select *
from table
where a=1";

            var sqlText = new SqlCommandExt.SqlText(query);
            Assert.AreEqual(expected, sqlText.FormatedSqlText);
        }

        enum SomeEnum
        {
            One = 1, Two = 2
        }
    }
}
