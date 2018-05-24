using System.Data;

namespace Accounts.Data.Repositories
{
    internal class DbContext
    {
        protected readonly IDbConnection _db;

        public DbContext(IDbConnection db)
        {
            _db = db;
        }
    }
}