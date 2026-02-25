using System.Data;
using Microsoft.Data.SqlClient;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Interface for SQL database operations, replacing static SqlHelper for DI testability.
    /// Implementations can be registered as scoped or transient services.
    /// </summary>
    public interface ISqlHelper
    {
        SqlConnection CreateConnection();
        int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms);
        SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms);
        object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms);
    }
}
