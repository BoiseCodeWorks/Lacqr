using Channels.Data.Interfaces;
using Channels.Data.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Channels.Data.Repositories
{
    internal class ChannelsRepository : DbContext
    {

        internal ChannelsRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }
        internal IChannel Create(INewChannel c)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO channels(Id, Name, OwnerId)
                VALUES(@Id, @Name, @OwnerId, @RoomId);
                SELECT LAST_INSERT_ID();
                ", new
                {
                    Id,
                    c.Name,
                    c.OwnerId
                });
                return new Channel()
                {
                    Id = Id,
                    Name = c.Name,
                    OwnerId = c.OwnerId
                };
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }



    }
}
