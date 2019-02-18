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
        public void GetRawSql_Query_Int()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 10);
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=10";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_Int1()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@param1", 10);
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=10";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_IntEnum()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", SomeEnum.One);
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=1";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_String()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", "string");
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1='string'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_IntString()
        {
            var query = @"select * from Table where Column1=@param1 and Column2=@param2";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 10);
            sqlCommand.Parameters.AddWithValue("param2", "string");
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=10 and Column2='string'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_Float()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 1.2);
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=1.2";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_Decimal()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", 1.2m);
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1=1.2";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Query_DateTime()
        {
            var query = @"select * from Table where Column1=@param1";
            var sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("param1", DateTime.Parse("01.10.2000"));
            var actual = sqlCommand.GetRawSql();
            var expected = @"select * from Table where Column1='2000-10-01'";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRawSql_Procedure_IntString()
        {
            var query = @"ProcedureName";
            var sqlCommand = new SqlCommand(query) { CommandType = CommandType.StoredProcedure };
            sqlCommand.Parameters.AddWithValue("param1", 10);
            sqlCommand.Parameters.AddWithValue("param2", "string");
            var actual = sqlCommand.GetRawSql();
            var expected = @"exec [ProcedureName] [@param1]=10, [@param2]='string'";
            Assert.AreEqual(expected, actual);
        }

        enum SomeEnum
        {
            One = 1, Two = 2
        }
    }
}
