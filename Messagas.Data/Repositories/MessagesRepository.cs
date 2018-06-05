using Dapper;
using Messages.Data.Interfaces;
using Messages.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Messages.Data.Repositories
{
    internal class MessagesRepository : DbContext
    {


        internal MessagesRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }
        internal IMessage Create(INewMessage m)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                int id = _db.ExecuteScalar<int>(@"
                INSERT INTO messages(Id, UserId, Content, RoomId)
                VALUES(@Id, @UserId, @Content, @RoomId);
                SELECT LAST_INSERT_ID();
                ", new
                {
                    Id,
                    m.UserId,
                    m.Content,
                    m.RoomId
                });
                return new Message()
                {
                    Id = Id,
                    UserId = m.UserId,
                    Content = m.Content,
                    RoomId = m.RoomId
                };
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        internal string Delete(string id)
        {
            var deleteMessage = _db.Execute(@"DELETE FROM messages WHERE Id = id", new {id});
            return deleteMessage > 0 ? "Succesfully deleted message" : "Nothing Deleted";
        }

        internal List<IMessage> GetMessages()
        {
            var rawMessages = _db.Query<Message>($@"SELECT * FROM messages");
            List <IMessage> messages = new List<IMessage>();
            foreach(var m in rawMessages)
            {
                messages.Add(m);
            }
            return messages;
        }
    }
}
