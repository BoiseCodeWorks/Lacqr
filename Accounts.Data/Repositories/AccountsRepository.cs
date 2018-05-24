using Accounts.Data.Interfaces;
using Accounts.Data.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Accounts.Data.Repositories
{
    internal class AccountsRepository : DbContext
    {
        internal AccountsRepository(IDbConnection db) : base(db)
        {
        }

        internal IUser Register(IAccountRegistration creds)
        {
            // encrypt the password??
            creds.Password = BCrypt.Net.BCrypt.HashPassword(creds.Password);
            //sql
            try
            {
                string id = _db.ExecuteScalar<string>(@"
                INSERT INTO users (Id, Email, Password)
                VALUES (@Id, @Email, @Password);
                SELECT LAST_INSERT_ID();
            ", new
                {
                    Id = Guid.NewGuid().ToString(),
                    creds.Email,
                    creds.Password
                });

                return new User()
                {
                    Id = id,
                    Username = creds.Username,
                    Email = creds.Email
                };
            }
            catch (MySqlException e)
            {
                throw new Exception("ERROR: " + e.Message);
            }
        }

        internal IUser Login(IAccountLogin creds)
        {
            // more sql
            Account account = _db.QueryFirstOrDefault<Account>(@"
                SELECT * FROM users WHERE email = @Email
            ", creds);
            if (account != null)
            {
                var valid = BCrypt.Net.BCrypt.Verify(creds.Password, account.Password);
                if (valid)
                {
                    return account.GetUser();
                }
            }
            return null;
        }

        internal IUser GetUserByEmail(string email)
        {
            Account account = _db.QueryFirstOrDefault<Account>(@"
                SELECT * FROM users WHERE email = @Email
            ", new { email });
            return account.GetUser();
        }

        internal IUser GetUserById(string id)
        {
            Account account = _db.QueryFirstOrDefault<Account>(@"
            SELECT * FROM users WHERE id = @id
            ", new { id });
            return account.GetUser();
        }

        internal IUser UpdateUser(IUser user)
        {
            var i = _db.Execute(@"
                UPDATE users SET
                    email = @Email,
                    username = @Username
                WHERE id = @id
            ", user);
            if (i > 0)
            {
                return user;
            }
            return null;
        }

        internal bool ChangeUserPassword(IAccountPasswordChange change)
        {
            Account account = _db.QueryFirstOrDefault<Account>(@"
            SELECT * FROM users WHERE id = @id
            ", change);

            var valid = BCrypt.Net.BCrypt.Verify(change.OldPassword, account.Password);
            if (valid)
            {
                change.NewPassword = BCrypt.Net.BCrypt.HashPassword(change.NewPassword);
                var i = _db.Execute(@"
                    UPDATE users SET
                        password = @NewPassword
                    WHERE id = @id
                ", change);
                return true;
            }
            return false;
        }
    }

}
