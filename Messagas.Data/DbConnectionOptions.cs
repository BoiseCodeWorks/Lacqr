using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Messages.Data
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


