using System.Data;
using System.Data.SqlClient;

namespace Messages.Data
{
    internal class DbConnectionOptions
    {
        internal IDbConnection GetMySqlConnection(string _connectionString)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}


