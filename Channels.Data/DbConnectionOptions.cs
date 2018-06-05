using MySql.Data.MySqlClient;
using System.Data;

namespace Channels.Data
{
    internal class DbConnectionOptions
    {
        internal IDbConnection GetMySqlConnection(string _connectionString)
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}


