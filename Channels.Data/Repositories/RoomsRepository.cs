using Channels.Data.Interfaces;
using Channels.Data.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Channels.Data.Repositories
{
    internal class RoomsRepository : DbContext
    {

        internal RoomsRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }
        internal IChannelRoom Create(INewRoom r)
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
                    r.Name,
                    r.OwnerId
                });
                return new ChannelRoom()
                {
                    Id = Id,
                    Name = r.Name,
                    OwnerId = r.OwnerId
                };
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }



    }
}
