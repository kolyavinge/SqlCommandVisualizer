using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SqlCommandVisualizer
{
    public static class SqlCommandExt
    {
        public static string GetRawSql(this SqlCommand sqlCommand)
        {
            if (sqlCommand.CommandType == CommandType.Text)
            {
                return GetRawSqlForText(sqlCommand);
            }
            else if (sqlCommand.CommandType == CommandType.StoredProcedure)
            {
                return GetRawSqlForProcedure(sqlCommand);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static string GetRawSqlForText(SqlCommand sqlCommand)
        {
            var rawSql = sqlCommand.CommandText;
            foreach (SqlParameter parameter in sqlCommand.Parameters)
            {
                var parameterName = GetParameterName(parameter);
                var parameterValue = GetParameterValue(parameter);
                rawSql = rawSql.Replace(parameterName, parameterValue);
            }

            return rawSql;
        }

        private static string GetRawSqlForProcedure(SqlCommand sqlCommand)
        {
            var sb = new StringBuilder();
            sb.Append($"exec [{sqlCommand.CommandText}] ");
            foreach (SqlParameter parameter in sqlCommand.Parameters)
            {
                sb.Append($"[{GetParameterName(parameter)}]={GetParameterValue(parameter)}, ");
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        private static string GetParameterName(SqlParameter parameter)
        {
            return parameter.ParameterName.First() != '@' ? '@' + parameter.ParameterName : parameter.ParameterName;
        }

        private static string GetParameterValue(SqlParameter parameter)
        {
            switch (parameter.SqlDbType)
            {
                case SqlDbType.Float:
                    return ((double)parameter.Value).ToString("G", CultureInfo.InvariantCulture);
                case SqlDbType.Decimal:
                    return ((decimal)parameter.Value).ToString("G", CultureInfo.InvariantCulture);
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    return String.Format("'{0}'", parameter.Value);
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.SmallDateTime:
                    return String.Format("'{0}'", ((DateTime)parameter.Value).ToString("yyyy-MM-dd"));
                default:
                    return parameter.Value.ToString();
            }
        }
    }
}
