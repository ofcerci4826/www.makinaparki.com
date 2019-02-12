using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Vegatro.Core;
using Vegatro.Core.IO;

namespace Vegatro.Database
{
    /// <summary>
    /// Microsoft SQL Adapter 
    /// </summary>
    public class MsSQL : VegatroBase
    {
        /// <summary>
        /// SQL query command timeout in seconds
        /// </summary>
        public int TimeoutCommand { get; set; } = 600;

        /// <summary>
        /// SQL connection string
        /// </summary>
        public string ConnectionString { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPrefix"></param>
        public MsSQL(string logPrefix)
        {
            LogPrefix = logPrefix;
            TimeoutCommand    = string.IsNullOrEmpty(Config.Get("Database:CommandTimeout")) ? TimeoutCommand : Convert.ToInt32(TimeoutCommand);
            ConnectionString  = Config.Get("Database:ConnectionString");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPrefix"></param>
        /// <param name="connectionString"></param>
        public MsSQL(string logPrefix, string connectionString)
        {
            LogPrefix = logPrefix;
            TimeoutCommand    = string.IsNullOrEmpty(Config.Get("Database:CommandTimeout")) ? TimeoutCommand : Convert.ToInt32(TimeoutCommand);
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPrefix"></param>
        /// <param name="timeoutCommand"></param>
        public MsSQL(string logPrefix, int timeoutCommand)
        {
            LogPrefix = logPrefix;
            TimeoutCommand = timeoutCommand;
            ConnectionString = Config.Get("Database:ConnectionString"); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPrefix"></param>
        /// <param name="connectionString"></param>
        /// <param name="timeoutCommand"></param>
        public MsSQL(string logPrefix, string connectionString, int timeoutCommand)
        {
            LogPrefix = logPrefix;
            TimeoutCommand = timeoutCommand;
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Opens database connection
        /// </summary>
        /// <param name="connection">SqlConnection</param>
        /// <returns>Returns true if succeed</returns>
        public bool Connect(ref SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return true;
                }

                connection = new SqlConnection
                {
                    ConnectionString = ConnectionString
                };

                connection.Open();

                return true;
            }
            catch (Exception ex)
            {
                if (ex != null && ex.Data != null)
                    ex.Data.Add("Query", "Connect");

                Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Disconnects database connection
        /// </summary>
        /// <param name="connection">MySqlConnection</param>
        /// <returns>Returns true if succeed or connection is null</returns>
        public bool Disconnect(ref SqlConnection connection)
        {
            if (connection == null)
                return true;

            try
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();

                connection = null;

                return true;
            }
            catch (Exception ex)
            {
                if (ex != null && ex.Data != null)
                    ex.Data.Add("Query", "Disconnect");

                Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Opens connection and gives caller an instance of connection to run MySQL queries 
        /// and closes connection 
        /// </summary>
        /// <param name="dbAction">Desired specific database action</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>True if connection opens successfully</returns>
        public bool Prepare(Action<SqlConnection> dbAction, bool throwError)
        {
            SqlConnection connection = null;

            try
            {
                if (!Connect(ref connection))
                    return false;

                dbAction(connection);

                return true;
            }
            catch (Exception ex)
            {
                Error(ex);

                if (throwError)
                    throw ex;

                return false;
            }
            finally
            {
                Disconnect(ref connection);
            }
        }

        /// <summary>
        /// Runs query
        /// </summary>
        /// <param name="connection">ref sql connection</param>
        /// <param name="query">query string</param>
        /// <returns>Affected row count, if fails return -1</returns>
        public int RunQuery(ref SqlConnection connection, string query)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandText = query,
                Connection  = connection
            };

            objCommand.CommandTimeout = TimeoutCommand;

            int affectedRows = -1;

            Trace("[QE] " + query);
            try
            {
                affectedRows = objCommand.ExecuteNonQuery();
                Trace("[QR] " + query);
            }
            catch (Exception ex)
            {
                if (ex != null && ex.Data != null)
                    ex.Data.Add("Query", Utils.TracableQuery(query));

                throw ex;
            }

            return affectedRows;
        }

        /// <summary>
        /// Runs query
        /// </summary>
        /// <param name="connection">ref sql connection</param>
        /// <param name="procedure">store procedure name without CALL</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>Affected row count, if fails return -1</returns>
        public int RunQuery(ref SqlConnection connection, string procedure, List<object> parameters)
        {
            return RunQuery(ref connection, PrepareQuery(procedure, parameters));
        }

        /// <summary>
        /// Runs query and returns dataset
        /// </summary>
        /// <param name="connection">ref sql connection</param>
        /// <param name="query">query string</param>
        /// <returns>Returns dataset from database</returns>
        public DataSet RunDataSetQuery(ref SqlConnection connection, string query)
        {
            SqlCommand objCommand = new SqlCommand
            {
                CommandText = query,
                Connection  = connection
            };

            objCommand.CommandTimeout = TimeoutCommand;

            DataSet result = new DataSet();

            Trace("[QE] " + query);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(objCommand);
                adapter.Fill(result);

                Trace("[QR] " + query);
            }
            catch (Exception ex)
            {
                if (ex != null && ex.Data != null)
                    ex.Data.Add("Query", Utils.TracableQuery(query));

                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Runs query and returns dataset
        /// </summary>
        /// <param name="connection">ref sql connection</param>
        /// <param name="procedure">query string</param>
        /// <param name="parameters">list of parameters</param>
        /// <returns>Returns dataset from database</returns>
        public DataSet RunDataSetQuery(ref SqlConnection connection, string procedure, List<object> parameters)
        {
            return RunDataSetQuery(ref connection, PrepareQuery(procedure, parameters));
        }

        /// <summary>
        /// Creates store procedure query string
        /// </summary>
        /// <param name="procedure">Procedure name with or without EXEC</param>
        /// <param name="parameters">List of parameters</param>
        /// <returns>Query string</returns>
        public static string PrepareQuery(string procedure, List<object> parameters = null)
        {
            StringBuilder query = new StringBuilder(procedure.Contains("EXEC ") ? "" : "EXEC ");
            query.Append(procedure);

            if (parameters == null)
                return query.ToString();

            for (int i = 0; i < parameters.Count; i++)
            {
                object param = parameters[i];

                query.Append(" ");

                if (param == null)
                    query.Append("NULL");
                else if (param is long)
                    query.Append(Utils.SqlLong((long)param));
                else if (param is int)
                    query.Append(Utils.SqlInt((int)param));
                else if (param is string || param is String)
                    query.Append(Utils.SqlNVarchar((string)param));
                else if (param is double || param is float || param is Double)
                    query.Append(Utils.SqlDouble((double)param));
                else if (param is decimal || param is Decimal)
                    query.Append(Utils.SqlDecimal((decimal)param));
                else if (param is bool || param is Boolean)
                    query.Append(Utils.SqlBoolean((bool)param));
                else if (param is DateTime)
                    query.Append(Utils.SqlDatetime((DateTime)param, true));

                if (i != parameters.Count - 1)
                    query.Append(",");
            }

            return query.ToString();
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="query">Query</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public T Get<T>(string query, List<object> parameters, Func<DataRow, T> callback, bool throwError = false)
        {
            T result = default(T);

            Prepare(conn =>
            {
                DataSet ds = RunDataSetQuery(ref conn, query, parameters);
                if (!Utils.IsDataSetValid(ds))
                    return;

                foreach (DataRow row in ds.Tables[0].Rows)
                   result = callback(row);
            }, throwError);

            return result;
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="query">Query</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public T Get<T>(string query, Func<DataRow, T> callback, bool throwError = false)
        {
            return Get(query, null, callback, throwError);
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="query">Query</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public List<T> List<T>(string query, List<object> parameters, Func<DataRow, T> callback, bool throwError = false)
        {
            List<T> result = new List<T>();

            Prepare(conn =>
            {
                DataSet ds = RunDataSetQuery(ref conn, query, parameters);
                if (!Utils.IsDataSetValid(ds))
                    return;

                foreach (DataRow row in ds.Tables[0].Rows)
                    result.Add(callback(row));
            }, throwError);

            return result;
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="query">Query</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public List<T> List<T>(string query, Func<DataRow, T> callback, bool throwError = false)
        {
            return List(query, null, callback, throwError);
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="parameters">List of parameters</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public DataSet Set(string query, List<object> parameters, Action<DataSet> callback, bool throwError = false)
        {
            DataSet result = new DataSet();

            Prepare(conn =>
            {
                DataSet ds = RunDataSetQuery(ref conn, query, parameters);
                if (!Utils.IsDataSetValid(ds, false))
                    return;

                callback(ds);
                result = ds;
            }, throwError);

            return result;
        }

        /// <summary>
        /// Gets objects from database by given query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="callback">Callback method</param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>Given generic object</returns>
        public DataSet Set(string query, Action<DataSet> callback, bool throwError = false)
        {
            return Set(query, null, callback, throwError);
        }

        /// <summary>
        /// Runs query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="parameters">List of Parameters</param>
        /// <param name="checkRows"></param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>boolean</returns>
        public bool Run(string query, List<object> parameters, bool checkRows = true, bool throwError = false)
        {
            bool result = false;

            Prepare(conn =>
            {
                int rows = RunQuery(ref conn, query, parameters);

                if (!checkRows) {
                    result = true;
                    return;
                }

                if (rows >= 0)
                    result = true;
            }, throwError);

            return result;
        }

        /// <summary>
        /// Runs query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="checkRows"></param>
        /// <param name="throwError">Set it true to throw if any error occures</param>
        /// <returns>boolean</returns>
        public bool Run(string query, bool checkRows = true, bool throwError = false)
        {
            return Run(query, null, checkRows, throwError);
        }
    }
}
