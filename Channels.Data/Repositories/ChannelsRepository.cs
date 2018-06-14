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
                VALUES(@Id, @Name, @OwnerId);
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

        internal IChannel GetChannel(ISubscriber sub)
        {
            var userIds = _db.Query<string>(@"SELECT userId FROM channelsubscribers WHERE channelId = @SubscribableId;", sub);
            var channel = _db.QueryFirstOrDefault<Channel>(@"
            SELECT * FROM ChannelSubscribers cs JOIN Channels c
            ON c.id = cs.channelId
            WHERE cs.userId = @UserId
            AND cs.channelId = @SubscribableId
            ", sub);
            channel.Subscribers = userIds;
            return channel;
        }

        internal IEnumerable<IChannel> GetSubscribedChannels(string userId)
        {
            return _db.Query<Channel>(@"
            SELECT * FROM ChannelSubscribers cs 
            JOIN Channels c ON c.id = cs.channelId
            WHERE cs.userId = @userId
            ", new
            {
                userId
            });
        }

        internal IEnumerable<IChannel> GetAllChannels()
        {
            return _db.Query<Channel>(@"
            SELECT * FROM Channels
            ");
        }

        internal void Subscribe(ISubscriber subscriber)
        {
            string id = Guid.NewGuid().ToString();
            _db.ExecuteScalar(@"
            INSERT INTO ChannelSubscribers (Id, ChannelId, UserId)
            VALUES (@id, @channelId, @userId)
            ", new
            {
                id,
                channelId = subscriber.SubscribableId,
                userId = subscriber.UserId
            });
        }

        internal void Unsubscribe(ISubscriber subscriber)
        {
            _db.ExecuteScalar(@"
            DELETE FROM ChannelSubscribers
            WHERE UserId = @UserId
            AND ChannelId = @SubscribablelId
            ", subscriber);
        }



    }
}
