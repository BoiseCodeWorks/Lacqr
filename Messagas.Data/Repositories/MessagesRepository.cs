using Dapper;
using Messages.Data.Interfaces;
using Messages.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Messages.Data.Repositories
{
    internal class MessagesRepository : DbContext
    {


        internal MessagesRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }
        internal IMessage Create(IMessage creds)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO messages(Id, UserId, Content)
                VALUES(@Id, @UserId, @Content);
                SELECT LAST_INSERT_ID();
                ", new
                {
                    Id,
                    creds.UserId,
                    creds.Content
                });
                return new Message()
                {
                    Id = Id,
                    UserId = creds.UserId,
                    Content = creds.Content
                };
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }



    }
}
