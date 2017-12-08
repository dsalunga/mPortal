using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public static class SqlHelper
    {
        //Database connection strings
        public static readonly string ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        private static int timeOut;


        static SqlHelper()
        {
            string tryTimeOut = ConfigHelper.Get("TimeOut");
            timeOut = tryTimeOut == string.Empty ? 200 : Convert.ToInt32(tryTimeOut);
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnString);
        }

        public static string GetConnectionString(string connectionStringKey)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        }

        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open) 
                        conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            // Log
                            throw;
                        }
                        //finally
                        //{

                        //}
                    }
                }
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    if (conn.State != ConnectionState.Open) 
                        conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, CommandType.StoredProcedure, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                if (conn.State != ConnectionState.Open) 
                    conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        trans.Commit();
                        return val;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    trans.Commit();
                    return val;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        //public static DbDataReader ExecuteSelectReader(string tableName, params DbParameter[] dbParams)
        //{
        //    return null;
        //}

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(ConnString, cmdText, cmdParms);
        }

        public static SqlDataReader ExecuteReader(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet ExecuteDataSet(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);

                        try
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                cmd.Parameters.Clear();

                                trans.Commit();
                                return ds;
                            }
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        public static DataSet ExecuteDataSetSchema(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.FillSchema(ds, SchemaType.Mapped);
                        da.Fill(ds);

                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        public static DataSet ExecuteDataSetSchema(SqlConnection cnn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, cnn, null, cmdType, cmdText, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.FillSchema(ds, SchemaType.Mapped);
                    da.Fill(ds);

                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }

        public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        public static DataSet ExecuteDataSet(string cmdText, string dataSetName, string tableName, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, cmdText, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet(dataSetName);
                        da.Fill(ds, tableName);

                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        public static void ExecuteUpdateDataSet(string cmdText, DataTable table)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da))
                        {
                            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                            da.Update(table);

                            cmd.Parameters.Clear();
                        }
                    }
                }
            }
        }

        public static void ExecuteUpdateDataSet(SqlConnection conn, string cmdText, DataTable table)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                //using (conn)
                //{
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    using (SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da))
                    {
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Update(table);

                        cmd.Parameters.Clear();
                    }
                }
                //}
            }
        }

        //public static SqlDataAdapter ExecuteDataAdapter(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) 
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    SqlConnection conn = new SqlConnection(connString);

        //    // we use a try/catch here because if the method throws an exception we want to 
        //    // close the connection throw code, because no datareader will exist, hence the 
        //    // commandBehaviour.CloseConnection will not work
        //    try 
        //    {
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        //        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        //        return da;
        //    }
        //    catch
        //    {
        //        conn.Close();
        //        throw;
        //    }
        //}

        //public static SqlDataAdapter ExecuteDataAdapter(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    SqlConnection conn = new SqlConnection(ConnString);

        //    // we use a try/catch here because if the method throws an exception we want to 
        //    // close the connection throw code, because no datareader will exist, hence the 
        //    // commandBehaviour.CloseConnection will not work
        //    try
        //    {
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        //        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        //        return da;
        //    }
        //    catch
        //    {
        //        conn.Close();
        //        throw;
        //    }
        //}

        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                            object val = cmd.ExecuteScalar();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                            object val = cmd.ExecuteScalar();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static object ExecuteScalar(string connString, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State != ConnectionState.Open) conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            PrepareCommand(cmd, conn, trans, CommandType.StoredProcedure, cmdText, cmdParms);
                            object val = cmd.ExecuteScalar();
                            cmd.Parameters.Clear();

                            trans.Commit();
                            return val;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms)
        {
            return ExecuteScalar(ConnString, cmdText, cmdParms);
        }

        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                        object val = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();

                        trans.Commit();
                        return val;

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = timeOut;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}