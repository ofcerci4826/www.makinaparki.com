using System;
using System.Data;

namespace Vegatro.Database
{
    /// <summary>
    /// Utility methods
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// DataSet NULL mı, ds.Tables NULL mı, ds.Tables[0] NULL mı, ds.Tables[0].Rows NULL mı kontrollerini yapar
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="checkRowCount">TRUE ise ds.Tables[0].Rows.Count > 0 mı kontrolu de yapar</param>
        /// <returns>DataSet in valid olup olmadigini doner</returns>
        public static bool IsDataSetValid(DataSet ds, bool checkRowCount = true)
        {
            return !(ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0] == null || ds.Tables[0].Rows == null || (checkRowCount && ds.Tables[0].Rows.Count <= 0));
        }

        /// <summary>
        /// Gets integer value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>integer</returns>
        public static int GetInteger(object column, int defaultValue = -1)
        {
            return (column != DBNull.Value ? Convert.ToInt32(column) : defaultValue);
        }

        /// <summary>
        /// Gets long value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>long</returns>
        public static long GetLong(object column, long defaultValue = -1)
        {
            return (column != DBNull.Value ? Convert.ToInt64(column) : defaultValue);
        }

        /// <summary>
        /// Gets double value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>double</returns>
        public static double GetDouble(object column, double defaultValue = -1)
        {
            return (column != DBNull.Value ? Convert.ToDouble(column) : defaultValue);
        }

        /// <summary>
        /// Gets string value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>string</returns>
        public static string GetString(object column, string defaultValue = "")
        {
            return (column != DBNull.Value ? Convert.ToString(column) : defaultValue);
        }

        /// <summary>
        /// Gets bool value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>bool</returns>
        public static bool GetBool(object column, bool defaultValue = false)
        {
            return (column != DBNull.Value ? Convert.ToBoolean(column) : defaultValue);
        }

        /// <summary>
        /// Gets datetime value of column
        /// </summary>
        /// <param name="column">column</param>
        /// <returns>datetime</returns>
        public static DateTime GetDatetime(object column)
        {
            return (column != DBNull.Value ? Convert.ToDateTime(column) : default(DateTime));
        }

        /// <summary>
        /// Gets integer value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>integer</returns>
        public static int GetInteger(this DataRow row, string column, int defaultValue = -1)
        {
            return row.Table.Columns.Contains(column) ? GetInteger(row[column], defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets long value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>long</returns>
        public static long GetLong(this DataRow row, string column, long defaultValue = -1)
        {
            return row.Table.Columns.Contains(column) ? GetLong(row[column], defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets double value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>double</returns>
        public static double GetDouble(this DataRow row, string column, double defaultValue = -1)
        {
            return row.Table.Columns.Contains(column) ? GetDouble(row[column], defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets string value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>string</returns>
        public static string GetString(this DataRow row, string column, string defaultValue = "")
        {
            return row.Table.Columns.Contains(column) ? GetString(row[column], defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets bool value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>bool</returns>
        public static bool GetBool(this DataRow row, string column, bool defaultValue = false)
        {
            return row.Table.Columns.Contains(column) ? GetBool(row[column], defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets datetime value of column
        /// </summary>
        /// <param name="row">DataRow</param>
        /// <param name="column">column</param>
        /// <returns>datetime</returns>
        public static DateTime GetDatetime(this DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? GetDatetime(row[column]) : default(DateTime);
        }

        /// <summary>
        /// Converts the value of the specified Int to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        public static string SqlInt(int? Parameter)
        {
            if (Parameter == null)
                return "NULL";

            return Convert.ToString(Parameter);
        }

        /// <summary>
        /// Converts the value of the specified Int to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String.  A long or NULL value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlLong(long? Parameter)
        {
            if (Parameter == null)
                return "NULL";

            return Convert.ToString(Parameter);
        }

        /// <summary>
        /// Converts the value of the specified double to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String.  A double or NULL value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlDouble(double? Parameter)
        {
            if (Parameter == null)
                return "NULL";

            string retVal = Convert.ToString(Parameter);
            retVal = retVal.Replace(",", ".");

            return retVal;
        }

        /// <summary>
        /// Converts the value of the specified decimal to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String.  A decimal or NULL value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlDecimal(decimal? Parameter)
        {
            if (Parameter == null)
                return "NULL";

            string retVal = Convert.ToString(Parameter);
            retVal = retVal.Replace(",", ".");

            return retVal;
        }

        /// <summary>
        /// Converts the value of the specified string to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String. A string value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlVarchar(string Parameter)
        {
            if (string.IsNullOrEmpty(Parameter))
                return "NULL";

            Parameter = Parameter.Replace("'", "''");
            Parameter = "'" + Parameter + "'";

            return Parameter;
        }

        /// <summary>
        /// Converts the value of the specified string to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String. A string value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlNVarchar(string Parameter)
        {
            if (string.IsNullOrEmpty(Parameter))
                return "NULL";

            Parameter = Parameter.Replace("'", "''");
            Parameter = "N'" + Parameter + "'";

            return Parameter;
        }

        /// <summary>
        /// Converts the value of the specified Int to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <example>
        /// if(UseHoureMinute)
        ///     dateFormatString = "yyyyMMdd HH:mm"
        /// else
        ///     dateFormatString = "yyyyMMdd"
        /// </example>
        /// <param name="Parameter">Parameter that will be converted System.String.  A DateTime or NULL value.</param>
        /// <param name="UseHoureMinute">UseHoureMinute that will use format string.  A True or False value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlDatetime(DateTime? Parameter, bool UseHoureMinute)
        {
            if (Parameter == null)
                return "NULL";

            string toStringFormat = "yyyyMMdd";
            if (UseHoureMinute)
                toStringFormat += " HH:mm";

            DateTime dateTime = Convert.ToDateTime(Parameter);

            string retVal = null;
            retVal = dateTime.ToString(toStringFormat);
            retVal = retVal.Replace("'", "''");
            retVal = "'" + retVal + "'";

            return retVal;
        }

        /// <summary>
        /// Converts the value of the specified bool to its equivalent System.String
        ///     representation.
        /// </summary>
        /// <param name="Parameter">Parameter that will be converted System.String.  A bool or NULL value.</param>
        /// <returns>The System.String equivalent of the value of value</returns>
        public static string SqlBoolean(bool? Parameter)
        {
            if (Parameter == null)
                return "NULL";

            if (Parameter == true)
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// Prepares given query to trace
        /// If given string's length greater than 150, crops it and adds ... to the end
        /// </summary>
        /// <param name="query">Sql query</param>
        /// <returns>Tracable query</returns>
        public static string TracableQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
                return "";

            if (query.Length > 150)
                return query.Substring(0, 150) + "...";

            return query;
        }
    }
}
