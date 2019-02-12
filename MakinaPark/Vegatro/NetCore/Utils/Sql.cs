using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Vegatro.Database;

namespace Vegatro.NetCore.Utils
{
    /// <summary>
    /// MsSql Singleton
    /// </summary>
    public class Sql
    {
        /// <summary>
        /// Sql Instance
        /// </summary>
        private static MsSQL SqlInstance { get; set; }

        /// <summary>
        /// Resets sql singleton instance
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static void ResetInstance(string prefix = null)
        {
            SqlInstance = new MsSQL((!string.IsNullOrEmpty(prefix) ? prefix : LogHelper.GetPrefix()));
            SqlInstance.OnError = (error) =>
            {
                SaveError(error);
            };
        }

        /// <summary>
        /// Gets sql singleton instance
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static MsSQL GetInstance(string prefix = null)
        {
            if (SqlInstance != null)
                return SqlInstance;

            SqlInstance = new MsSQL((!string.IsNullOrEmpty(prefix) ? prefix : LogHelper.GetPrefix()));
            SqlInstance.OnError = (error) =>
            {
                SaveError(error);
            };

            return SqlInstance;
        }

        /// <summary>
        /// Saves error to database (sp_diagnostic_error)
        /// </summary>
        /// <param name="error">Exception</param>
        public static void SaveError(Exception error)
        {
            if (error == null)
                return;

            SqlConnection connection = null;

            try
            {
                if (!SqlInstance.Connect(ref connection))
                    return;

                string query = "QUERY EXECUTION";

                if (error.Data != null && error.Data.Contains("Query"))
                    query = error.Data["Query"].ToString();

                SqlInstance.RunQuery(ref connection, "sp_diagnostic_error", new List<object> { Error.UNEXPECTED, Module.DATABASE, query, "Unexpected database error", error.Message });
            }
            catch (Exception)
            {

            }
            finally
            {
                SqlInstance.Disconnect(ref connection);
            }
        }
    }
}
